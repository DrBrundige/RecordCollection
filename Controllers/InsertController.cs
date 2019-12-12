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
	public class InsertController : Controller
	{
		private Context _context;

		// @todo Rework Add Band, Record, Song, City to return strings

		public InsertController(Context context)
		{
			_context = context;
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
				System.Console.WriteLine("Band insertion errant! ModelState invalid!");
			}
			else if (body.band != null)
			{
				// System.Console.WriteLine(body.band.Name);
				if (InsertionMethods.AddBand(body.band, _context))
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
					if (InsertionMethods.AddBand(thisBand, _context))
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
				System.Console.WriteLine("Record insertion errant! ModelState invalid!");
			}
			else if (body.record != null)
			{
				// System.Console.WriteLine(body.band.Name);
				if (InsertionMethods.AddRecord(body.record, _context))
				{
					results.Success = true;
					results.Message = "Success! Record added!";
					results.RowsUpdated = 1;
				}
				else
				{
					results.Success = false;
					results.Message = "Error! No Records were added!";
					results.RowsUpdated = 0;
				}
			}
			else
			{
				var count = 0;
				foreach (Record thisRecord in body.records)
				{
					if (InsertionMethods.AddRecord(thisRecord, _context))
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
			System.Console.WriteLine("Inserting new song(s)");
			MyResponseView results = new MyResponseView();

			if (body.song == null && body.songs == null)
			{
				results.Success = false;
				results.Message = "Song insertion errant! ModelState invalid!";
				System.Console.WriteLine("Song insertion errant! ModelState invalid!");
			}
			else if (body.song != null)
			{
				// System.Console.WriteLine(body.band.Name);
				if (InsertionMethods.AddSong(body.song, _context))
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
					if (InsertionMethods.AddSong(thisSong, _context))
					{
						count += 1;
					}
				}

				results.Success = true;
				results.RowsUpdated = count;
				results.Message = "Success! " + count + " songs added!";
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
				results.Message = "City insertion errant! ModelState invalid!";
				results.RowsUpdated = 0;
				System.Console.WriteLine("City insertion errant! ModelState invalid!");
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


		[Route("musician")]
		[HttpPost]
		public JsonResult NewMusician([FromBody] MyPostView body)
		{
			MyResponseView results = new MyResponseView();
			try
			{
				if (body.musician is Musician == false && body.musicians is List<Musician> == false)
				{
					results.Success = false;
					results.Message = "Musician insertion errant! ModelState invalid!";
					System.Console.WriteLine("Musician insertion errant! ModelState invalid!");
				}
				else if (body.musician != null)
				{
					// System.Console.WriteLine(body.band.Name);

					if (InsertionMethods.AddMusician(body.musician, _context))
					{
						// runs AddMusician. If successful, Success is set to true
						results.Success = true;
						results.Message = "Success! Musician added!";
						results.RowsUpdated = 1;
					}
					else
					{
						// runs AddMusician. If unsuccessful, Success is set to false
						results.Success = false;
						results.Message = "Error! No musicians were added!";
						results.RowsUpdated = 0;
					}
				}
				else
				{
					var count = 0;
					foreach (Musician thisMusician in body.musicians)
					{
						if (InsertionMethods.AddMusician(thisMusician, _context))
						{
							count += 1;
						}
					}

					results.Success = true;
					results.RowsUpdated = count;
					results.Message = "Success! " + count + " Musicians added!";
				}
			}
			catch (System.Exception)
			{
				results.Success = false;
				results.Message = "Musician insertion errant! An unknowable error has occured!";
				System.Console.WriteLine("Musician insertion errant! An unknowable error has occured!");
				return Json(results);
				throw;
			}

			return Json(results);
		}

		[Route("musician/connect")]
		[HttpPost]
		public JsonResult NewConnection([FromBody] MyPostView body)
		{
			MyResponseView results = new MyResponseView();
			try
			{
				if (body.musicianToRecord is MusicianToRecord == false)
				{
					results.Success = false;
					results.Message = "Musician connection errant! ModelState invalid!";
					System.Console.WriteLine("Musician connection errant! ModelState invalid!");
				}
				else if (body.musicianToRecord != null)
				{
					// System.Console.WriteLine(body.band.Name);
					string message = InsertionMethods.ConnectMusician(body.musicianToRecord, _context);
					if (message.Equals(""))
					{
						// runs AddMusician. If successful, Success is set to true
						results.Success = true;
						results.Message = "Success! Connection added!";
						results.RowsUpdated = 1;
					}
					else
					{
						// runs AddMusician. If unsuccessful, Success is set to false
						results.Success = false;
						results.Message = message;
						results.RowsUpdated = 0;
					}
				}
				// else
				// {
				// 	var count = 0;
				// 	foreach (MusicianToRecord thisMusician in body.mus)
				// 	{
				// 		if (AddMusician(thisMusician))
				// 		{
				// 			count += 1;
				// 		}
				// 	}

				// 	results.Success = true;
				// 	results.RowsUpdated = count;
				// 	results.Message = "Success! " + count + " Musicians added!";
				// }
			}
			catch (System.Exception E)
			{
				results.Success = false;
				results.Message = "Musician connection errant! An unknowable error has occured!";
				System.Console.WriteLine("Musician connection errant! An unknowable error has occured!");
				System.Console.WriteLine(E);
				return Json(results);
				throw;
			}

			return Json(results);
		}

		[Route("record/connect")]
		[HttpPost]
		public JsonResult NewTagConnection([FromBody] MyPostView body)
		{
			System.Console.WriteLine("Connecting new record and tag");
			MyResponseView results = new MyResponseView();
			try
			{
				if (body.TagToRecord is TagToRecord == false)
				{
					results.Success = false;
					results.Message = "Tag connection errant! ModelState invalid!";
					System.Console.WriteLine("Tag connection errant! ModelState invalid!");
				}
				else if (body.TagToRecord != null)
				{
					// System.Console.WriteLine(body.band.Name);
					string message = InsertionMethods.ConnectTag(body.TagToRecord, _context);
					if (message.Equals(""))
					{
						// runs connectTag. If successful, Success is set to true
						results.Success = true;
						results.Message = "Success! Connection added!";
						results.RowsUpdated = 1;
					}
					else
					{
						// runs AddMusician. If unsuccessful, Success is set to false
						results.Success = false;
						results.Message = message;
						results.RowsUpdated = 0;
					}
				}
				else
				{
					results.Success = false;
					results.Message = "Tag connection errant! ModelState invalid!";
					System.Console.WriteLine("Tag connection errant! ModelState invalid!");
				}
			}
			catch (System.Exception E)
			{
				results.Success = false;
				results.Message = "Tag connection errant! An unknowable error has occured!";
				System.Console.WriteLine("Tag connection errant! An unknowable error has occured!");
				System.Console.WriteLine(E);
				return Json(results);
				throw;
			}

			return Json(results);
		}

	}
}
