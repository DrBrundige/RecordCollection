using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IntroToEntity.Models
{
	public class MySongsView
	{
		// Model returned from an 'all songs in record' request
		public string Message { get; set; }
		public bool Success { get; set; }
		// public Dictionary<string, List<object>> Data { get; set; }
		public int weightedScore { get; set; }
		public int holisticScore { get; set; }
		
		public List<Song> Songs { get; set; }
		public List<Dictionary<string, string>> SongNames { get; set; }


		public MySongsView(){
			SongNames = new List<Dictionary<string, string>>();
		}
	}
}