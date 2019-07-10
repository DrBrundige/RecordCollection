using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IntroToEntity.Models
{
	public class Record
	{
		[Key]
		public int RecordId { get; set; }

		[Required]
		public string Name { get; set; }

		// Name is what will be stored in the database, while DisplayName will be displayed
		// These are usually the same, and copied over if DisplayName is null
		// DisplayName is stylized. The Beach House album might have a name of 'Seven' and a displayname of '7'
		// Artist Names omit the 'the' where applicable
		public string DisplayName { get; set; }

		[Required]
		public int BandId { get; set; }
		public Band Band { get; set; }

		[NotMapped]
		public string BandName { get; set; }

		public List<Song> Songs { get; set; }
		public List<TagToRecord> TagToRecords { get; set; }

		public int Year { get; set; }

		[Required]
		public int GenreID { get; set; }

		public Genre Genre { get; set; }

		public int LabelId { get; set; }
		// public Label Label { get; set; }
		public int Zeitgeist { get; set; }

		public string Certification { get; set; }

		// [NotMapped]
		// public int NoSongs { get; set; }

		// [NotMapped]
		// public int HolisticScore { get; set; }

		// [NotMapped]
		// public int EightScore { get; set; }

		public DateTime CreatedAt { get; set; }
		public DateTime UpdatedAt { get; set; }

	}
}