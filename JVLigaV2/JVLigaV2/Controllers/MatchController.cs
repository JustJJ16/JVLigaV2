using System.Linq;
using JVLiga.Models.Match;
using JVLigaV2.LeagueData;
using JVLigaV2.LeagueData.Services;
using LeagueData;
using LeagueData.Models;
using LeagueServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

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
				foreach (var match in matches)
				{

					_db.Entry(match).Reference(m => m.HomeTeam).Load();
					_db.Entry(match).Reference(m => m.GuestTeam).Load();
				}
				var listingResult = matches
					.Select(result => new MatchIndexListingModel
					{
						Id = result.Id,
						HomeTeam = _team.GetById(result.HomeTeam.Id).Name,
						GuestTeam = _team.GetById(result.GuestTeam.Id).Name,
						Date = result.Date

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
				foreach (var match in matches)
				{

					_db.Entry(match).Reference(m => m.HomeTeam).Load();
					_db.Entry(match).Reference(m => m.GuestTeam).Load();
				}
				var listingResult = matches
					.Select(result => new MatchIndexListingModel
					{
						Id = result.Id,
						HomeTeam = _team.GetById(result.HomeTeam.Id).Name,
						GuestTeam = _team.GetById(result.GuestTeam.Id).Name,
						Date = result.Date

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
			_db.Entry(match).Reference(m => m.HomeTeam).Load();
			_db.Entry(match).Reference(m => m.GuestTeam).Load();
			var strMatch = new MatchIndexListingModel()
			{
				Date = match.Date,
				HomeTeam = _team.GetById(match.HomeTeam.Id).Name,
				GuestTeam = _team.GetById(match.GuestTeam.Id).Name,
				Id = match.Id
			};
			MatchEditModel model = new MatchEditModel()
			{
				Match = strMatch,
				Title = strMatch.HomeTeam + " - " + strMatch.GuestTeam + ", " + strMatch.Date,
				Result1 = new Result() { Set = 1},
				Result2 = new Result() { Set = 2},
				Result3 = new Result() { Set = 3}

			};
			return View(model);
		}

		[HttpPost]
		public IActionResult Edit(MatchEditModel model)
		{
			_result.Add(model.Result1);
			_result.Add(model.Result2);
			_result.Add(model.Result3);

			ViewBag.Succeed = "Výsledky zadány.";

			return View(model);
		}
	}
}
