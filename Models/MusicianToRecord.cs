using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IntroToEntity.Models
{
	public class MusicianToRecord
	{
		[Key]
		public int MusicianToRecordId { get; set; }

		[Required]
		public int MusicianId { get; set; }
		[Required]
		public int RecordId { get; set; }
		[Required]
		public int RollId { get; set; }

		[NotMapped]
		public string MusicianName { get; set; }
		[NotMapped]
		public string RecordName { get; set; }
		[NotMapped]
		public string RollName { get; set; }

		// public Musician Musician { get; set; }
		public Record Record { get; set; }
		public Roll Roll { get; set; }
	}
}