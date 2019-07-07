using System;
using System.Collections.Generic;
using System.Linq;
using IntroToEntity.Models;

namespace IntroToEntity.Controllers
{
	public class InsertionMethods
	{

		public static bool AddBand(Band thisBand, Context _context)
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

		public static bool AddRecord(Record thisRecord, Context _context)
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

		public static bool AddSong(Song thisSong, Context _context)
		{
			System.Console.WriteLine("Adding record: " + thisSong.Name);

			Record exists = _context.Records.FirstOrDefault(x => x.Name == thisSong.Name);

			if (exists == null)
			{
				// Assigns record to the song if RecordName is provided
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

		public static bool AddMusician(Musician thisMusician, Context _context)
		{
			System.Console.WriteLine("Adding musician: " + thisMusician.Name);

			Musician exists = _context.Musicians.FirstOrDefault(x => x.Name == thisMusician.Name);

			if (exists == null)
			{

				// System.Console.WriteLine(thisSong.RecordId);

				// System.Console.WriteLine(body.band.CityId);
				thisMusician.CreatedAt = DateTime.Now;
				thisMusician.UpdatedAt = DateTime.Now;

				try
				{
					_context.Musicians.Add(thisMusician);
					_context.SaveChanges();
				}
				catch (System.Exception)
				{
					_context.Musicians.Remove(thisMusician);
					System.Console.WriteLine("Error! Attempt to add musician " + thisMusician.Name + " Failed!");
					return false;
					throw;
				}

				return true;
			}
			else
			{
				// How to return whether song already exists / vs other error
				System.Console.WriteLine("Error! Song " + thisMusician.Name + " already exists!");
				return false;
			}
		}

		public static string ConnectMusician(MusicianToRecord thisConnection, Context _context)
		{
			System.Console.WriteLine("Connecting musician: " + thisConnection.MusicianId + " to record " + thisConnection.RecordId);

			// Checks to make sure each foreign key points to an existing row.
			// In entity, even if an unsuccessful database insertion is enclosed in a TRY block
			// it will still crash, so it is important to make sure this doesn't happen
			Musician musicianExists = _context.Musicians.FirstOrDefault(x => x.MusicianId == thisConnection.MusicianId);
			if (musicianExists == null)
			{
				string message = "Error! Attempt to add musician " + thisConnection.MusicianId + " Failed! Musician with ID " + thisConnection.MusicianId + " does not exist!";
				System.Console.WriteLine(message);
				return message;
			}
			Record recordExists = _context.Records.FirstOrDefault(x => x.RecordId == thisConnection.RecordId);
			if (recordExists == null)
			{
				string message = "Error! Attempt to connect musician " + thisConnection.MusicianId + " Failed! Record with ID " + thisConnection.RecordId + " does not exist!";
				System.Console.WriteLine(message);
				return message;
			}
			Roll rollExists = _context.Rolls.FirstOrDefault(x => x.RollId == thisConnection.RollId);
			if (rollExists == null)
			{
				string message = "Error! Attempt to connect musician " + thisConnection.MusicianId + " Failed! Roll with ID " + thisConnection.RollId + " does not exist!";
				System.Console.WriteLine(message);
				return message;
			}

			IEnumerable<MusicianToRecord> connectionExists = _context.MusicianToRecord.Where(x => x.MusicianId == thisConnection.MusicianId).Where(x => x.RecordId == thisConnection.RecordId).Where(x => x.RollId == thisConnection.RollId);
			if (connectionExists.Count() == 0)
			{
				string message = "Error! Attempt to connect Musician " + thisConnection.MusicianId + " failed! Connection already exists!";
				System.Console.WriteLine(message);
				return message;
			}

			try
			{
				_context.MusicianToRecord.Add(thisConnection);
				_context.SaveChanges();
			}
			catch (System.Exception)
			{
				_context.MusicianToRecord.Remove(thisConnection);
				string message = "Error! Attempt to connect musician " + thisConnection.MusicianId + " Failed!";
				System.Console.WriteLine(message);
				return message;
				throw;
			}

			return "";
		}


		public static List<Dictionary<string, string>> ExtractSongs(List<Song> songsList)
		{
			List<Dictionary<string, string>> SongNames = new List<Dictionary<string, string>>();

			foreach (Song song in songsList)
			{
				Dictionary<string, string> niceSong = new Dictionary<string, string>();
				niceSong.Add("ID", song.SongId.ToString());
				niceSong.Add("name", song.Name);
				niceSong.Add("score", (song.Score).ToString());
				niceSong.Add("tracklisting", song.TrackListing.ToString());
				// System.Console.WriteLine(niceSong["name"]);
				SongNames.Add(niceSong);
			}

			return SongNames;
		}

		public static List<Dictionary<string, string>> WeightSongs(List<Dictionary<string, string>> SongNames)
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

	}

}