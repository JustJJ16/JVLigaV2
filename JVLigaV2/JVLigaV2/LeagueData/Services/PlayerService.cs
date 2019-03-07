using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JVLigaV2.LeagueData.Models;
using Microsoft.EntityFrameworkCore;

namespace JVLigaV2.LeagueData.Services
{
	public class PlayerService
	{
		private readonly LeagueContext _context;
		public PlayerService(LeagueContext context)
		{
			_context = context;
		}

		public IEnumerable<Player> GetPlayersForTeam(Team team)
		{
			return _context.Players.Where(p => p.Team == team)
				.Include(p => p.Team)
				.Include(p => p.Team.Hall);
		}

		public IEnumerable<Player> GetAll()
		{
			return _context.Players
				.Include(p => p.Team)
				.Include(p => p.Team.Hall);
		}

		public Player GetById(int id)
		{
			return _context.Players
				.Include(p => p.Team)
				.Include(p => p.Team.Hall)
				.FirstOrDefault(p => p.Id == id);
		}
	}
}
