using System.Collections.Generic;

namespace JVLiga.Models.Halls
{
	public class ArticleIndexModel
	{
		public IEnumerable<HallIndexListingModel> Halls { get; set; }
		public string Title;
	}
}
