using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nager.Date.PublicHolidays;

namespace JVLigaV2.Models.Match
{
	public class StandingsListingModel
	{
		public int Order { get; set; }
		public string TeamName { get; set; }
		public int TeamId { get; set; }
		public int NumOfMatches { get; set; }
		public int Wins { get; set; }
		public int Losses { get; set; }

	}
}
