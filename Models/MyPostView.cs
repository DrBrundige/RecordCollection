using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IntroToEntity.Models
{
	public class MyPostView
	{

		public string chungus { get; set; }
		public List<Band> bands { get; set; }
		public Band band { get; set; }
		public List<Record> records { get; set; }
		public Record record { get; set; }
		public List<Song> songs { get; set; }
		public Song song { get; set; }
		public City city { get; set; }
		public Musician musician { get; set; }

		public List<Musician> musicians { get; set; }

		public MusicianToRecord musicianToRecord { get; set; }
	}
}