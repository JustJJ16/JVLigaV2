using System.ComponentModel.DataAnnotations;

namespace JVLigaV2.Models.Admin
{
	public class SeasonModel
	{
		[Required(ErrorMessage = "Zadejte rok sezóny.")]
		[Display(Name = "Rok sezóny")]
		public int Year { get; set; }
	}
}
