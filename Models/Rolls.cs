using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IntroToEntity.Models
{
	public class Roll
	{
		[Key]
		public int RollId { get; set; }

		[Required]
		public string Name { get; set; }
		// Wether the roll is considered creative, ie producer or songwriter
		[Required]
		public bool Creative { get; set; }
	}
}