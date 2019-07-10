using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IntroToEntity.Models
{
	public class TagToRecord
	{
		[Key]
		public int TagToRecordId { get; set; }

		[Required]
		public int TagId { get; set; }
		[Required]
		public int RecordId { get; set; }

		[NotMapped]
		public string TagName { get; set; }
		[NotMapped]
		public string RecordName { get; set; }

		// public Tag Tag { get; set; }
		// public Record Record { get; set; }
	}
}