using System;

namespace JVLigaV2.Models.Articles
{
	public class ArticleIndexListingModel
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public string Desrciption { get; set; }
		public string ImagePath { get; set; }
		public DateTime PublishedDate { get; set; }
	}
}
