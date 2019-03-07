using System.Collections.Generic;
using JVLigaV2.LeagueData.Models;

namespace JVLigaV2.Models.Match
{
	public class MatchEditModel
	{
		public MatchIndexListingModel MatchWithTeams { get; set; }
		public LeagueData.Models.Match Match { get; set; }
		public List<Result> Results { get; set; }
		public string Title { get; set; }
	}
}
