using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JVLigaV2.LeagueData.Models;

namespace JVLigaV2.Models.Admin
{
	public class UserManagementModel
	{
		public IEnumerable<ApplicationUser> Users { get; set; }
	}
}
