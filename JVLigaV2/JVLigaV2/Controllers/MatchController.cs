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
		private readonly PlayerService _player;
		private readonly ResultService _result;
		private readonly LeagueContext _db;
		private readonly SignInManager<ApplicationUser> _singInManager;
		private readonly UserManager<ApplicationUser> _userManager;

		public MatchController(MatchService match, TeamService team, ResultService result, PlayerService player, LeagueContext db, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
		{
			_match = match;
			_team = team;
			_result = result;
			_player = player;
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
						HomeTeam = _team.GetById(result.HomeTeam.Id),
						GuestTeam = _team.GetById(result.GuestTeam.Id),
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
						HomeTeam = _team.GetById(result.HomeTeam.Id),
						GuestTeam = _team.GetById(result.GuestTeam.Id),
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
				HomeTeam = match.HomeTeam,
				GuestTeam = match.GuestTeam,
				Id = match.Id
			};
			var results = _result.GetResultsForMatch(id).ToList();
			MatchEditModel model = new MatchEditModel()
			{
				MatchWithTeams = strMatch,
				Match = match,
				Title = strMatch.HomeTeam.Name + " - " + strMatch.GuestTeam.Name + ", " + strMatch.Date,
				Results = results

			};
			return View(model);
		}

		[HttpPost]
		public IActionResult Edit(MatchEditModel model)
		{
			if (!ModelState.IsValid) return Redirect("/");
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
				var resultR = _result.GetResultBySetAndMatch(result.Set, result.Match.Id);
				resultR.GuestTeamPoints = result.GuestTeamPoints;
				resultR.HomeTeamPoints = result.HomeTeamPoints;
				_result.UpdateResult(resultR);
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
			return RedirectToAction("Edit","Match");
		}

		public IActionResult Change(int id)
		{
			var model = new MatchChangeModel();
			model.Match = _match.GetById(id);
			model.NewDateTime = _match.GetById(id).Date;
			return View(model);
		}
		[HttpPost]
		public IActionResult Change(MatchChangeModel model)
		{
			if (!ModelState.IsValid) Redirect("/");
			var match = _match.GetById(model.Match.Id);
			match.Date = model.NewDateTime;
			_db.Entry(match).State = EntityState.Modified;
			_db.SaveChanges();
			return RedirectToAction("Season", "Match", new {year = DateTime.Now.Year});
		}


		public IActionResult Detail(int id)
		{
			MatchDetailModel model = new MatchDetailModel();
			model.Match = _match.GetById(id);
			model.HomeTeamPlayers = _player.GetPlayersForTeam(model.Match.HomeTeam);
			model.GuestTeamPlayers = _player.GetPlayersForTeam(model.Match.GuestTeam);
			model.Results = _result.GetResultsForMatch(model.Match.Id);
			return View(model);
		}
	}
}
