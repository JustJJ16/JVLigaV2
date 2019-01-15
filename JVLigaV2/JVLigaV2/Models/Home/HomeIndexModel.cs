using JVLiga.Models.Articles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JVLiga.Models.Home
{
	public class HomeIndexModel
	{
		public IEnumerable<ArticleIndexListingModel> Articles { get; set; }
	}
}
