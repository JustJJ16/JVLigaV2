using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JVLigaV2.Models.Match
{
	public class StandingsModel
	{
		public IEnumerable<StandingsListingModel> StandingListingModel { get; set; }
		public int Season { get; set; }
	}
}
