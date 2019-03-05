using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using LeagueData.Models;

namespace JVLigaV2.Models.Admin
{
	public class TeamIndexModel
	{
		[Required]
		public Team Team { get; set; }
		[Required]
		[DisplayName("Hala")]
		public string HallId { get; set; }
	}
}
