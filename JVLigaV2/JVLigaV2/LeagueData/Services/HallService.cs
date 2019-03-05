using System.Collections.Generic;
using System.Linq;
using JVLigaV2.LeagueData.Models;

namespace JVLigaV2.LeagueData.Services
{
	public class HallService
	{
		private readonly LeagueContext _context;

		public HallService(LeagueContext context)
		{
			_context = context;
		}

		public void Add(Hall newHall)
		{
			_context.Add(newHall);
			_context.SaveChanges();
		}

		public IEnumerable<Hall> GetAll()
		{
			return _context.Halls;
		}

		public Hall GetById(int id)
		{
			return
				_context.Halls
				.FirstOrDefault(a => a.Id == id);
		}

		public string GetName(int id)
		{
			if (_context.Halls.Any(h => h.Id == id))
			{
				return 
					_context.Halls
						.FirstOrDefault(h => h.Id == id)
						?.Name;
			}
			else return "";
		}
	}
}
