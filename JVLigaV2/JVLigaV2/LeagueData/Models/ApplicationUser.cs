using Microsoft.AspNetCore.Identity;

namespace JVLigaV2.LeagueData.Models
{
	public class ApplicationUser : IdentityUser
	{
		public Player Player { get; set; }
	}
}
