using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JVLigaV2.LeagueData.Services;
using JVLigaV2.Models.Player;
using Microsoft.AspNetCore.Mvc;

namespace JVLigaV2.Controllers
{
	public class PlayerController : Controller
	{
		private readonly PlayerService _player;
		public PlayerController(PlayerService palyer)
		{
			_player = palyer;
		}

		public IActionResult Detail(int id)
		{
			var model = new PlayerDetailModel {Player = _player.GetById(id)};
			return View(model);
		}
	}
}