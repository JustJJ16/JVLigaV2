using JVLigaV2.LeagueData.Models;

namespace JVLigaV2.Models.Articles
{
	public class ArticleDetailModel
	{
		public string Title { get; set; }
		public string Description { get; set; }
		public string Body { get; set; }
		public string ImgSrc { get; set; }
		public LeagueData.Models.Team Team { get; set; }
		public LeagueData.Models.Match Match { get; set; }
		public LeagueData.Models.Player Player { get; set; }
	}
}
