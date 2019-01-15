using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using LeagueData.Migrations;

namespace LeagueData.Models
{
	public class Team
	{
		[Key]
		public int Id { get; set; }
		[Required]
		[MaxLength(50)]
		public string Name { get; set; }
		[Required]
		public Hall Hall { get; set; }
	}
}
