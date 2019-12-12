using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IntroToEntity.Models
{
	public class MyAllRecordsView
	{
		
		public string Message { get; set; }
		public bool Success { get; set; }
		// public Dictionary<string, List<object>> Data { get; set; }
		public List<Dictionary<string, object>> Data { get; set; }
		public int Rows { get; set; }

		public MyAllRecordsView(){
			Data = new List<Dictionary<string, object>>();
		}
	}
}