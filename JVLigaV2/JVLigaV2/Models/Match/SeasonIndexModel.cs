using System.Collections.Generic;

namespace JVLigaV2.Models.Match
{
	public class SeasonIndexModel
	{
		public IEnumerable<MatchIndexListingModel> Matches { get; set; }
		public string Title { get; set; }
	}
}
