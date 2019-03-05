using System.Linq;
using System.Threading.Tasks;
using JVLigaV2.LeagueData;
using JVLigaV2.Models.Account;
using LeagueData;
using LeagueData.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace JVLigaV2.Controllers
{
	public class AccountController : Controller
	{


		private readonly SignInManager<ApplicationUser> _signInManager;
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly RoleManager<IdentityRole> _roleManager;
		private readonly ILogger<LoginModel> _logger;
		private readonly LeagueContext _db;

		public AccountController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, ILogger<LoginModel> logger, LeagueContext db)
		{
			_signInManager = signInManager;
			_logger = logger;
			_userManager = userManager;
			_roleManager = roleManager;
			_db = db;
		}

		public IActionResult LogIn()
		{
			var model = new LoginModel();
			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> Login(LoginModel model)
		{
			if (ModelState.IsValid)
			{
				// This doesn't count login failures towards account lockout
				// To enable password failures to trigger account lockout, set lockoutOnFailure: true
				var user = _db.Users.FirstOrDefault(u => u.UserName == model.UserName);
				if (user == null)
				{
					ModelState.AddModelError("UserName", "Uživatel nebyl nalezen.");
					return View(model);
				}
				var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, lockoutOnFailure: true);
				if (result.Succeeded)
				{
					_logger.LogInformation("User logged in.");
					return Redirect("/");
				}
				//if (result.RequiresTwoFactor)
				//{
				//	return RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
				//}
				//if (result.IsLockedOut)
				//{
				//	_logger.LogWarning("User account locked out.");
				//	return Redirect("/Account/LockOut");
				//}
				else
				{
					ModelState.AddModelError("Password", "Špatně zadané jméno nebo heslo.");
					return View(model);
				}
			}

			// If we got this far, something failed, redisplay form
			return Redirect("/Account/Login");
		}

		[HttpPost]
		public async Task<IActionResult> Register(RegisterModel model)
		{
			if (ModelState.IsValid)
			{
				var users = _db.Users;
				ApplicationUser user = new ApplicationUser { UserName = model.UserName, Email = model.Email };
				IdentityResult result = await _userManager.CreateAsync(user, model.Password);
				if (result.Succeeded)
				{
					_logger.LogInformation("User created a new account with password.");

					if (users != null)
					{
						IdentityRole adminRole = new IdentityRole();
						adminRole.Name = "Admin";
						await _roleManager.CreateAsync(adminRole);
						var result1 = await _userManager.AddToRoleAsync(user, "Admin");

						IdentityRole editorRole = new IdentityRole();
						editorRole.Name = "Editor";
						await _roleManager.CreateAsync(editorRole);

						IdentityRole playerRole = new IdentityRole();
						playerRole.Name = "Player";
						await _roleManager.CreateAsync(playerRole);
					}

					//var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
					//var callbackUrl = Url.Page(
					//	"/Account/ConfirmEmail",
					//	pageHandler: null,
					//	values: new { userId = user.Id, code = code },
					//	protocol: Request.Scheme);

					//await _emailSender.SendEmailAsync(model.Email, "Confirm your email",
					//	$"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

					await _signInManager.SignInAsync(user, isPersistent: false);
					return Redirect("/");
				}
				foreach (var error in result.Errors)
				{
					ModelState.AddModelError(string.Empty, error.Description);
					System.Diagnostics.Debug.WriteLine(error.Description);
				}
			}

			// If we got this far, something failed, redisplay form
			return RedirectToPage("/Account/Register");
		}
		public async Task<IActionResult> LogOut()
		{
			await _signInManager.SignOutAsync();
			_logger.LogInformation("User logged out.");
			return Redirect("/");

		}

		public IActionResult Register()
		{
			return View();
		}
	}
}