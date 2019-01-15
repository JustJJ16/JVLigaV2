using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LeagueData.Models
{
	public class Player
	{
		[Key]
		public string Id { get; set; }
		[Required]
		[MaxLength(50)]
		public string FirstName { get; set; }
		[Required]
		[MaxLength(50)]
		public string SecondName { get; set; }
		[Required]
		public Team Team { get; set; }
	}
}
