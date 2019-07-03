using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IntroToEntity.Models
{
	public class Label
	{
		[Key]
		public int LabelId { get; set; }

		[Required]
		public string Name { get; set; }

		public int CityId { get; set; }

		public City City { get; set; }

		// public List<Band> Bands { get; set; }

		// public List<Record> Records { get; set; }

		public DateTime CreatedAt { get; set; }
		public DateTime UpdatedAt { get; set; }

	}
}