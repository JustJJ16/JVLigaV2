﻿using System.ComponentModel.DataAnnotations;

namespace JVLigaV2.LeagueData.Models
{
	public class Player
	{
		[Key]
		public int Id { get; set; }
		[Required]
		[MaxLength(50)]
		public string FirstName { get; set; }
		[Required]
		[MaxLength(50)]
		public string LastName { get; set; }
		[Required]
		public Team Team { get; set; }
	}
}
