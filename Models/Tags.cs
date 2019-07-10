using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IntroToEntity.Models
{
	public class Tag
	{
		[Key]
		public int TagId { get; set; }

		[Required]
		public string Name { get; set; }

		public List<TagToRecord> TagToRecords { get; set; }
		public int Genre { get; set; }
	}
}