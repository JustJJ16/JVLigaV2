using System;
using System.Collections.Generic;
using System.Linq;
using JVLigaV2.LeagueData.Models;
using Microsoft.EntityFrameworkCore;

namespace JVLigaV2.LeagueData.Services
{
	public class ResultService
	{
		private readonly LeagueContext _context;
		public ResultService(LeagueContext context)
		{
			_context = context;
		}
		public void Add(Result result)
		{
			_context.Add(result);
			_context.SaveChanges();
		}

		public IEnumerable<Result> GetResultsForMatch(int id)
		{
			return _context.Results
				.Where(r => r.Match.Id == id)
				.Include(r => r.Match);
		}

		public Result GetResultBySetAndMatch(int set, int id)
		{
			return _context.Results
				.Where(r => r.Match.Id == id)
				.FirstOrDefault(r => r.Set == set);
		}

		public void UpdateResult(Result result)
		{
			if (_context.Results.Where(r => r.Match == result.Match).Count(r => r.Set == result.Set) > 1)
			{
				throw new Exception("Výsledek pro tento zápas a set již existuje");
			}

			_context.Entry(result).State = EntityState.Modified;
			_context.SaveChanges();
		}
	}
}
