using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace JVLiga.Models.Articles
{
	public class ArticleIndexListingModel
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public string Desrciption { get; set; }
		public string ImagePath { get; set; }
	}
}
