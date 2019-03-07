using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JVLigaV2.LeagueData.Models;
using Microsoft.EntityFrameworkCore;

namespace JVLigaV2.LeagueData.Services
{
	public class UserService
	{
		private readonly LeagueContext _context;

		public UserService(LeagueContext context)
		{
			_context = context;
		}

		public IEnumerable<ApplicationUser> GetAllUsers()
		{
			return _context.Users.Include(u => u.Player);
		}

		public ApplicationUser GetById(string id)
		{
			return _context.Users.Include(u=>u.Player).FirstOrDefault(u => u.Id == id);
		}
	}
}
