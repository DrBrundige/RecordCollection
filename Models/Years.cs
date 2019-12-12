using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IntroToEntity.Models
{
	public class Years
	{
		[NotMapped]
		public string Year { get; set; }
		public List<Record> Records { get; set; }
	}
}