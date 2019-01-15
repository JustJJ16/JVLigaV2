using LeagueData.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace JVLigaV2.LeagueData
{
	public class LeagueContext : IdentityDbContext<ApplicationUser>
	{
		public LeagueContext(DbContextOptions options) : base(options) {}

		public DbSet<Player> Players { get; set; }
		public DbSet<Team> Teams { get; set; }
		public DbSet<Hall> Halls { get; set; }
		public DbSet<Article> Articles { get; set; }
		public DbSet<Match> Matches { get; set; }
		public DbSet<Result> Results { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			modelBuilder.Entity<ApplicationUser>().Ignore(c => c.NormalizedUserName)
				.Ignore(c => c.NormalizedEmail)
				.Ignore(c => c.TwoFactorEnabled);//and so on...

			modelBuilder.Entity<ApplicationUser>().ToTable("Users");//to change the name of table.

		}
		
	}
}
