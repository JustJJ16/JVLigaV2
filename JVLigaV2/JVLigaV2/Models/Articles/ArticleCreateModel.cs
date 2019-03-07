using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;
using JVLigaV2.LeagueData.Models;

namespace JVLigaV2.Models.Articles
{
	public class ArticleCreateModel
	{
		[Required]
		[MaxLength(100)]
		[Display(Name="Nadpis")]
		public string Title { get; set; }

		[Required]
		[MaxLength(1000)]
		[Display(Name = "Popis")]
		public string Description { get; set; }
		[Required]
		[MaxLength(20000)]
		[Display(Name = "Tělo")]
		public string Body { get; set; }
		[Required]
		[Display(Name = "Úvodní fotka")]
		public IFormFile ArticleImage { get; set; }
		[Display(Name = "Vztahuje se k zápasu")]
		public string MatchId { get; set; }
		[Display(Name = "Vztahuje se k týmu")]
		public string TeamId { get; set; }
		[Display(Name = "Vztahuje se k hráči")]
		public string PlayerId { get; set; }
		public DateTime PublishedDate { get; set; }
		public ApplicationUser User { get; set; }
	}
}
