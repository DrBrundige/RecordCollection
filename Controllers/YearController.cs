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
	public class YearController : Controller
	{
		private Context _context;

		// @todo Rework Add Band, Record, Song, City to return strings

		public YearController(Context context)
		{
			_context = context;
		}

		[Route("years/{year}")]
		[HttpGet]
		public JsonResult AllYears(int year)
		{
			System.Console.WriteLine("Returning records with year " + year);

			MyAllRecordsView results = new MyAllRecordsView();
			List<Record> Records = _context.Records.Where(x => x.Year == year).ToList();

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
			results.Message = "Success! Returned some records with year " + year;

			return Json(results);
		}
	}
}
