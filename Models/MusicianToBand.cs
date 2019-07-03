using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IntroToEntity.Models
{
	public class MusicianToBand
	{
		[Key]
		public int MusicianToBandId { get; set; }

		[Required]
		public int MusicianId { get; set; }
		[Required]
		public int BandId { get; set; }
		[Required]
		public int RollId { get; set; }

		[Required]
		public Musician Musician { get; set; }
		[Required]
		public Band Band { get; set; }
		[Required]
		public Roll Roll { get; set; }

		public int YearStart { get; set; }
		public int YearEnd { get; set; }

		// Whether the artist is most known for this roll
		public bool Known { get; set; }

	}
}