using System;
using System.Collections.Generic;
using System.Linq;
using JVLigaV2.LeagueData;
using JVLigaV2.LeagueData.Services;
using JVLigaV2.Models.Admin;
using LeagueData;
using LeagueData.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace JVLigaV2.Controllers
{
	public class AdminController : Controller
	{

		private readonly LeagueContext _db;
		private readonly HallService _halls;
		private readonly SeasonService _seasons;
		private readonly SignInManager<ApplicationUser> _singInManager;
		private readonly UserManager<ApplicationUser> _userManager;

		public AdminController(HallService halls, SeasonService seasons,LeagueContext db, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
		{
			_halls = halls;
			_seasons = seasons;
			_db = db;
			_singInManager = signInManager;
			_userManager = userManager;
		}
		public IActionResult CreateTeam()
		{
			List<Hall> HallList = _halls.GetAll().ToList();
			List<SelectListItem> Halls = new List<SelectListItem>();
			foreach(var hall in HallList)
			{
				SelectListItem item = new SelectListItem();
				item.Value = hall.Id.ToString();
				item.Text = hall.Name;
				Halls.Add(item);
			}
			ViewBag.Halls = Halls;
			ViewBag.TeamCreated = "";
			return View();
		}

		[HttpPost]
		public IActionResult CreateTeam(TeamIndexModel teamModel)
		{
			var hallForTeam = _halls.GetById(Convert.ToInt32(teamModel.HallId));
			teamModel.Team.Hall = hallForTeam;
			_db.Add(teamModel.Team);
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
			return View();
		}
		public IActionResult CreateHall()
		{
			return View();
		}

		[HttpPost]
		public IActionResult CreateHall(Hall hall)
		{
			_db.Add(hall);
			_db.SaveChanges();
			return View();
		}

		public IActionResult GenerateSeason()
		{
			return View();
		}

		[HttpPost]
		public IActionResult GenerateSeason(SeasonModel model)
		{
			var teams = _db.Teams;
			if (teams.Count() != 14)
			{
				ModelState.AddModelError("Year", "Počet týmů v lize musí být 14!");
				return View(model);
			}
			_seasons.GenerateSeason(model.Year);
			return View();
		}
	}
}