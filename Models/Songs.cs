using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IntroToEntity.Models
{
	public class Song
	{
		[Key]
		public int SongId { get; set; }

		[Required]
		public string Name { get; set; }

		[Required]
		public int TrackListing { get; set; }

		[Range(0,100)]
		public int Score { get; set; }

		[Required]
		public int RecordId { get; set; }

		[NotMapped]
		public string RecordName { get; set; }

		public Record Record { get; set; }
		
		[NotMapped]
		public string BandName { get; set; }

		public DateTime CreatedAt { get; set; }
		public DateTime UpdatedAt { get; set; }
	}
}