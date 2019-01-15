using LeagueData.Models;
using System.Collections.Generic;

namespace JVLiga.Models.Articles
{
	public class ArticleIndexModel
	{
		public IEnumerable<ArticleIndexListingModel> Articles { get; set; }
		//public ApplicationUser User { get; set; }
		//public string Title;
	}
}
