using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace JVLiga.Models.Account
{
	public class LoginModel
	{

		[Required(ErrorMessage = "Zadejte uživatelské jméno!")]
		public string UserName { get; set; }

		[Required(ErrorMessage = "Zadejte heslo!")]
		[DataType(DataType.Password)]
		public string Password { get; set; }

		[Display(Name = "Zapamatovat si?")]
		public bool RememberMe { get; set; }

		[TempData]
		public string ErrorMessage { get; set; }

		[BindProperty]
		public LoginModel Input { get; set; }

		public IList<AuthenticationScheme> ExternalLogins { get; set; }

		public string ReturnUrl { get; set; }

		public string title = "Log in";
	}
}
