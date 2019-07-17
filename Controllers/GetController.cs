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
	public class GetController : Controller
	{
		private Context _context;

		// @todo Rework Add Band, Record, Song, City to return strings

		public GetController(Context context)
		{
			_context = context;
		}

		[Route("songs")]
		[HttpGet]
		public JsonResult AllSongs()
		{
			System.Console.WriteLine("Returning all songs");
			List<Song> Songs = _context.Songs.ToList();

			MyAllRecordsView results = new MyAllRecordsView();

			Dictionary<string, object> songName;
			foreach (Song song in Songs)
			{
				songName = new Dictionary<string, object>();
				songName.Add("Name", song.Name);
				songName.Add("ID", song.SongId);
				songName.Add("RecordID", song.RecordId);
				songName.Add("Score", song.Score);
				results.Data.Add(songName);
			}

			// results.Songs = _context.Songs.ToList();
			results.Success = true;
			results.Message = "Success! Returned some songs";

			return Json(results);
		}

		[Route("records")]
		[HttpGet]
		public JsonResult AllRecords()
		{
			System.Console.WriteLine("Returning all records");
			List<Record> Records = _context.Records.ToList();

			MyAllRecordsView results = new MyAllRecordsView();

			Dictionary<string, object> recordName;
			foreach (Record record in Records)
			{
				recordName = new Dictionary<string, object>();
				recordName.Add("Name", record.Name);
				recordName.Add("ID", record.RecordId);
				recordName.Add("BandId", record.BandId);
				results.Data.Add(recordName);
			}

			// results.Songs = _context.Songs.ToList();
			results.Success = true;
			results.Message = "Success! Returned some songs";

			return Json(results);
		}

		[Route("bands")]
		[HttpGet]
		public JsonResult AllBands()
		{
			System.Console.WriteLine("Returning all bands");
			List<Band> Bands = _context.Bands.ToList();

			MyAllRecordsView results = new MyAllRecordsView();

			Dictionary<string, object> bandName;
			foreach (Band band in Bands)
			{
				bandName = new Dictionary<string, object>();
				if (band.DisplayName == null)
				{
					bandName.Add("Name", band.Name);
				} else {
					bandName.Add("Name", band.DisplayName);
				}
				bandName.Add("BandId", band.BandId);
				results.Data.Add(bandName);
			}

			// results.Songs = _context.Songs.ToList();
			results.Success = true;
			results.Message = "Success! Returned some bands";

			return Json(results);
		}

		[Route("musicians")]
		[HttpGet]
		public JsonResult AllMusicians()
		{
			System.Console.WriteLine("Returning all musicians");
			List<Musician> Musicians = _context.Musicians.ToList();

			MyAllRecordsView results = new MyAllRecordsView();

			Dictionary<string, object> musicianName;
			foreach (Musician musician in Musicians)
			{
				musicianName = new Dictionary<string, object>();
				musicianName.Add("Name", musician.Name);
				musicianName.Add("ID", musician.MusicianId);
				if (musician.GivenName != null)
				{
					musicianName.Add("GivenName", musician.GivenName);
				}
				results.Data.Add(musicianName);
			}

			// results.Songs = _context.Songs.ToList();
			results.Success = true;
			results.Message = "Success! Returned " + results.Data.Count() + " musicians";

			return Json(results);
		}

		[Route("tags")]
		[HttpGet]
		public JsonResult AllTags()
		{
			System.Console.WriteLine("Returning all tags");
			List<Tag> Tags = _context.Tags.ToList();

			MyAllRecordsView results = new MyAllRecordsView();

			Dictionary<string, object> tagName;
			foreach (Tag tag in Tags)
			{
				tagName = new Dictionary<string, object>();
				tagName.Add("TagID", tag.TagId);
				tagName.Add("Name", tag.Name);
				tagName.Add("Genre", tag.Genre);
				results.Data.Add(tagName);
			}
			// results.Songs = _context.Songs.ToList();
			results.Success = true;
			results.Message = "Success! Returned " + results.Data.Count() + " tags";

			return Json(results);
		}

		[Route("records/info/{id}")]
		[HttpGet]
		public JsonResult GetRecord(int id)
		{
			System.Console.WriteLine("Searching for record: " + id);
			MyRecordsView results = new MyRecordsView();

			Record record = _context.Records.Include(x => x.Band).ThenInclude(x => x.City).FirstOrDefault(x => x.RecordId == id);
			if (record != null)
			{
				results.Record = record;

				results.Success = true;
				results.Message = "Success! Returned record: " + id;
				System.Console.WriteLine("Success! Returned record: " + id);
			}
			else
			{
				results.Success = false;
				results.Message = "Failure! Could not find record with name " + id;
				System.Console.WriteLine("Failure! Could not find record with name " + id);
			}

			return Json(results);
		}

		[Route("records/songs/{id}")]
		[HttpGet]
		public JsonResult GetSongs(int id)
		{
			// SELECT musicians.name, records.name, rolls.name FROM musiciantorecord
			// JOIN musicians on musiciantorecord.MusicianId = musicians.MusicianId
			// JOIN rolls on musiciantorecord.RollId= rolls.RollId
			// JOIN records on musiciantorecord.RecordId = records.RecordId
			// WHERE musiciantoRecord.recordid = 1;
			System.Console.WriteLine("Searching for record: " + id);
			MySongsView results = new MySongsView();

			List<Song> songsList = _context.Songs.Where(x => x.RecordId == id).ToList();

			if (songsList.Count() > 0)
			{
				System.Console.WriteLine("Success! Found " + songsList.Count() + " songs!");
				// System.Console.WriteLine("Results: " + results.SongNames.Count());

				results.SongNames = InsertionMethods.ExtractSongs(songsList);

				results.Success = true;
				results.Message = "Success! Returned record: " + id;
				System.Console.WriteLine("Success! Returned record: " + id);
			}
			else
			{
				results.Success = false;
				results.Message = "Failure! Returned no songs for record: " + id;
				System.Console.WriteLine("Failure! Returned no songs for record: " + id);
			}

			return Json(results);
		}

		[Route("records/songs/{id}/ranked")]
		[HttpGet]
		public JsonResult TopSongs(int id)
		{
			// SELECT musicians.name, records.name, rolls.name FROM musiciantorecord
			// JOIN musicians on musiciantorecord.MusicianId = musicians.MusicianId
			// JOIN rolls on musiciantorecord.RollId= rolls.RollId
			// JOIN records on musiciantorecord.RecordId = records.RecordId
			// WHERE musiciantoRecord.recordid = 1;
			System.Console.WriteLine("Searching for record: " + id);
			MySongsView results = new MySongsView();

			List<Song> songsList = _context.Songs.Where(x => x.RecordId == id).ToList();

			if (songsList.Count() > 0)
			{
				System.Console.WriteLine("Success! Found " + songsList.Count() + " songs!");
				System.Console.WriteLine("Results: " + results.SongNames.Count());

				results.SongNames = InsertionMethods.ExtractSongs(songsList);
				results.SongNames = InsertionMethods.WeightSongs(results.SongNames);

				results.SongNames.Sort(
						delegate (Dictionary<string, string> pair1,
						Dictionary<string, string> pair2)
						{
							int parse1 = Int32.Parse(pair1["score"]);
							int parse2 = Int32.Parse(pair2["score"]);
							return parse2.CompareTo(parse1);
						}
				);

				int i = 0;
				foreach (var song in results.SongNames)
				{
					results.holisticScore += Int32.Parse(song["holisticscore"]);
					if (i < 8)
					{
						results.weightedScore += Int32.Parse(song["weightedscore"]);
					}
					else
					{
						i++;
					}
				}

				results.Success = true;
				results.Message = "Success! Returned record: " + id;
				System.Console.WriteLine("Success! Returned record: " + id);
			}
			else
			{
				results.Success = false;
				results.Message = "Failure! Returned no songs for record: " + id;
				System.Console.WriteLine("Failure! Returned no songs for record: " + id);
			}

			return Json(results);
		}

	}
}
