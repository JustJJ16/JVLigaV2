using System.Linq;
using LeagueData;
using LeagueServices;
using Microsoft.AspNetCore.Mvc;

namespace JVLigaV2.Components
{
	public class SeasonMenu : ViewComponent
	{
		private readonly SeasonService _seasons;

		public SeasonMenu(SeasonService season)
		{
			_seasons = season;
		}
		public IViewComponentResult Invoke()
		{
			int[] seasons = _seasons.GetAvailableSeasons().ToArray();
			return View(seasons);
		}
	}
}
