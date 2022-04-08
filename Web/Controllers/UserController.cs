using AutoMapper;
using DataAccess;
using Microsoft.AspNetCore.Mvc;
using Models;
using Repositories.Helpers;
using Repositories.Interfaces;
using Repositories.Mapper;
using System;
using System.Linq;
using ViewModels.Input;
using Web.Services.Interfaces;

namespace Web.Controllers
{
	public class UserController : Controller
	{
		private ILoginRegisterRepository loginRegisterRepository;
		private IRoleService roleService;
		private IMapper mappingProfile;
		public UserController(IMapper _mappingProfile, ILoginRegisterRepository _loginRegisterRepository, IRoleService _roleService)
        {
			this.mappingProfile = _mappingProfile;
			this.loginRegisterRepository = _loginRegisterRepository;
			this.roleService = _roleService;
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
				var roles = this.roleService.GetRoles();
				user.Role = roles.SingleOrDefault(x => x.Name == model.RoleName);
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

		[HttpGet]
		public IActionResult Profile()
		{
			return View();
		}
	}
}
