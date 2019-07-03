using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IntroToEntity.Models
{
	public class City
	{
		[Key]
		public int CityId { get; set; }

		[Required]
		public string Name { get; set; }

		public string State { get; set; }

		[Required]
		public string Country { get; set; }

		public DateTime CreatedAt { get; set; }
		public DateTime UpdatedAt { get; set; }
	}
}