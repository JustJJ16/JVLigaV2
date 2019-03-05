using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using JVLiga.Models;
using JVLigaV2.LeagueData.Services;
using JVLigaV2.Models.Articles;
using JVLigaV2.Models.Home;
using Microsoft.AspNetCore.Mvc;

namespace JVLigaV2.Controllers
{
	public class HomeController : Controller
	{


		private readonly ArticleService _articles;
		private readonly SeasonService _seasons;

		public HomeController(ArticleService article, SeasonService season)
		{
			_articles = article;
			_seasons = season;
		}
		
		

		public IActionResult Index()
		{
			var articleModels = _articles.GetTop3();
			var listingResult = articleModels
				.Select(result => new ArticleIndexListingModel
				{
					Id = result.Id,
					Title = result.Title,
					Desrciption = result.Description,
					ImagePath = result.ImagePath
				});

			var model = new HomeIndexModel()
			{
				Articles = listingResult
			};
			ViewData["Title"] = "Domů";
			return View(model);
		}

		public IActionResult About()
		{
			ViewData["Message"] = "Your application description page.";

			ViewBag.Ahoj = new List<string>() { "ahoj", "jak je" };

			return View();
		}

		public IActionResult Contact()
		{
			ViewData["Message"] = "Your contact page.";

			return View();
		}

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}

	}
}