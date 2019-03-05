using System.Linq;
using JVLigaV2.LeagueData.Models;

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
			.FirstOrDefault(t => t.Id == id);
		}
	}
}
