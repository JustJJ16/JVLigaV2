using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JVLigaV2.LeagueData;
using JVLigaV2.LeagueData.Services;
using JVLigaV2.Models.Team;
using Microsoft.AspNetCore.Mvc;

namespace JVLigaV2.Controllers
{
	public class TeamController : Controller
	{
		private readonly TeamService _team;
		private readonly PlayerService _player;
		private readonly LeagueContext _db;
		public TeamController(TeamService team, PlayerService player, LeagueContext db)
		{
			_team = team;
			_player = player;
			_db = db;
		}

		public IActionResult Detail(int id)
		{
			TeamDetailModel model = new TeamDetailModel();
			model.Team = _team.GetById(id);
			model.Players = _player.GetPlayersForTeam(model.Team);
			return View(model);
		}

	}
}