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

		[Route("")]
		[HttpGet]
		public IActionResult Index()
		{
			return View();
		}

		[Route("songs")]
		[HttpGet]
		public JsonResult AllSongs()
		{
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

		[Route("musicians")]
		[HttpGet]
		public JsonResult AllMusicians()
		{
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


		[Route("records/info/{name}")]
		[HttpGet]
		public JsonResult GetRecord(string name)
		{
			System.Console.WriteLine("Searching for record: " + name);
			MyRecordsView results = new MyRecordsView();

			Record record = _context.Records.Include(x => x.Band).ThenInclude(x => x.City).FirstOrDefault(x => x.Name == name);
			if (record != null)
			{
				results.Record = record;
				// results.Songs = _context.Songs.Where(x => x.RecordId == record.RecordId).OrderBy(x => x.TrackListing).ToList();
				// record.NoSongs = results.Songs.Count();

				// results.Band = _context.Bands.Include(x => x.City).FirstOrDefault(x => x.BandId == record.BandId);
				// results.Label = _context.Labels.FirstOrDefault(x => x.LabelId == record.LabelId);

				// IEnumerable<MusicianToRecord> connections = _context.MusicianToRecord.Where(x => x.RecordId == record.RecordId);
				// foreach (MusicianToRecord connection in connections)
				// {
				// 	System.Console.WriteLine(connection.MusicianId);
				// 	results.Musicians.Add(_context.Musicians.FirstOrDefault(x => x.MusicianId == connection.MusicianId));
				// }
				// SELECT musicians.name, records.name, rolls.name FROM musiciantorecord
				// JOIN musicians on musiciantorecord.MusicianId = musicians.MusicianId
				// JOIN rolls on musiciantorecord.RollId= rolls.RollId
				// JOIN records on musiciantorecord.RecordId = records.RecordId
				// WHERE musiciantoRecord.recordid = 1;

				results.Success = true;
				results.Message = "Success! Returned record: " + name;
				System.Console.WriteLine("Success! Returned record: " + name);
			}
			else
			{
				results.Success = false;
				results.Message = "Failure! Could not find record with name " + name;
				System.Console.WriteLine("Failure! Could not find record with name " + name);
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
