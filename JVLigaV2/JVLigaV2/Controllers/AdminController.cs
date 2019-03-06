﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JVLigaV2.LeagueData;
using JVLigaV2.LeagueData.Models;
using JVLigaV2.LeagueData.Services;
using JVLigaV2.Models.Admin;
using JVLigaV2.Models.Halls;
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
		private readonly MatchService _match;
		private readonly PlayerService _player;

		public AdminController(HallService halls, SeasonService seasons, TeamService team, MatchService match, PlayerService player, LeagueContext db)
		{
			_halls = halls;
			_seasons = seasons;
			_team = team;
			_db = db;
			_match = match;
			_player = player;
		}
		public IActionResult TeamManagement()
		{
				if (!CheckAdminRights())
				return Redirect("/");
			var hallList = _halls.GetAll().ToList();
			var halls = new List<SelectListItem>();
			foreach (var hall in hallList)
			{
				var item = new SelectListItem
				{
					Value = hall.Id.ToString(),
					Text = hall.Name
				};
				halls.Add(item);
			}
			ViewBag.Halls = halls;
			ViewBag.TeamCreated = string.Empty;
			var model = new TeamManagementModel {Teams = _team.GetAll()};
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

				var hallList = _halls.GetAll().ToList();
				var halls = new List<SelectListItem>();
				foreach (var hall in hallList)
				{
					var item = new SelectListItem
					{
						Value = hall.Id.ToString(),
						Text = hall.Name
					};
					halls.Add(item);
				}
				ViewBag.Halls = halls;
				var model = new TeamManagementModel {Teams = _team.GetAll()};
				return View(model);
			}

			return Redirect("/");
		}
		
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
			var model = new HallManagementModel {Halls = _halls.GetAll()};
			return View(model);
		}

		[HttpPost]
		public IActionResult HallManagement(HallManagementModel model)
		{
			if (ModelState.IsValid)
			{
				var hall = new Hall {Name = model.HallName};
				_db.Add(hall);
				_db.SaveChanges();
				ViewBag.HallCreated = "Hala přidána";
				model.Halls = _halls.GetAll();
				return View(model);
			}

			return Redirect("/");
		}

		public async Task<IActionResult> DeleteHall(int id)
		{
			_db.Halls.Remove(_halls.GetById(id));
			await _db.SaveChangesAsync();
			return Redirect("/Admin/HallManagement");

		}

		public IActionResult SeasonManagement()
		{
			if (!CheckAdminRights())
				return Redirect("/");
			ViewBag.SeasonGenerated = string.Empty;
			SeasonManagementModel model = new SeasonManagementModel {Years = _seasons.GetAvailableSeasons()};
			return View(model);
		}

		[HttpPost]
		public IActionResult SeasonManagement(SeasonManagementModel model)
		{
			if (!ModelState.IsValid) return Redirect("/");
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

		public async Task<IActionResult> DeleteYear(int year)
		{
			if (!CheckAdminRights())
				return Redirect("/");
			IEnumerable<Match> matches = _match.GetMatchesBySeason(year);
			_db.Matches.RemoveRange(matches);
			await _db.SaveChangesAsync();
			return RedirectToAction("SeasonManagement","Admin");
		}

		public IActionResult PlayerManagement()
		{
			var model = new PlayerManagementModel();
			var teamList = _team.GetAll().ToList();
			var teams = new List<SelectListItem>();
			foreach (var team in teamList)
			{
				var item = new SelectListItem
				{
					Value = team.Id.ToString(),
					Text = team.Name
				};
				teams.Add(item);
			}
			ViewBag.Teams = teams;
			model.Players = _player.GetAll();

			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> PlayerManagement(PlayerManagementModel model)
		{
			var player = new Player
			{
				FirstName = model.FirstName,
				LastName = model.LastName,
				Team = _team.GetById(model.TeamId)
			};
			await _db.Players.AddAsync(player);
			await _db.SaveChangesAsync();

			return RedirectToAction("PlayerManagement", "Admin");
		}

		public IActionResult PlayerChange(string id)
		{
			var model = new PlayerChangeModel {Player = _player.GetById(id)};
			var teamList = _team.GetAll().ToList();
			var teams = new List<SelectListItem>();
			foreach (var team in teamList)
			{
				var item = new SelectListItem
				{
					Value = team.Id.ToString(),
					Text = team.Name
				};
				teams.Add(item);
			}
			ViewBag.Teams = teams;
			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> PlayerChange(PlayerChangeModel model, string id)
		{
			if (!ModelState.IsValid) return Redirect("/");

			var player = _player.GetById(id);
			player.Team = _team.GetById(model.TeamId);
			_db.Update(player);
			await _db.SaveChangesAsync();
			return RedirectToAction("PlayerManagement", "Admin");
		}

		public bool CheckAdminRights()
		{
			return User.IsInRole("Admin");
		}
	}
}