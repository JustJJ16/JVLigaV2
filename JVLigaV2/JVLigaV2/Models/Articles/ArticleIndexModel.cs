using System.Collections.Generic;
using JVLigaV2.LeagueData.Models;

namespace JVLigaV2.Models.Articles
{
	public class ArticleIndexModel
	{
		public IEnumerable<ArticleIndexListingModel> Articles { get; set; }
	}
}
