using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using JVLigaV2.LeagueData.Models;

namespace JVLigaV2.Models.Admin
{
	public class TeamManagementModel
	{
		[Required]
		[DisplayName("Název")]
		public string Name { get; set; }
		[Required]
		[DisplayName("Hala")]
		public string HallId { get; set; }
		public IEnumerable<LeagueData.Models.Team> Teams { get; set; }
	}
}
