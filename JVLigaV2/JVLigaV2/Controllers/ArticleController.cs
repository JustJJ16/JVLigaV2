using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using JVLigaV2.LeagueData;
using JVLigaV2.LeagueData.Models;
using JVLigaV2.LeagueData.Services;
using JVLigaV2.Models.Articles;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace JVLigaV2.Controllers
{
	public class ArticleController : Controller
	{

		private readonly LeagueContext _db;
		private readonly ArticleService _articles;
		private readonly TeamService _team;
		private readonly MatchService _match;
		private readonly PlayerService _player;
		private readonly IHostingEnvironment _hostingEnvironment;
		private readonly SignInManager<ApplicationUser> _singInManager;
		private readonly UserManager<ApplicationUser> _userManager;

		public ArticleController(MatchService match, PlayerService player, TeamService team, ArticleService articles, IHostingEnvironment hostingEnvironment, LeagueContext db, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
		{
			_articles = articles;
			_team = team;
			_player = player;
			_match = match;
			_db = db;
			_singInManager = signInManager;
			_userManager = userManager;
			_hostingEnvironment = hostingEnvironment;
		}

		public IActionResult Index()
		{
			var articles = _articles.GetAll().ToList();
			var model = new ArticleIndexModel();
			var listingResult = articles
				.Select(result => new ArticleIndexListingModel
				{
					Id = result.Id,
					Title = result.Title,
					Desrciption = result.Description,
					ImagePath = result.ImagePath,
					PublishedDate = result.PublishedDate
				});

			model.Articles = listingResult;
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
					Desrciption = result.Description,
					ImagePath = result.ImagePath,
					PublishedDate = result.PublishedDate
				});

			var model = new ArticleIndexModel()
			{
				Articles = listingResult
			};
				
			return View(model);
		}

		public IActionResult Create()
		{
			var playerList = _player.GetAll();
			var players = new List<SelectListItem> {new SelectListItem()};
			foreach (var player in playerList)
			{
				var item = new SelectListItem
				{
					Value = player.Id.ToString(),
					Text = player.FirstName + " " + player.LastName
				};
				players.Add(item);
			}
			var matchList = _match.GetAll();
			var matches = new List<SelectListItem> {new SelectListItem()};
			foreach (var match in matchList)
			{
				var item = new SelectListItem
				{
					Value = match.Id.ToString(),
					Text = match.HomeTeam.Name + "-" + match.GuestTeam.Name + " " + match.Date.ToString("dd.MM.yyy H:mm")
				};
				matches.Add(item);
			}

			var teamList = _team.GetAll();
			var teams = new List<SelectListItem> {new SelectListItem()};
			foreach (var team in teamList)
			{
				var item = new SelectListItem
				{
					Value = team.Id.ToString(),
					Text = team.Name
				};
				teams.Add(item);
			}
			ViewBag.Teams = teams;
			ViewBag.Players = players;
			ViewBag.Matches = matches;
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

			if(Convert.ToInt32(model.PlayerId) != 0)
			{
				article.Player = _player.GetById(Convert.ToInt32(model.PlayerId));
			}

			if(Convert.ToInt32(model.MatchId) != 0)
			{
				article.Match = _match.GetById(Convert.ToInt32(model.MatchId));
			}

			if(Convert.ToInt32(model.TeamId) != 0)
			{
				article.Team = _team.GetById(Convert.ToInt32(model.TeamId));
			}

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

		public IActionResult Detail(int id)
		{
			var article = _articles.GetById(id);
			var model = new ArticleDetailModel(){
				Title = article.Title,
				Description = article.Description,
				Body = article.Body,
				ImgSrc = article.ImagePath
			};

			if (article.Match != null)
				model.Match = _match.GetById(article.Match.Id);
			if (article.Player != null)
				model.Player = _player.GetById(article.Player.Id);
			if (article.Team != null)
				model.Team = _team.GetById(article.Team.Id);

			return View(model);
		}

		public async Task<IActionResult> Delete(int id)
		{
			var article = _articles.GetById(id);
			if (_singInManager.IsSignedIn(User))
			{
				var user = await _userManager.GetUserAsync(User);
				if (article.User.Id != user.Id)
				{
					if (!CheckAdminRights())
					{
						Redirect("/");
					}
				}
			}
			else Redirect("/");

			_db.Articles.Remove(article);
			await _db.SaveChangesAsync();
			return RedirectToAction("MyArticles", "Article");
		}

		public bool CheckAdminRights()
		{
			return User.IsInRole("Admin");
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