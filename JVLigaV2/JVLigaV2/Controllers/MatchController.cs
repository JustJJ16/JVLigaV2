using System;
using System.Linq;
using JVLigaV2.LeagueData;
using JVLigaV2.LeagueData.Models;
using JVLigaV2.LeagueData.Services;
using JVLigaV2.Models.Match;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JVLigaV2.Controllers
{
	public class MatchController : Controller
	{
		private readonly MatchService _match;
		private readonly TeamService _team;
		private readonly ResultService _result;
		private readonly LeagueContext _db;
		private readonly SignInManager<ApplicationUser> _singInManager;
		private readonly UserManager<ApplicationUser> _userManager;

		public MatchController(MatchService match, TeamService team, ResultService result, LeagueContext db, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
		{
			_match = match;
			_team = team;
			_result = result;
			_db = db;
			_singInManager = signInManager;
			_userManager = userManager;
		}

		public IActionResult Season(int year, bool full)
		{
			SeasonIndexModel model;
			if (full)
			{
				var matches = _match.GetMatchesBySeason(year);
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
					Title = "Sezóna " + year
				};

				ViewBag.Year = year;
			}
			else
			{
				var matches = _match.GetTopMatchesBySeason(year, 10);
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
					Title = "Nadcházejících 10 zápasů pro sezónu " + year
				};

				ViewBag.Year = year;
			}

			return View(model);
		}

		public IActionResult Edit(int id)
		{
			var match = _match.GetById(id);
			var strMatch = new MatchIndexListingModel()
			{
				Date = match.Date,
				HomeTeam = match.HomeTeam.Name,
				GuestTeam = match.GuestTeam.Name,
				Id = match.Id
			};
			MatchEditModel model = new MatchEditModel()
			{
				MatchWithTeams = strMatch,
				Match = match,
				Title = strMatch.HomeTeam + " - " + strMatch.GuestTeam + ", " + strMatch.Date,
				Results = new[] { new Result(){Set = 1}, new Result() { Set = 2}, new Result() { Set = 3} },

			};
			return View(model);
		}

		[HttpPost]
		public IActionResult Edit(MatchEditModel model)
		{
			int homeP = 0;
			int guestP = 0;
			Match match = _match.GetById(model.Match.Id);
			foreach (Result result in model.Results)
			{
				if (result.GuestTeamPoints > result.HomeTeamPoints)
					guestP++;
				if (result.GuestTeamPoints < result.HomeTeamPoints)
					homeP++;

				result.Match = match;

				_result.Add(result);
			}
			match.Winner = homeP > guestP;
			try
			{
				_db.SaveChanges();
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				throw;
			}
			var strMatch = new MatchIndexListingModel()
			{
				Date = match.Date,
				HomeTeam = match.HomeTeam.Name,
				GuestTeam = match.GuestTeam.Name,
				Id = match.Id
			};
			model.MatchWithTeams = strMatch;
			model.Title = strMatch.HomeTeam + " - " + strMatch.GuestTeam + ", " + strMatch.Date;

			ViewBag.Succeed = "Výsledky zadány.";

			return View(model);
		}
	}
}
