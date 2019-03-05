using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JVLigaV2.LeagueData.Models;
using JVLigaV2.LeagueData.Services;
using JVLigaV2.Models.Match;
using Microsoft.AspNetCore.Mvc;

namespace JVLigaV2.Components
{
	public class Standings : ViewComponent
	{
		private readonly MatchService _match;

		public Standings(MatchService match)
		{
			_match = match;
		}
		public IViewComponentResult Invoke()
		{
			var currSeasonMatches = _match.GetMatchesBySeason(DateTime.Now.Year).Where(m => m.Date < DateTime.Now).ToList();
			IEnumerable<Team> teams = currSeasonMatches.Select(m => m.HomeTeam).Distinct();
			StandingsModel standingsModel = new StandingsModel();
			List<StandingsListingModel> standings = new List<StandingsListingModel>();
			foreach (var team in teams)
			{
				int wins = currSeasonMatches.Count(t => (t.HomeTeam.Id == team.Id && t.Winner) || (t.GuestTeam.Id == team.Id && !t.Winner));
				int numOfMatches = currSeasonMatches.Count(t => (t.HomeTeam.Id == team.Id || t.GuestTeam.Id == team.Id));
				int losses = currSeasonMatches.Count(t => (t.HomeTeam.Id == team.Id && !t.Winner) || (t.GuestTeam.Id == team.Id && t.Winner));
				string teamName = team.Name;
				int order = 0;
				StandingsListingModel model = new StandingsListingModel()
				{
					Losses = losses,
					Wins = wins,
					TeamName = teamName,
					NumOfMatches = numOfMatches,
					Order = order,
					TeamId = team.Id
				};
				standings.Add(model);
			}

			standings = standings.OrderByDescending(s => s.Wins).ToList();
			for (int i = 0; i < standings.Count(); i++)
			{
				standings.ToList()[i].Order = i+1;
			}

			standingsModel.Season = DateTime.Now.Year;
			standingsModel.StandingListingModel = standings;
			return View(standingsModel);
		}
	}
}
