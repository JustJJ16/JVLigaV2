using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JVLigaV2.LeagueData.Services;
using JVLigaV2.Models.Match;
using Microsoft.AspNetCore.Mvc;

namespace JVLigaV2.Components
{
	public class UpcomingMatches : ViewComponent
	{
		private readonly MatchService _match;
		private readonly TeamService _team;

		public UpcomingMatches(MatchService match, TeamService team)
		{
			_match = match;
			_team = team;

		}

		public IViewComponentResult Invoke()
		{
			SeasonIndexModel model;
			int currYear = DateTime.Now.Year;
			var matches = _match.GetTopMatchesBySeason(currYear, 10);
			var listingResult = matches
				.Select(result => new MatchIndexListingModel
				{
					Id = result.Id,
					HomeTeam = _team.GetById(result.HomeTeam.Id).Name,
					GuestTeam = _team.GetById(result.GuestTeam.Id).Name,
					Date = result.Date,
					Winner = result.Winner

				});
			model = new SeasonIndexModel()
			{
				Matches = listingResult,
				Title = "Nadcházejících 10 zápasů pro sezónu " + currYear
			};

			ViewBag.Year = currYear;
			return View(model);
		}
	}
}
