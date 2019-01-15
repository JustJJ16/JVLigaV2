using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JVLiga.Models.Match
{
	public class SeasonIndexModel
	{
		public IEnumerable<MatchIndexListingModel> Matches { get; set; }
		public string Title { get; set; }
	}
}
