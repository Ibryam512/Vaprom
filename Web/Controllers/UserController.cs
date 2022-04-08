using Microsoft.AspNetCore.Mvc;
using ViewModels.Input;
using Repositories.Interfaces;
using Repositories.Helpers;
using System;
using System.Linq;
using DataAccess;
using AutoMapper;
using Repositories.Mapper;
using Models;

namespace Web.Controllers
{
	public class UserController : Controller
	{
		private ILoginRegisterRepository loginRegisterRepository;
		private VacationManagerDbContext vacationManagerDbContext;
		private IMapper mappingProfile;
		public UserController(IMapper _mappingProfile, ILoginRegisterRepository _loginRegisterRepository, VacationManagerDbContext _vacationManagerDbContext)
        {
			this.mappingProfile = _mappingProfile;
			this.loginRegisterRepository = _loginRegisterRepository;
			this.vacationManagerDbContext = _vacationManagerDbContext;
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

				User user = this.mappingProfile.Map<User>(model);
				try
				{
					loginRegisterRepository.Register(user);
					Logged.User = user;
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
		[HttpPost]
		public IActionResult Login(LoginUserViewModel model)
		{
			if (ModelState.IsValid)
			{
				try
				{
					string username = model.Username;
					string password = model.Password;
					this.loginRegisterRepository.Login(username, password);
				}
				catch (Exception)
				{
					ModelState.AddModelError("loginError", "Невалидно потребителско име или парола");
					return View(model);
				}
			}
			else
			{
				return View(model);
			}

			return RedirectToAction("Index", "Home");
		}
	}
}
