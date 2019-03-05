using System.ComponentModel.DataAnnotations;

namespace JVLigaV2.LeagueData.Models
{
	public class Hall
	{
		[Key]
		public int Id { get; set; }
		[Required]
		[MaxLength(50)]
		[Display(Name = "Název")]
		public string Name { get; set; }
	}
}
