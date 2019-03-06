using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using JVLigaV2.LeagueData.Models;

namespace JVLigaV2.Models.Admin
{
	public class PlayerManagementModel
	{
		[Required]
		[Display(Name="Jméno")]
		public string FirstName { get; set; }
		[Required]
		[Display(Name = "Příjmení")]
		public string LastName { get; set; }
		[Required]
		[Display(Name = "Tým")]
		public int TeamId { get; set; }
		public IEnumerable<Player> Players { get; set; }

	}
}
