﻿using System;
using System.Collections.Generic;
using System.Linq;
using JVLigaV2.LeagueData.Models;
using Microsoft.EntityFrameworkCore;

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
			return _context.Matches
				.Include(m => m.HomeTeam)
				.Include(m => m.GuestTeam)
				.Include(m => m.HomeTeam.Hall)
				.Include(m => m.HomeTeam.Hall);
		}

		public Match GetById(int id)
		{
			return
				_context.Matches
					.Include(m => m.HomeTeam)
					.Include(m => m.GuestTeam)
					.Include(m => m.HomeTeam.Hall)
					.Include(m => m.HomeTeam.Hall)
				.FirstOrDefault(m => m.Id == id);
		}

		public IEnumerable<Match> GetTopMatchesBySeason(int year, int num)
		{
			return _context.Matches
				.OrderBy(m => m.Date)
				.Where(m => m.Date.Year == year)
				.Where(m => m.Date >= DateTime.Now)
				.Take(num)
				.Include(m => m.HomeTeam)
				.Include(m => m.GuestTeam)
				.Include(m => m.HomeTeam.Hall)
				.Include(m => m.HomeTeam.Hall);
		}

		public IEnumerable<Match> GetMatchesBySeason(int year)
		{
			return _context.Matches.Where(m => m.Date.Year == year)
				.Include(m => m.HomeTeam)
				.Include(m => m.GuestTeam)
				.Include(m => m.HomeTeam.Hall)
				.Include(m => m.HomeTeam.Hall)
				.OrderBy(m => m.Date);
		}
	}
}
