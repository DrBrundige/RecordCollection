using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IntroToEntity.Models
{
	public class Genre
	{
		[Key]
		public int GenreId { get; set; }

		[Required]
		public string Name { get; set; }

		[Required]
		public int Domain { get; set; }
	}
}