using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IntroToEntity.Models
{
	public class MyResponseView
	{
		// Model returned from UPDATE or DELETE requests
		public string Message { get; set; }
		public bool Success { get; set; }
		public int RowsUpdated { get; set; }
	}
}