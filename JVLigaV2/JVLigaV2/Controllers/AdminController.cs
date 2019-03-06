using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JVLigaV2.LeagueData;
using JVLigaV2.LeagueData.Models;
using JVLigaV2.LeagueData.Services;
using JVLigaV2.Models.Admin;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace JVLigaV2.Controllers
{
	public class AdminController : Controller
	{

		private readonly LeagueContext _db;
		private readonly HallService _halls;
		private readonly SeasonService _seasons;
		private readonly TeamService _team;

		public AdminController(HallService halls, SeasonService seasons, TeamService team, LeagueContext db)
		{
			_halls = halls;
			_seasons = seasons;
			_team = team;
			_db = db;
		}
		public IActionResult TeamManagement()
		{
				if (!CheckAdminRights())
				return Redirect("/");
			List<Hall> hallList = _halls.GetAll().ToList();
			List<SelectListItem> halls = new List<SelectListItem>();
			foreach (var hall in hallList)
			{
				SelectListItem item = new SelectListItem();
				item.Value = hall.Id.ToString();
				item.Text = hall.Name;
				halls.Add(item);
			}
			ViewBag.Halls = halls;
			ViewBag.TeamCreated = string.Empty;
			TeamManagementModel model = new TeamManagementModel();
			model.Teams = _team.GetAll();
			return View(model);
		}

		[HttpPost]
		public IActionResult TeamManagement(TeamManagementModel teamModel)
		{
			if (ModelState.IsValid)
			{
				if (!CheckAdminRights())
					return Redirect("/");
				Team team = new Team();
				var hallForTeam = _halls.GetById(Convert.ToInt32(teamModel.HallId));
				team.Hall = hallForTeam;
				team.Name = teamModel.Name;
				_db.Add(team);
				_db.SaveChanges();

				ViewBag.TeamCreated = "Tým vytvořen";

				List<Hall> HallList = _halls.GetAll().ToList();
				List<SelectListItem> Halls = new List<SelectListItem>();
				foreach (var hall in HallList)
				{
					SelectListItem item = new SelectListItem();
					item.Value = hall.Id.ToString();
					item.Text = hall.Name;
					Halls.Add(item);
				}
				ViewBag.Halls = Halls;
				TeamManagementModel model = new TeamManagementModel();
				model.Teams = _team.GetAll();
				return View(model);
			}

			return Redirect("/");
		}

		//[HttpPost]
		public async Task<IActionResult> DeleteTeam(int id)
		{
			Team team = _team.GetById(id);
			_db.Teams.Remove(team);
			await _db.SaveChangesAsync();
			return Redirect("/Admin/TeamManagement");
		}

		public IActionResult HallManagement()
		{
			if (!CheckAdminRights())
				return Redirect("/");

			ViewBag.HallCreated = string.Empty;
			return View();
		}

		[HttpPost]
		public IActionResult HallManagement(Hall hall)
		{
			if (ModelState.IsValid)
			{
				_db.Add(hall);
				_db.SaveChanges();
				ViewBag.HallCreated = "Hala přidána";
				return View();
			}

			return Redirect("/");
		}

		public IActionResult SeasonManagement()
		{
			if (!CheckAdminRights())
				return Redirect("/");
			ViewBag.SeasonGenerated = string.Empty;
			SeasonModel model = new SeasonModel();
			model.Years = _seasons.GetAvailableSeasons();
			return View(model);
		}

		[HttpPost]
		public IActionResult SeasonManagement(SeasonModel model)
		{
			if (ModelState.IsValid)
			{
				var teams = _db.Teams;
				if (teams.Count() != 14)
				{
					ModelState.AddModelError("Year", "Počet týmů v lize musí být 14!");
					return View(model);
				}

				_seasons.GenerateSeason(model.Year);
				ViewBag.SeasonGenerated = "Sezóna vytvořena";
				model.Years = _seasons.GetAvailableSeasons();
				return View(model);
			}

			return Redirect("/");
		}

		public bool CheckAdminRights()
		{
			return User.IsInRole("Admin");
		}
	}
}