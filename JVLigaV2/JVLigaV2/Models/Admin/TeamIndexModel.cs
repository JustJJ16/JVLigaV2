using LeagueData.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JVLiga.Models.Admin
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
