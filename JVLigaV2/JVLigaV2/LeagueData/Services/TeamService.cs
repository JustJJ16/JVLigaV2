﻿using System;
using System.Collections.Generic;
using System.Linq;
using LeagueData;
using LeagueData.Models;
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
			.FirstOrDefault(t => t.Id == id);
		}

		/*public IEnumerable<Team> GetStandingsForCurrYear()
		{
			List<Result> results =  _context.Results.Include(r => r.Match)
				.Where(r => r.Match.Date.Year == DateTime.Now.Year).ToList();

		}*/
	}
}
