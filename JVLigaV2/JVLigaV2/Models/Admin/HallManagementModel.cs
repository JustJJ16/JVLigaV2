using JVLigaV2.LeagueData.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JVLigaV2.Models.Admin
{
	public class HallManagementModel
	{
		[Display(Name = "Název")]
		public string HallName { get; set; }
		public IEnumerable<Hall> Halls { get; set; }
	}
}
