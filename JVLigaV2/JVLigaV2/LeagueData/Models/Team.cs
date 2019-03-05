using System.ComponentModel.DataAnnotations;

namespace JVLigaV2.LeagueData.Models
{
	public class Team
	{
		[Key]
		public int Id { get; set; }
		[Required]
		[MaxLength(50)]
		public string Name { get; set; }
		[Required]
		public Hall Hall { get; set; }
	}
}
