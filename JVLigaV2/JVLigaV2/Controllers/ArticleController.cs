using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using JVLiga.Models.Articles;
using JVLigaV2.LeagueData;
using JVLigaV2.LeagueData.Services;
using JVLigaV2.Models.Articles;
using LeagueData;
using LeagueData.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace JVLigaV2.Controllers
{
	public class ArticleController : Controller
	{

		private readonly LeagueContext _db;
		private readonly ArticleService _articles;
		private readonly SignInManager<ApplicationUser> _singInManager;
		private readonly UserManager<ApplicationUser> _userManager;

		public ArticleController(ArticleService articles, LeagueContext db, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
		{
			_articles = articles;
			_db = db;
			_singInManager = signInManager;
			_userManager = userManager;
		}

		public IActionResult Index()
		{
			var articleModels = _articles.GetAll();
			var listingResult = articleModels
				.Select(result => new ArticleIndexListingModel
				{
					Id = result.Id,
					Title = result.Title,
					Desrciption = result.Description
				});

			var model = new ArticleIndexModel()
			{
				Articles = listingResult
			};
			return View(model);
		}
		public async Task<IActionResult> MyArticles()
		{
			var articleModels = _articles.GetMyArticles(await _userManager.GetUserAsync(User));
			var listingResult = articleModels
				.Select(result => new ArticleIndexListingModel
				{
					Id = result.Id,
					Title = result.Title,
					Desrciption = result.Description
				});

			var model = new ArticleIndexModel()
			{
				Articles = listingResult
			};
				
			return View(model);
		}

		public IActionResult Create()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> Create(ArticleCreateModel model, List<IFormFile> files)
		{
			if (!ModelState.IsValid)
			{
				return RedirectToPage("/Create");
			}

			if (_singInManager.IsSignedIn(User))
			{
				model.User = await _userManager.GetUserAsync(User);
			}

			model.PublishedDate = DateTime.Now;

			long size = files.Sum(f => f.Length);

			// full path to file in temp location
			var filePath = Path.GetTempFileName();

			foreach (var formFile in files)
			{
				if (formFile.Length > 0)
				{
					using (var stream = new FileStream(filePath, FileMode.Create))
					{
						await formFile.CopyToAsync(stream);
					}
				}
			}

			using (var memoryStream = new MemoryStream())
			{
				await model.ArticleImage.CopyToAsync(memoryStream);
				// model.AvatarImage = memoryStream.ToArray();
			}

			//_db.Add(3);

			try
			{
				await _db.SaveChangesAsync();
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				throw;
			}
			return Redirect("/Article");
		}

		public ActionResult Detail(int id)
		{
			var article = _articles.GetById(id);
			ArticleDetailModel model = new ArticleDetailModel(){
				Title = article.Title,
				Description = article.Description,
				Body = article.Body
			};

			return View(model);
		}
	}
}