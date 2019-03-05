using LeagueData.Models;

namespace JVLigaV2.Models.Match
{
	public class MatchEditModel
	{
		public MatchIndexListingModel MatchWithTeams { get; set; }
		public global::LeagueData.Models.Match Match { get; set; }
		public Result[] Results { get; set; }
		public string Title { get; set; }
	}
}
