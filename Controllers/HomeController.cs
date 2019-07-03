using System;
using System.Collections.Generic;
// using System.Diagnostics;
using System.Linq;
// using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using IntroToEntity.Models;
using Microsoft.EntityFrameworkCore;
// using Microsoft.AspNetCore.Identity;
// using Microsoft.AspNetCore.Http;

namespace IntroToEntity.Controllers
{
	public class HomeController : Controller
	{
		private Context _context;

		public bool AddBand(Band thisBand)
		{
			System.Console.WriteLine("Adding: " + thisBand.Name);

			Band exists = _context.Bands.FirstOrDefault(x => x.Name == thisBand.Name);

			if (exists == null)
			{
				if (thisBand.CityName != null)
				{
					thisBand.City = _context.Cities.FirstOrDefault(x => x.Name == thisBand.CityName);
				}
				else
				{
					thisBand.CityId = 22;
				}
				// System.Console.WriteLine(body.band.CityId);
				thisBand.CreatedAt = DateTime.Now;
				thisBand.UpdatedAt = DateTime.Now;

				try
				{
					_context.Bands.Add(thisBand);
					_context.SaveChanges();
				}
				catch (System.Exception)
				{
					_context.Bands.Remove(thisBand);
					System.Console.WriteLine("Error! Attempt to add band " + thisBand.Name + " Failed!");
					return false;
					throw;
				}

				return true;
			}
			else
			{
				// How to return whether band already exists / vs other error
				System.Console.WriteLine("Error! Band " + thisBand.Name + " already exists!");
				return false;
			}
		}

		public bool AddRecord(Record thisRecord)
		{
			System.Console.WriteLine("Adding record: " + thisRecord.Name);

			Record exists = _context.Records.FirstOrDefault(x => x.Name == thisRecord.Name);

			if (exists == null)
			{
				thisRecord.Band = _context.Bands.FirstOrDefault(x => x.Name == thisRecord.BandName);
				if (thisRecord.DisplayName == "")
				{
					thisRecord.DisplayName = null;
				}

				// System.Console.WriteLine(body.band.CityId);
				thisRecord.CreatedAt = DateTime.Now;
				thisRecord.UpdatedAt = DateTime.Now;

				try
				{
					_context.Records.Add(thisRecord);
					_context.SaveChanges();
				}
				catch (System.Exception)
				{
					_context.Records.Remove(thisRecord);
					System.Console.WriteLine("Error! Attempt to add record " + thisRecord.Name + " Failed!");
					return false;
					throw;
				}

				return true;
			}
			else
			{
				// How to return whether band already exists / vs other error
				System.Console.WriteLine("Error! Record " + thisRecord.Name + " already exists!");
				return false;
			}
		}

		public bool AddSong(Song thisSong)
		{
			System.Console.WriteLine("Adding record: " + thisSong.Name);

			Record exists = _context.Records.FirstOrDefault(x => x.Name == thisSong.Name);

			if (exists == null)
			{
				if (thisSong.RecordName != null)
				{
					thisSong.Record = _context.Records.FirstOrDefault(x => x.Name == thisSong.RecordName);
					// System.Console.WriteLine(thisSong.Record.Name);
				}
				// System.Console.WriteLine(thisSong.RecordId);

				// System.Console.WriteLine(body.band.CityId);
				thisSong.CreatedAt = DateTime.Now;
				thisSong.UpdatedAt = DateTime.Now;

				try
				{
					_context.Songs.Add(thisSong);
					_context.SaveChanges();
				}
				catch (System.Exception)
				{
					_context.Songs.Remove(thisSong);
					System.Console.WriteLine("Error! Attempt to add record " + thisSong.Name + " Failed!");
					return false;
					throw;
				}

				return true;
			}
			else
			{
				// How to return whether song already exists / vs other error
				System.Console.WriteLine("Error! Song " + thisSong.Name + " already exists!");
				return false;
			}
		}


		public List<Dictionary<string, string>> ExtractSongs(List<Song> songsList)
		{
			List<Dictionary<string, string>> SongNames = new List<Dictionary<string, string>>();

			foreach (Song song in songsList)
			{
				Dictionary<string, string> niceSong = new Dictionary<string, string>();
				niceSong.Add("name", song.Name);
				niceSong.Add("score", (song.Score).ToString());
				niceSong.Add("tracklisting", song.TrackListing.ToString());
				// System.Console.WriteLine(niceSong["name"]);
				SongNames.Add(niceSong);
			}

			return SongNames;
		}

		public List<Dictionary<string, string>> WeightSongs(List<Dictionary<string, string>> SongNames)
		{
			foreach (Dictionary<string, string> song in SongNames)
			{
				int score = Int32.Parse(song["score"]);

				int[] hgrades = { 0, 50, 60, 70, 75, 80, 85 };
				string[] hweights = { "0", "20", "40", "70", "80", "120" };

				int[] wgrades = { 0, 60, 70, 75, 80, 85, 90, 95, 100 };
				string[] wweights = { "0", "10", "50", "70", "90", "150", "200", "225", "250" };

				string wScore = wweights[wweights.Length - 1];
				string hScore = hweights[hweights.Length - 1];

				int i = 1;
				while (i < wgrades.Length)
				{
					if (score >= wgrades[i - 1] && score < wgrades[i])
					{
						wScore = wweights[i - 1];
						i = wweights.Length;
					}
					i++;
				}
				i = 1;
				while (i < hgrades.Length)
				{
					if (score >= hgrades[i - 1] && score < hgrades[i])
					{
						hScore = hweights[i - 1];
						i = hweights.Length;
					}
					i++;
				}

				song.Add("weightedscore", wScore);
				song.Add("holisticscore", hScore);
			}

			return SongNames;
		}


		public HomeController(Context context)
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
			MySongsView results = new MySongsView();

			results.Songs = _context.Songs.ToList();
			results.Success = true;
			results.Message = "Success! Returned some songs";

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

				results.SongNames = ExtractSongs(songsList);

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

				results.SongNames = ExtractSongs(songsList);
				results.SongNames = WeightSongs(results.SongNames);

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
					if(i < 8){
						results.weightedScore += Int32.Parse(song["weightedscore"]);
					} else {
						i ++;
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


		[Route("band")]
		[HttpPost]
		public JsonResult NewBands([FromBody] MyPostView body)
		{
			MyResponseView results = new MyResponseView();

			if (body.band == null && body.bands == null)
			{
				results.Success = false;
				results.Message = "Error! ModelState invalid!";
				System.Console.WriteLine("Error! ModelState invalid!");
			}
			else if (body.band != null)
			{
				// System.Console.WriteLine(body.band.Name);
				if (AddBand(body.band))
				{
					results.Success = true;
					results.Message = "Success! Band added!";
					results.RowsUpdated = 1;
				}
				else
				{
					results.Success = false;
					results.Message = "Error! No bands were added!";
					results.RowsUpdated = 0;
				}
			}
			else
			{
				var count = 0;
				foreach (Band thisBand in body.bands)
				{
					if (AddBand(thisBand))
					{
						count += 1;
					}
				}
				// _context.SaveChanges();

				results.Success = true;
				results.RowsUpdated = count;
				results.Message = "Success! " + count + " Bands added!";
			}

			return Json(results);
		}

		[Route("record")]
		[HttpPost]
		public JsonResult NewRecords([FromBody] MyPostView body)
		{
			MyResponseView results = new MyResponseView();

			if (body.record == null && body.records == null)
			{
				results.Success = false;
				results.Message = "Error! ModelState invalid!";
				System.Console.WriteLine("Error! ModelState invalid!");
			}
			else if (body.record != null)
			{
				// System.Console.WriteLine(body.band.Name);
				if (AddRecord(body.record))
				{
					results.Success = true;
					results.Message = "Success! Record added!";
					results.RowsUpdated = 1;
				}
				else
				{
					results.Success = false;
					results.Message = "Error! No Record were added!";
					results.RowsUpdated = 0;
				}
			}
			else
			{
				var count = 0;
				foreach (Record thisRecord in body.records)
				{
					if (AddRecord(thisRecord))
					{
						count += 1;
					}
				}
				// _context.SaveChanges();

				results.Success = true;
				results.RowsUpdated = count;
				results.Message = "Success! " + count + " Records added!";
			}

			return Json(results);
		}

		[Route("song")]
		[HttpPost]
		public JsonResult NewSongs([FromBody] MyPostView body)
		{
			MyResponseView results = new MyResponseView();

			if (body.song == null && body.songs == null)
			{
				results.Success = false;
				results.Message = "Error! ModelState invalid!";
				System.Console.WriteLine("Error! ModelState invalid!");
			}
			else if (body.song != null)
			{
				// System.Console.WriteLine(body.band.Name);
				if (AddSong(body.song))
				{
					results.Success = true;
					results.Message = "Success! Song added!";
					results.RowsUpdated = 1;
				}
				else
				{
					results.Success = false;
					results.Message = "Error! No songs were added!";
					results.RowsUpdated = 0;
				}
			}
			else
			{
				var count = 0;
				foreach (Song thisSong in body.songs)
				{
					if (AddSong(thisSong))
					{
						count += 1;
					}
				}

				results.Success = true;
				results.RowsUpdated = count;
				results.Message = "Success! " + count + " Records added!";
			}

			return Json(results);
		}

		[Route("city")]
		[HttpPost]
		public JsonResult NewCity([FromBody] MyPostView body)
		{
			MyResponseView results = new MyResponseView();

			if (body.city == null)
			{
				results.Success = false;
				results.Message = "Error! ModelState invalid!";
				results.RowsUpdated = 0;
				System.Console.WriteLine("Error! ModelState invalid!");
			}
			else
			{
				System.Console.WriteLine("Adding: " + body.city.Name);
				body.city.CreatedAt = DateTime.Now;
				body.city.UpdatedAt = DateTime.Now;

				try
				{
					_context.Cities.Add(body.city);
					_context.SaveChanges();
				}
				catch (System.Exception)
				{
					_context.Cities.Remove(body.city);
					System.Console.WriteLine("Error! Attempt to add city " + body.city.Name + " Failed!");
					results.Success = false;
					results.Message = "Error! Attempt to add city " + body.city.Name + " Failed!";
					results.RowsUpdated = 0;
					return Json(results);
				}

				results.Success = true;
				results.Message = "Success! City added!";
				results.RowsUpdated = 1;
			}

			return Json(results);
		}

	}
}
