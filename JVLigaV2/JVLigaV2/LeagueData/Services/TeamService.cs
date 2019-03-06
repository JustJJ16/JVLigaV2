using System.Collections;
using System.Collections.Generic;
using System.Linq;
using JVLigaV2.LeagueData.Models;
using Microsoft.EntityFrameworkCore;

namespace JVLigaV2.LeagueData.Services
{
	public class TeamService
	{
		private readonly LeagueContext _context;

		public TeamService(LeagueContext context)
		{
			_context = context;
		}

		public Team GetById(int id)
		{
			return _context.Teams
					.Include(t => t.Hall)
					.FirstOrDefault(t => t.Id == id);
		}

		public IEnumerable<Team> GetAll()
		{
			return _context.Teams;
		}
	}
}
