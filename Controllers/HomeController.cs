using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using IntroToEntity.Models;

using System.Linq;
// using System.Diagnostics;
// using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
// using Microsoft.AspNetCore.Identity;
// using Microsoft.AspNetCore.Http;

namespace IntroToEntity.Controllers
{
	public class HomeController : Controller
	{
		private Context _context;

		public HomeController(Context context)
		{
			_context = context;
		}

		[Route("recordcollection")]
		[HttpGet]
		public IActionResult Index()
		{
			// System.Console.WriteLine("Big chungus.");
			return View();
		}

		[Route("recordcollection/records")]
		[HttpGet]
		public IActionResult AllRecords()
		{
			MyPostView bigChungus = new MyPostView();
			bigChungus.records = _context.Records.Where(x => x.BandId > 0).OrderBy(x => x.RecordId).Include(x => x.Band).ToList();

			bigChungus.records = parseRecordDisplayNames(bigChungus.records);

			return View("AllRecords", bigChungus);
		}

		[Route("recordcollection/bands")]
		[HttpGet]
		public IActionResult AllBands()
		{
			MyPostView bigChungus = new MyPostView();
			bigChungus.bands = _context.Bands.Where(x => x.BandId > 0).OrderBy(x => x.BandId).Include(x => x.City).ToList();

			bigChungus.bands = parseBandDisplayNames(bigChungus.bands);

			return View("AllBands", bigChungus);
		}

		[Route("recordcollection/bands/{id}")]
		[HttpGet]
		public IActionResult ShowBand(int id)
		{
			System.Console.WriteLine("Displaying band with id: " + id);
			MyPostView bigChungus = new MyPostView();
			bigChungus.band = _context.Bands.Include(x => x.City).FirstOrDefault(x => x.BandId == id);
			if (bigChungus.band == null)
			{
				bigChungus.band = _context.Bands.Include(x => x.City).FirstOrDefault(x => x.BandId == 0);
			}

			bigChungus.band.NiceRecords = _context.Records.Where(x => x.BandId == id).OrderBy(x => x.Year).ToList();

			return View("ShowBand", bigChungus);
		}

		[Route("recordcollection/records/{id}")]
		[HttpGet]
		public IActionResult ShowRecord(int id)
		{
			System.Console.WriteLine("Displaying record with id: " + id);
			MyPostView bigChungus = new MyPostView();
			bigChungus.record = _context.Records.Include(x => x.Songs).Include(x => x.Band).FirstOrDefault(x => x.RecordId == id);
			if (bigChungus.record == null)
			{
				bigChungus.record = _context.Records.Include(x => x.Songs).FirstOrDefault(x => x.RecordId == 0);
			}
			if (bigChungus.record.DisplayName == null)
			{
				bigChungus.record.DisplayName = bigChungus.record.Name;
			}
			if (bigChungus.record.Band.Name == null)
			{
				bigChungus.record.Band.DisplayName = bigChungus.record.Band.Name;
			}

			return View("ShowRecord", bigChungus);
		}

		public List<Record> parseRecordDisplayNames(List<Record> records)
		{
			foreach (Record record in records)
			{
				if (record.DisplayName == null)
				{
					record.DisplayName = record.Name;
				}
				if (record.Band.DisplayName == null)
				{
					record.Band.DisplayName = record.Band.Name;
				}
			}
			return records;
		}

		public List<Band> parseBandDisplayNames(List<Band> bands)
		{
			foreach (Band band in bands)
			{
				if (band.DisplayName == null)
				{
					band.DisplayName = band.Name;
				}
			}
			return bands;
		}

	}
}
