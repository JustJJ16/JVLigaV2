using System;
using System.Collections.Generic;
using System.Linq;
using LeagueData;
using LeagueData.Models;

namespace JVLigaV2.LeagueData.Services
{
	public class MatchService
	{
		private readonly LeagueContext _context;
		public MatchService(LeagueContext context)
		{
			_context = context;
		}
		public IEnumerable<Match> GetAll()
		{
			return _context.Matches;
		}

		public Match GetById(int id)
		{
			return
				_context.Matches
				.FirstOrDefault(m => m.Id == id);
		}

		public IEnumerable<Match> GetTopMatchesBySeason(int year, int num)
		{
			return _context.Matches.Where(m => m.Date.Year == year)
				.Take(num)
				.Where(m => m.Date >= DateTime.Now);
		}

		public IEnumerable<Match> GetMatchesBySeason(int year)
		{
			return _context.Matches.Where(m => m.Date.Year == year);
		}
	}
}
