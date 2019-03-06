using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace JVLigaV2.Models.Admin
{
	public class SeasonManagementModel
	{
		[Required(ErrorMessage = "Zadejte rok sezóny.")]
		[Display(Name = "Rok sezóny")]
		public int Year { get; set; }
		public IEnumerable<int> Years { get; set; }
	}
}
