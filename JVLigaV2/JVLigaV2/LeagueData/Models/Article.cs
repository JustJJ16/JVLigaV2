using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LeagueData.Models
{
	public class Article
	{
		[Key]
		public int Id { get; set; }
		[Required]
		[MaxLength(100)]
		public string Title { get; set; }

		[Required]
		[MaxLength(1000)]
		public string Description { get; set; }
		[Required]
		[MaxLength(20000)]
		public string Body { get; set; }
		[Required]
		public DateTime PublishedDate { get; set; }
		public Match Match { get; set; }
		public Team Team { get; set; }
		public Player Player { get; set; }
		public ApplicationUser User { get; set; }

	}
}
