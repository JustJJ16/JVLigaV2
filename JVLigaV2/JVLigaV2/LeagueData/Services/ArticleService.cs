using System.Collections.Generic;
using System.Linq;
using JVLigaV2.LeagueData.Models;

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
				.OrderByDescending(a => a.PublishedDate);
		}

		public Article GetById(int id)
		{
			return _context.Articles
				.FirstOrDefault(a => a.Id == id);
		}

		public IEnumerable<Article> GetMyArticles(ApplicationUser applicationUser)
		{
			return _context.Articles
				.Where(a => a.User == applicationUser)
				.OrderByDescending(a => a.PublishedDate);
		}

		public IEnumerable<Article> GetTop3()
		{
			return _context.Articles
				.Take(3)
				.OrderByDescending(a => a.PublishedDate);
		}
	}
}
