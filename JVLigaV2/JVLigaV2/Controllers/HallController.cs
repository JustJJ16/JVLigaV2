using System;
using System.Linq;
using System.Threading.Tasks;
using JVLiga.Models.Halls;
using JVLigaV2.LeagueData;
using JVLigaV2.LeagueData.Services;
using LeagueData;
using LeagueData.Models;
using LeagueServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JVLigaV2.Controllers
{
	public class HallController : Controller
	{
		private readonly LeagueContext _db;
		private readonly HallService _halls;

		public HallController(HallService halls, LeagueContext db)
		{
			_halls = halls;
			_db = db;
		}

		[BindProperty]
		public Hall Hall { get; set; }

		public IActionResult Index()
		{
			var hallModels = _halls.GetAll();
			var listingResult = hallModels
				.Select(result => new HallIndexListingModel
				{
					ID = result.Id,
					NAME = result.Name
				});

			var model = new ArticleIndexModel()
			{
				Halls = listingResult
			};
			model.Title = "Halls";
			return View(model);
		}

		public IActionResult Detail(int id)
		{
			Hall = _halls.GetById(id);
			var model = new HallDetailModel
			{
				HallId = id,
				Name = Hall.Name
			};
			return View(model);
		}

		public IActionResult Edit(int id)
		{
			Hall = _halls.GetById(id);
			var model = new HallDetailModel
			{
				HallId = id,
				Name = Hall.Name
			};
			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> Edit(Hall hall)
		{
			if (!ModelState.IsValid)
			{
				return RedirectToPage("/Index");
			}
			_db.Attach(hall).State = EntityState.Modified;
			try
			{
				await _db.SaveChangesAsync();
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				throw;
			}
			return Redirect("/Hall");
		}

		public async Task<IActionResult> Delete(Hall hall)
		{
			if (!ModelState.IsValid)
			{
				return RedirectToPage("/Index");
			}

			_db.Remove(hall);

			try
			{
				await _db.SaveChangesAsync();
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				throw;
			}
			return Redirect("/Hall");

		}

		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Create(Hall hall)
		{
			if (!ModelState.IsValid)
			{
				return RedirectToPage("/Index");
			}

			_db.Add(hall);

			try
			{
				await _db.SaveChangesAsync();
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				throw;
			}
			return Redirect("/Hall");
		}
	}
}