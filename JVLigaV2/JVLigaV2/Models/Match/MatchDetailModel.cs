using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JVLigaV2.LeagueData.Models;

namespace JVLigaV2.Models.Match
{
	public class MatchDetailModel
	{
		public LeagueData.Models.Match Match { get; set; }
		public IEnumerable<Player> HomeTeamPlayers { get; set; }
		public IEnumerable<Player> GuestTeamPlayers { get; set; }

	}
}
