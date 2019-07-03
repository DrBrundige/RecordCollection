using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IntroToEntity.Models
{
	public class Musician
	{
		[Key]
		public int MusicianId { get; set; }

		[Required]
		public string Name { get; set; }

		[NotMapped]
		public List<string> Rolls { get; set; }

		public DateTime CreatedAt { get; set; }
		public DateTime UpdatedAt { get; set; }
	}
}