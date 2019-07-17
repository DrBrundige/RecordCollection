using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IntroToEntity.Models
{
	public class Band
	{
		[Key]
		public int BandId { get; set; }

		[Required]
		public string Name { get; set; }

		public string DisplayName { get; set; }

		public int CityId { get; set; }
		public City City { get; set; }
		
		[NotMapped]
		public String CityName { get; set; }

		public List<Record> NiceRecords { get; set; }
		public List<MusicianToBand> NiceMembers { get; set; }

		[NotMapped]
		public int NoMembers { get; set; }

		[NotMapped]
		public int NoRecords { get; set; }

		public DateTime CreatedAt { get; set; }
		public DateTime UpdatedAt { get; set; }
	}
}