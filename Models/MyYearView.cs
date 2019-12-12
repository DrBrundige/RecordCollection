using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IntroToEntity.Models
{
	public class MyYearView
	{
		public List<Years> Years { get; set; }
		public string Message { get; set; }
		public bool Success { get; set; }

	}
}