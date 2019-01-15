using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace LeagueData.Models
{
	public class ApplicationUser : IdentityUser
	{
		public Player Player { get; set; }
	}
}
