using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IntroToEntity.Models;

// using System.Diagnostics;
// using System.Threading.Tasks;
// using Microsoft.AspNetCore.Identity;
// using Microsoft.AspNetCore.Http;

namespace IntroToEntity.Controllers
{
	public class DeleteController : Controller
	{
		private Context _context;

		// @todo Rework Add Band, Record, Song, City to return strings

		public DeleteController(Context context)
		{
			_context = context;
		}

		[Route("songs/{id}")]
		[HttpDelete]
		public JsonResult DeleteSong(int id)
		{
			// Deletes a song with the given int id
			MyResponseView results = new MyResponseView();

			try
			{
				System.Console.WriteLine("Deleting song where id is " + id);

				Song song = _context.Songs.FirstOrDefault(x => x.SongId == id);

				// If song is null, no song by id exists. Otherwise continues
				if (song == null)
				{
					results.Success = false;
					results.RowsUpdated = 0;
					System.Console.WriteLine("Failure! No song with id " + id + " exists! No songs deleted!");
					results.Message = "Failure! No song with id " + id + " exists! No songs deleted!";
					return Json(results);
				}

				_context.Remove(song);
				_context.SaveChanges();

				results.Success = true;
				results.RowsUpdated = 1;
				results.Message = "Success! Song " + song.Name + " deleted";

				return Json(results);
			}
			catch (System.Exception)
			{
				// results.Songs = _context.Songs.ToList();
				results.Success = false;
				results.RowsUpdated = 0;
				System.Console.WriteLine("Failure! An unknown error occured! No songs deleted!");
				results.Message = "Failure! An unknown error occured! No songs deleted!";
				return Json(results);

				throw;
			}

		}

		[Route("records/{id}")]
		[HttpDelete]
		public JsonResult DeleteRecord(int id)
		{
			MyResponseView results = new MyResponseView();
			// In order to delete a record, all songs belonging to that record must first be deleted

			try
			{
				System.Console.WriteLine("Deleting record where id is " + id);

				Record record = _context.Records.FirstOrDefault(x => x.RecordId == id);

				if (record == null)
				{
					results.Success = false;
					results.RowsUpdated = 0;
					System.Console.WriteLine("Failure! No record with id " + id + " exists! No records deleted!");
					results.Message = "Failure! No record with id " + id + " exists! No records deleted!";
					return Json(results);
				}

				IEnumerable<Song> songs = _context.Songs.Where(x => x.RecordId == id);
				int noSongs = songs.Count();

				// Removes each song where recordid is parameterized id
				foreach (Song song in songs)
				{
					_context.Songs.Remove(song);
				}
				_context.Records.Remove(record);
				_context.SaveChanges();

				results.Success = true;
				results.RowsUpdated = noSongs + 1;
				results.Message = "Success! Record " + record.Name + " and " + noSongs + " songs deleted";

				return Json(results);
			}
			catch (System.Exception)
			{
				// results.Songs = _context.Songs.ToList();
				results.Success = false;
				results.RowsUpdated = 0;
				System.Console.WriteLine("Failure! An unknown error occured! No records deleted!");
				results.Message = "Failure! An unknown error occured! No records deleted!";
				return Json(results);

				throw;
			}

		}

		[Route("bands/{id}")]
		[HttpDelete]
		public JsonResult DeleteBand(int id)
		{
			MyResponseView results = new MyResponseView();

			try
			{
				System.Console.WriteLine("Deleting band where id is " + id);

				Band band = _context.Bands.FirstOrDefault(x => x.BandId == id);

				// If band is null, no band with id exists. Otherwise continues
				if (band == null)
				{
					results.Success = false;
					results.RowsUpdated = 0;
					System.Console.WriteLine("Failure! No band with id " + id + " exists! No bands deleted!");
					results.Message = "Failure! No band with id " + id + " exists! No bands deleted!";
					return Json(results);
				}

				IEnumerable<Record> records = _context.Records.Where(x => x.BandId == id);
				int noRecords = records.Count();
				int noSongs = 0;

				// For each record with bandid, deletes each song, then deletes record
				foreach (Record record in records)
				{
					IEnumerable<Song> songs = _context.Songs.Where(x => x.RecordId == record.RecordId);
					noSongs += songs.Count();

					foreach (Song song in songs)
					{
						_context.Songs.Remove(song);
					}
					_context.Records.Remove(record);
				}

				_context.Bands.Remove(band);
				_context.SaveChanges();

				results.Success = true;
				results.RowsUpdated = noRecords + noSongs + 1;
				results.Message = "Success! Band " + band.Name + " and " + noSongs + " songs deleted";

				return Json(results);
			}
			catch (System.Exception)
			{
				// results.Songs = _context.Songs.ToList();
				results.Success = false;
				results.RowsUpdated = 0;
				System.Console.WriteLine("Failure! An unknown error occured! No bands deleted!");
				results.Message = "Failure! An unknown error occured! No bands deleted!";
				return Json(results);

				throw;
			}

		}

	}
}
