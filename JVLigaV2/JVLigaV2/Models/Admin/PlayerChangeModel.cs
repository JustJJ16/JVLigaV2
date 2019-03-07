using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using JVLigaV2.LeagueData.Models;

namespace JVLigaV2.Models.Admin
{
	public class PlayerChangeModel
	{
		public LeagueData.Models.Player Player { get; set; }
		[Required]
		[Display(Name = "Tým")]
		public int TeamId { get; set; }
	}
}
