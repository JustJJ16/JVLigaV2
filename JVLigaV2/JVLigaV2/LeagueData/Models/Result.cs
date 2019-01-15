using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LeagueData.Models
{
	public class Result
	{
		[Key]
		public int Id { get; set; }
		[Required]
		public int Set { get; set; }
		[Display(Name ="Body domácí")]
		public int HomeTeamPoints { get; set; }
		[Display(Name = "Body hosté")]
		public int GuestTeamPoints { get; set; }
		[Required]
		public Match Match { get; set; }
	}
}
