using System;
using System.ComponentModel.DataAnnotations;
using JVLigaV2.LeagueData.Models;

namespace JVLigaV2.Models.Match
{
	public class MatchIndexListingModel
	{
		public int Id { get; set; }
		[Display(Name ="Domácí")]
		public LeagueData.Models.Team HomeTeam { get; set; }
		[Display(Name = "Hosté")]
		public LeagueData.Models.Team GuestTeam { get; set; }
		[Display(Name = "Datum")]
		public DateTime Date { get; set; }

		public bool Winner { get; set; }
	}
}
