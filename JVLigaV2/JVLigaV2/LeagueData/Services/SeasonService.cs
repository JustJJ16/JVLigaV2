﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Xml;
using JVLigaV2.LeagueData.Models;
using Nager.Date;

namespace JVLigaV2.LeagueData.Services
{
	public class SeasonService
	{
		private readonly LeagueContext _context;

		public SeasonService(LeagueContext context)
		{
			_context = context;
		}

		public bool SeasonExists(int year)
		{
			var matches = _context.Matches.Where(m => m.Date.Year == year);
			return matches.Any();
		}

		public void DeleteSeason(int year)
		{
			var matches = _context.Matches.Where(m => m.Date.Year == year);
			foreach (var match in matches)
			{
				_context.Remove(match);
			}
			
		}

		public void GenerateSeason(int year, List<Team> teams)
		{
			List<DateTime> availableDates = new List<DateTime>();
			//Ziskani vikendu (dostupnych dnu) pro zapasy
			for (DateTime date = new DateTime(year, 1, 1); date <= new DateTime(year, 12, 31); date = date.AddDays(1))
			{
				if ((date.DayOfWeek == DayOfWeek.Sunday || date.DayOfWeek == DayOfWeek.Saturday) && !DateSystem.IsPublicHoliday(date, CountryCode.CZ) )
					availableDates.Add(date);
			}

			//Smazani prvniho dostupneho dne, pokud je prvni den Nedele a posledniho, pokud neni cely vikend
			if (availableDates.FirstOrDefault().DayOfWeek == DayOfWeek.Sunday)
				availableDates.Remove(availableDates.FirstOrDefault());
			if (availableDates.Last().DayOfWeek == DayOfWeek.Saturday)
				availableDates.Remove(availableDates.Last());

			for (var i = 0; i < availableDates.Count; i++)
			{
				if (availableDates[i].DayOfWeek == DayOfWeek.Saturday)
					availableDates[i] += new TimeSpan(17, 0, 0);
				else if (availableDates[i].DayOfWeek == DayOfWeek.Sunday)
				{
					availableDates[i] += new TimeSpan(19, 0, 0);
				}
			}

			int periods = availableDates.Count / ((teams.Count-1)*2);
			int gamesInPeriod = Convert.ToInt32((double)teams.Count/periods);
			int gamesToPlay = teams.Count;
			int teamIndex = 0;
			int guestTeamIndex = teamIndex + gamesInPeriod;
			int currPeriod = 0;
			int totalGames = (teams.Count - 1)*2*3;

			for (int j = 0; j < totalGames; j++)
			{
				
				if (((double) gamesToPlay / gamesInPeriod) >= 1)
				{
					for (int i = 0; i < gamesInPeriod; i++)
					{
						if (teamIndex == guestTeamIndex)
						{
							TeamIndexUp(ref guestTeamIndex, teams.Count);
						}
						Match m = new Match()
						{
							Date = availableDates[j],
							GuestTeam = teams[guestTeamIndex],
							HomeTeam = teams[teamIndex]
						}; for (int r = 0; r < 3; r++)
						{
							Result result = new Result();
							result.GuestTeamPoints = 0;
							result.HomeTeamPoints = 0;
							result.Set = r + 1;
							result.Match = m;
							_context.Add(result);
						}
						_context.Add(m);
						TeamIndexUp(ref guestTeamIndex, teams.Count);
						TeamIndexUp(ref teamIndex, teams.Count);
						gamesToPlay--;
						if(gamesToPlay == 0)
							gamesToPlay = teams.Count;
					}
				}
				else
				{
					for (int i = 0; i < teams.Count-(currPeriod*gamesInPeriod); i++)
					{
						if (teamIndex == guestTeamIndex)
						{
							TeamIndexUp(ref guestTeamIndex, teams.Count);
						}
						Match m = new Match()
						{
							Date = availableDates[j],
							GuestTeam = teams[guestTeamIndex],
							HomeTeam = teams[teamIndex]
						};
						for (int r = 0; r < 3; r++)
						{
							Result result = new Result();
							result.GuestTeamPoints = 0;
							result.HomeTeamPoints = 0;
							result.Set = r + 1;
							result.Match = m;
							_context.Add(result);
						}
						_context.Add(m);
						TeamIndexUp(ref guestTeamIndex, teams.Count);
						TeamIndexUp(ref teamIndex, teams.Count);
						gamesToPlay--;
						if (gamesToPlay == 0)
							gamesToPlay = teams.Count;
					}
				}
				currPeriod++;
				if (currPeriod == periods)
				{
					currPeriod = 0;
					TeamIndexUp(ref guestTeamIndex, teams.Count);
				}
			}

			//for (int i = 0; i < 3; i++)
			//{
			//	Result result = new Result();
			//	result.GuestTeamPoints = 0;
			//	result.HomeTeamPoints = 0;
			//	result.Set = i + 1;
			//	result.Match = j;
			//	_context.Results.Add(result);
			//}

			_context.SaveChanges();
		}

		private void TeamIndexUp(ref int value, int maxValue)
		{
			value++;
			if (value >= maxValue)
				value -= maxValue;
		}

		public IEnumerable<int> GetAvailableSeasons()
		{
			var matches = _context.Matches;
			List<int> years = new List<int>();
			foreach (var match in matches)
			{
				years.Add(match.Date.Year);
			}
			return years.Distinct().OrderBy(y => y);
		}

	}
	
}

