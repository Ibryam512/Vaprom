using AutoMapper;
using DataAccess;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.SearchModel;
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
		private ITeamService teamService;
		private IUserService userService;

		public UserController(IMapper _mappingProfile, ILoginRegisterRepository _loginRegisterRepository, IRoleService _roleService,
			ITeamService _teamService, IUserService _userService)
		{
			this.mappingProfile = _mappingProfile;
			this.loginRegisterRepository = _loginRegisterRepository;
			this.roleService = _roleService;
			this.teamService = _teamService;
			this.userService = _userService;
		}

		[HttpGet]
		public IActionResult Index()
		{
			UserSearch search = new UserSearch();
			search.Results = this.userService.GetUsers();
			return View(search);
		}

		[HttpGet]
		public IActionResult Search(UserSearch search)
		{
			search.Results = this.userService.GetUsers();
			if (search.FirstName is not null)
			{
				search.Results = search.Results.Where(x => x.FirstName.Contains(search.FirstName)).ToList();
			}
			if (search.LastName is not null)
			{
				search.Results = search.Results.Where(x => x.LastName.Contains(search.LastName)).ToList();
			}
			if (search.Role != "Избери роля...")
			{
				search.Results = search.Results.Where(x => x.Role.Name == search.Role).ToList();
			}
			if (search.Team != "Избери екип...")
			{
				search.Results = search.Results.Where(x => x.Team.Name == search.Team).ToList();
			}

			return View("Index", search);
		}

		#region Register

		[HttpGet]
		public IActionResult Register()
		{
			if (Logged.CEOAuth())
			{
				RegisterUserViewModel model = new RegisterUserViewModel();

				return View(model);
			}
			else
				return Unauthorized();
			
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
					user.TeamId = "0";
					user.Team = teamService.GetTeam("0");
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

		#endregion

		#region Login

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

		#endregion

		[HttpGet]
		[Route("user/profile/{username}")]
		public IActionResult Profile([FromRoute]string username)
		{
			User user = userService.GetUser(username);

			return View(user);
		}

		[HttpGet]
		[Route("user/delete/{username}")]
		public IActionResult Delete([FromRoute] string username)
		{
			if(Logged.CEOAuth())
			{
				User user = userService.GetUser(username);

				this.userService.DeleteUser(user);
				return RedirectToAction("Index", "User");
			}
			else
			{
				return Unauthorized();
			}
		}
	}
}
