using System.Collections.Generic;
using System.Linq;
using JVLigaV2.LeagueData.Models;
using Microsoft.EntityFrameworkCore;

namespace JVLigaV2.LeagueData.Services
{
	public class ArticleService
	{
		private readonly LeagueContext _context;

		public ArticleService(LeagueContext context)
		{
			_context = context;
		}
		public void Add(Article newArticle)
		{
			_context.Add(newArticle);
			_context.SaveChanges();
		}

		public IEnumerable<Article> GetAll()
		{
			return _context.Articles
				.Include(a => a.Match)
				.Include(a => a.Match.HomeTeam)
				.Include(a => a.Match.HomeTeam.Hall)
				.Include(a => a.Match.GuestTeam)
				.Include(a => a.Match.GuestTeam.Hall)
				.Include(a => a.Player)
				.Include(a => a.Player.Team)
				.Include(a => a.Team)
				.Include(a => a.User)
				.Include(a => a.User.Player)
				.OrderByDescending(a => a.PublishedDate);
		}

		public Article GetById(int id)
		{
			return _context.Articles
					.Include(a => a.Match)
				.Include(a => a.Match.HomeTeam)
				.Include(a => a.Match.HomeTeam.Hall)
				.Include(a => a.Match.GuestTeam)
				.Include(a => a.Match.GuestTeam.Hall)
				.Include(a => a.Player)
				.Include(a => a.Player.Team)
				.Include(a => a.Team)
				.Include(a => a.User)
				.Include(a => a.User.Player)
				.FirstOrDefault(a => a.Id == id);
		}

		public IEnumerable<Article> GetMyArticles(ApplicationUser applicationUser)
		{
			return _context.Articles
				.Include(a => a.Match)
				.Include(a => a.Match.HomeTeam)
				.Include(a => a.Match.HomeTeam.Hall)
				.Include(a => a.Match.GuestTeam)
				.Include(a => a.Match.GuestTeam.Hall)
				.Include(a => a.Player)
				.Include(a => a.Player.Team)
				.Include(a => a.Team)
				.Include(a => a.User)
				.Include(a => a.User.Player)
				.Where(a => a.User == applicationUser)
				.OrderByDescending(a => a.PublishedDate);
		}

		public IEnumerable<Article> GetTop3()
		{
			return _context.Articles
				.Include(a => a.Match)
				.Include(a => a.Match.HomeTeam)
				.Include(a => a.Match.HomeTeam.Hall)
				.Include(a => a.Match.GuestTeam)
				.Include(a => a.Match.GuestTeam.Hall)
				.Include(a => a.Player)
				.Include(a => a.Player.Team)
				.Include(a => a.Team)
				.Include(a => a.User)
				.Include(a => a.User.Player)
				.OrderByDescending(a => a.PublishedDate)
				.Take(3);
		}
	}
}
