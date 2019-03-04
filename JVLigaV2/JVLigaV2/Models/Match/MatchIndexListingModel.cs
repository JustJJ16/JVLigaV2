using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JVLiga.Models.Match
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
