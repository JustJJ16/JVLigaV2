using System;
using System.ComponentModel.DataAnnotations;

namespace JVLigaV2.LeagueData.Models
{
	public class Match
	{
		[Key]
		public int Id { get; set; }
		public DateTime Date { get; set; }
		public Team HomeTeam { get; set; }
		public Team GuestTeam { get; set; }
		public bool Winner { get; set; }
	}
}
