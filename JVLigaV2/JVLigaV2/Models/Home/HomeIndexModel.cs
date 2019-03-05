using System.Collections.Generic;
using JVLigaV2.Models.Articles;

namespace JVLigaV2.Models.Home
{
	public class HomeIndexModel
	{
		public IEnumerable<ArticleIndexListingModel> Articles { get; set; }
	}
}
