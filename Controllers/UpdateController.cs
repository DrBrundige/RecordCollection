using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using IntroToEntity.Models;

// using System.Diagnostics;
// using System.Threading.Tasks;
// using Microsoft.EntityFrameworkCore;
// using Microsoft.AspNetCore.Identity;
// using Microsoft.AspNetCore.Http;

namespace IntroToEntity.Controllers
{
	public class UpdateController : Controller
	{
		private Context _context;

		// @todo Rework Add Band, Record, Song, City to return strings

		public UpdateController(Context context)
		{
			_context = context;
		}

		[Route("bands/{id}")]
		[HttpPut]
		public JsonResult UpdateBand(int id, [FromBody] MyPostView body)
		{
			MyResponseView results = new MyResponseView();

			try
			{
				System.Console.WriteLine("Updating band where id is " + id);

				Band band = _context.Bands.FirstOrDefault(x => x.BandId == id);

				// If band is null, no band with id exists. Otherwise continues
				if (band == null)
				{
					results.Success = false;
					results.RowsUpdated = 0;
					System.Console.WriteLine("Failure! No band with id " + id + " exists! No bands updated!");
					results.Message = "Failure! No band with id " + id + " exists! No bands updated!";
					return Json(results);
				}

				// If band is null, no band with id exists. Otherwise continues
				if (body.band == null)
				{
					results.Success = false;
					results.RowsUpdated = 0;
					System.Console.WriteLine("Failure! Modelstate Invalid! No Bands updated!");
					results.Message = "Failure! Modelstate Invalid! No bands updated!";
					return Json(results);
				}

				// If any of these fields exist in the body, overwrite them
				if (body.band.Name != null)
				{
					band.Name = body.band.Name;
				}
				if (body.band.DisplayName != null)
				{
					band.DisplayName = body.band.DisplayName;
				}
				if (body.band.CityId != 0)
				{
					band.CityId = body.band.CityId;
				}
				band.UpdatedAt = DateTime.Now;

				_context.Update(band);
				_context.SaveChanges();

				results.Success = true;
				results.RowsUpdated = 1;
				results.Message = "Success! Band " + band.Name + " updated";

				return Json(results);
			}
			catch (System.Exception)
			{
				// results.Songs = _context.Songs.ToList();
				results.Success = false;
				results.RowsUpdated = 0;
				System.Console.WriteLine("Failure! An unknown error occured! No bands updated!");
				results.Message = "Failure! An unknown error occured! No bands updated!";
				return Json(results);

				throw;
			}

		}

		[Route("records/{id}")]
		[HttpPut]
		public JsonResult UpdateRecord(int id, [FromBody] MyPostView body)
		{
			MyResponseView results = new MyResponseView();

			try
			{
				System.Console.WriteLine("Updating band where id is " + id);

				Record record = _context.Records.FirstOrDefault(x => x.RecordId == id);

				// If band is null, no band with id exists. Otherwise continues
				if (record == null)
				{
					results.Success = false;
					results.RowsUpdated = 0;
					System.Console.WriteLine("Failure! No record with id " + id + " exists! No records updated!");
					results.Message = "Failure! No record with id " + id + " exists! No records updated!";
					return Json(results);
				}

				// If record is null, no record with id exists. Otherwise continues
				if (body.record == null)
				{
					results.Success = false;
					results.RowsUpdated = 0;
					System.Console.WriteLine("Failure! Modelstate Invalid! No records updated!");
					results.Message = "Failure! Modelstate Invalid! No records updated!";
					return Json(results);
				}

				// If any of these fields exist in the body, overwrite them
				// Name, BandId, Year, LabelId, Zeitgeist, Certification, DisplayName
				if (body.record.Name != null)
				{
					record.Name = body.record.Name;
				}
				if (body.record.DisplayName != null)
				{
					record.DisplayName = body.record.DisplayName;
				}
				if (body.record.Year != 0)
				{
					record.Year = body.record.Year;
				}
				if (body.record.Zeitgeist != 0)
				{
					record.Zeitgeist = body.record.Zeitgeist;
				}
				if (body.record.Certification != null)
				{
					record.Certification = body.record.Certification;
				}

				if (body.record.BandId != 0)
				{
					if (_context.Bands.FirstOrDefault(x => x.BandId == body.record.BandId) != null)
						record.BandId = body.record.BandId;
				}

				// Resets UpdatedAt timestamp
				record.UpdatedAt = DateTime.Now;

				_context.Update(record);
				_context.SaveChanges();

				results.Success = true;
				results.RowsUpdated = 1;
				results.Message = "Success! Record " + record.Name + " updated";

				return Json(results);
			}
			catch (System.Exception)
			{
				// results.Songs = _context.Songs.ToList();
				results.Success = false;
				results.RowsUpdated = 0;
				System.Console.WriteLine("Failure! An unknown error occured! No records updated!");
				results.Message = "Failure! An unknown error occured! No records updated!";
				return Json(results);

				throw;
			}

		}

		[Route("songs/{id}")]
		[HttpPut]
		public JsonResult UpdateSong(int id, [FromBody] MyPostView body)
		{
			MyResponseView results = new MyResponseView();

			try
			{
				System.Console.WriteLine("Updating band where id is " + id);

				Song song = _context.Songs.FirstOrDefault(x => x.SongId == id);

				// If band is null, no band with id exists. Otherwise continues
				if (song == null)
				{
					results.Success = false;
					results.RowsUpdated = 0;
					System.Console.WriteLine("Failure! No song with id " + id + " exists! No songs updated!");
					results.Message = "Failure! No song with id " + id + " exists! No songs updated!";
					return Json(results);
				}

				// If record is null, no record with id exists. Otherwise continues
				if (body.song == null)
				{
					results.Success = false;
					results.RowsUpdated = 0;
					System.Console.WriteLine("Failure! Modelstate Invalid! No songs updated!");
					results.Message = "Failure! Modelstate Invalid! No songs updated!";
					return Json(results);
				}

				// If any of these fields exist in the body, overwrite them
				// Name, BandId, Year, LabelId, Zeitgeist, Certification, DisplayName
				if (body.song.Name != null)
				{
					song.Name = body.song.Name;
				}
				if (body.song.TrackListing != 0)
				{
					song.TrackListing = body.song.TrackListing;
				}
				if (body.song.Score != 0)
				{
					song.Score = body.song.Score;
				}

				if (body.song.RecordId != 0)
				{
					if (_context.Bands.FirstOrDefault(x => x.BandId == body.record.BandId) != null)
						song.RecordId = body.record.BandId;
				}

				// Resets UpdatedAt timestamp
				song.UpdatedAt = DateTime.Now;

				_context.Update(song);
				_context.SaveChanges();

				results.Success = true;
				results.RowsUpdated = 1;
				results.Message = "Success! Song " + song.Name + " updated";

				return Json(results);
			}
			catch (System.Exception)
			{
				// results.Songs = _context.Songs.ToList();
				results.Success = false;
				results.RowsUpdated = 0;
				System.Console.WriteLine("Failure! An unknown error occured! No songs updated!");
				results.Message = "Failure! An unknown error occured! No songs updated!";
				return Json(results);

				throw;
			}

		}


	}
}
