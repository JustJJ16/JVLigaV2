using LeagueData.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JVLiga.Models.Match
{
	public class MatchEditModel
	{
		public MatchIndexListingModel Match { get; set; }
		public Result Result1 { get; set; }
		public Result Result2 { get; set; }
		public Result Result3 { get; set; }
		public string Title { get; set; }
	}
}
