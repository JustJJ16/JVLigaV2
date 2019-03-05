using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using JVLigaV2.LeagueData.Models;

namespace JVLigaV2.Models.Admin
{
	public class TeamCreateModel
	{
		[Required]
		[DisplayName("Název")]
		public string Name { get; set; }
		[Required]
		[DisplayName("Hala")]
		public string HallId { get; set; }
	}
}
