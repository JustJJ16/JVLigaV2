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
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace JVLigaV2.Controllers
{
	public class ArticleController : Controller
	{

		private readonly LeagueContext _db;
		private readonly ArticleService _articles;
		private readonly IHostingEnvironment _hostingEnvironment;
		private readonly SignInManager<ApplicationUser> _singInManager;
		private readonly UserManager<ApplicationUser> _userManager;

		public ArticleController(ArticleService articles, IHostingEnvironment hostingEnvironment, LeagueContext db, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
		{
			_articles = articles;
			_db = db;
			_singInManager = signInManager;
			_userManager = userManager;
			_hostingEnvironment = hostingEnvironment;
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
		public async Task<IActionResult> Create(ArticleCreateModel model)
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

			Article article = new Article()
			{
				Title = model.Title,
				Description = model.Description,
				Body = model.Body,
				PublishedDate = model.PublishedDate,
				User = model.User
			};

			if (model.ArticleImage != null)
			{
				var uniqueFileName = GetUniqueFileName(model.ArticleImage.FileName);
				var uploads = Path.Combine(_hostingEnvironment.WebRootPath, "Uploads");
				var filePath = Path.Combine(uploads, uniqueFileName);
				model.ArticleImage.CopyTo(new FileStream(filePath, FileMode.Create));

				article.ImagePath = $@"\Uploads\{uniqueFileName}";

				_articles.Add(article);
			}
			
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

		private string GetUniqueFileName(string fileName)
		{
			fileName = Path.GetFileName(fileName);
			return Path.GetFileNameWithoutExtension(fileName)
					  + "_"
					  + Guid.NewGuid().ToString().Substring(0, 4)
					  + Path.GetExtension(fileName);
		}
	}
}