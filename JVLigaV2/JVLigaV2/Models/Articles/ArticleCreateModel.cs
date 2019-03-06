using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using JVLigaV2.LeagueData.Models;

namespace JVLigaV2.Models.Articles
{
	public class ArticleCreateModel
	{
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
		public IFormFile ArticleImage { get; set; }
		[Required]
		public DateTime PublishedDate { get; set; }
		public LeagueData.Models.Match Match { get; set; }
		public LeagueData.Models.Team Team { get; set; }
		public Player Player { get; set; }
		public ApplicationUser User { get; set; }
	}
}
