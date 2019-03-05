using System;
using System.ComponentModel.DataAnnotations;

namespace JVLigaV2.Models.Match
{
	public class MatchIndexListingModel
	{
		public int Id { get; set; }
		[Display(Name ="Domácí")]
		public string HomeTeam { get; set; }
		[Display(Name = "Hosté")]
		public string GuestTeam { get; set; }
		[Display(Name = "Datum")]
		public DateTime Date { get; set; }

		public bool Winner { get; set; }
	}
}
