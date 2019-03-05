using System.ComponentModel.DataAnnotations;

namespace JVLigaV2.Models.Account
{
	public class RegisterModel
	{
		[Required]
		[StringLength(30, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
		[Display(Name = "Uživatelské jméno")]
		public string UserName { get; set; }

		[Required]
		[EmailAddress]
		[Display(Name = "Email")]
		public string Email { get; set; }

		[Required]
		[StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
		[DataType(DataType.Password)]
		[Display(Name = "Heslo")]
		public string Password { get; set; }

		[DataType(DataType.Password)]
		[Display(Name = "Potvrdit heslo")]
		[Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
		public string ConfirmPassword { get; set; }
	}
}
