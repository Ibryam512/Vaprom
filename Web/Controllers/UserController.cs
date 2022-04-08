using Microsoft.AspNetCore.Mvc;
using ViewModels.Input;

namespace Web.Controllers
{
	public class UserController : Controller
	{
		[HttpGet]
		public IActionResult Register()
		{
			RegisterUserViewModel model = new RegisterUserViewModel();

			return View(model);
		}

		[HttpGet]
		public IActionResult Login()
		{
			LoginUserViewModel model = new LoginUserViewModel();

			return View(model);
		}

		[HttpGet]
		public IActionResult Profile()
		{
			return View();
		}
	}
}
