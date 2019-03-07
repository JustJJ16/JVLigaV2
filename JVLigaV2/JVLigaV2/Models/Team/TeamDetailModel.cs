using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JVLigaV2.LeagueData.Models;

namespace JVLigaV2.Models.Team
{
	public class TeamDetailModel
	{
		public LeagueData.Models.Team Team { get; set; }
		public IEnumerable<LeagueData.Models.Player> Players { get; set; }
	}
}
