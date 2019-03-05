using System.Collections.Generic;

namespace JVLigaV2.Models.Halls
{
	public class ArticleIndexModel
	{
		public IEnumerable<HallIndexListingModel> Halls { get; set; }
		public string Title;
	}
}
