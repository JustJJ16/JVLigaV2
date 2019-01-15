using LeagueData;
using LeagueData.Models;

namespace JVLigaV2.LeagueData.Services
{
	public class ResultService
	{
		private readonly LeagueContext _context;
		public ResultService(LeagueContext context)
		{
			_context = context;
		}
		public void Add(Result result)
		{
			_context.Add(result);
			_context.SaveChanges();
		}
	}
}
