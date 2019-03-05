using System.ComponentModel.DataAnnotations;

namespace JVLigaV2.LeagueData.Models
{
	public class Result
	{
		[Key]
		public int Id { get; set; }
		[Required]
		public int Set { get; set; }
		[Display(Name ="Body domácí")]
		public int HomeTeamPoints { get; set; }
		[Display(Name = "Body hosté")]
		public int GuestTeamPoints { get; set; }
		[Required]
		public Match Match { get; set; }
	}
}
