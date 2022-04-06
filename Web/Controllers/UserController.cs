using Microsoft.AspNetCore.Mvc;
using ViewModels.Input;
using Repositories.Interfaces;
using Repositories.Helpers;
using System;
using System.Linq;

namespace Web.Controllers
{
	public class UserController : Controller
	{
		private ILoginRegisterRepository loginRegisterRepository;
		public UserController(ILoginRegisterRepository _loginRegisterRepository)
        {
			this.loginRegisterRepository = _loginRegisterRepository;
        }
		[HttpGet]
		public IActionResult Register()
		{
			RegisterUserViewModel model = new RegisterUserViewModel();

			return View(model);
		}
		[HttpPost]
		public IActionResult Register(RegisterUserViewModel model)
		{
			if (ModelState.IsValid)
			{
				if (model.Password != model.ConfirmPassword)
				{
					ModelState.AddModelError("passConfirm", "Паролите не съвпадат!");
					return View(model);
				}


				string firstName = model.FirstName;
				string lastName = model.LastName;
				string username = model.Username;
				string password = model.Password;

				try
				{
					loginRegisterRepository.Register(username, password, firstName, lastName);
				}
				catch (Exception)
				{
					ModelState.AddModelError("usernameExists", "Потребител със същото име вече съществува");
					return View(model);
				}
			}
			else
			{
				return View(model);
			}

			return RedirectToAction("Index", "Home");
		}

		[HttpGet]
		public IActionResult Login()
		{
			LoginUserViewModel model = new LoginUserViewModel();

			return View(model);
		}
	}
}
