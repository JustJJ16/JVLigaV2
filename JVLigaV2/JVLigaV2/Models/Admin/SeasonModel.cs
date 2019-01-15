using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JVLiga.Models.Admin
{
	public class SeasonModel
	{
		[Required(ErrorMessage = "Zadejte rok sezóny.")]
		[Display(Name = "Rok sezóny")]
		public int Year { get; set; }
	}
}
