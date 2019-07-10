using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IntroToEntity.Models
{
	public class MyRecordsView
	{
		
		public string Message { get; set; }
		public bool Success { get; set; }
		public Record Record { get; set; }
		public Band Band { get; set; }
		// public List<Song> Songs { get; set; }
		public Label Label { get; set; }

		public List<Musician> Musicians { get; set; }
		public List<TagToRecord> TagToRecords { get; set; }

	}
}