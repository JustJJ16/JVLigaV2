using JVLiga.Models.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace JVLigaV2.Controllers
{
	public class LoginController : Controller
	{

		private readonly SignInManager<IdentityUser> _signInManager;
		private readonly ILogger<LoginModel> _logger;

		public LoginController(SignInManager<IdentityUser> signInManager, ILogger<LoginModel> logger)
		{
			_signInManager = signInManager;
			_logger = logger;
		}


		public IActionResult Index()
		{
			return View();
		}

		
	}
}