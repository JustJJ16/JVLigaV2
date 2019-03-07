using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JVLigaV2.Models.Match
{
	public class MatchChangeModel
	{
		public LeagueData.Models.Match Match { get; set; }
		[Required]
		[Display(Name="Nové datum")]
		public DateTime NewDateTime { get; set; }
	}
}
