using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Models;

using Models.SearchModel;
using Repositories.Helpers;
using System.Collections.Generic;
using System.Linq;

using ViewModels.Input;
using Web.Services.Interfaces;
using System.Linq;
using System.Collections.Generic;
using Models.SearchModel;

namespace Web.Controllers
{
	public class TeamController : Controller
	{

		private IMapper mapper;
		private ITeamService teamService;
		private IUserService userService;
		private IProjectService projectService;
		public TeamController(IMapper mapper, ITeamService teamService, IUserService userService, IProjectService projectService)
		{
			this.mapper = mapper;
			this.teamService = teamService;
			this.userService = userService;
			this.projectService = projectService;
		}

		#region CreateTeam


		[HttpGet]
		public IActionResult Index()
		{
			TeamSearch search = new TeamSearch();


			search.Results = this.teamService.GetTeams();
			search.Results.ForEach(x => x.TeamLeader = this.userService.GetUserById(x.TeamLeaderId));



			return View(search);
		}

		[HttpGet]
		public IActionResult Search(TeamSearch search)
		{

			search.Results = this.teamService.GetTeams();
			search.Results.ForEach(x => x.TeamLeader = this.userService.GetUserById(x.TeamLeaderId));


			if (search.Name is not null)
			{
				search.Results = search.Results.Where(x => x.Name.Contains(search.Name)).ToList();
			}
			if (search.TeamLeadNames is not null)
			{
				search.Results = search.Results.Where(x => x.TeamLeader is not null && (x.TeamLeader.FirstName.Contains(search.TeamLeadNames) || x.TeamLeader.LastName.Contains(search.TeamLeadNames))).ToList();
			}

			return View("Index", search);
		}

		#region CreateTeam
		[HttpGet]
		public IActionResult Create()
		{
			TeamViewModel model = new TeamViewModel();
			return View(model);
		}

		[HttpPost]
		public IActionResult Create(TeamViewModel model)
		{
			if (ModelState.IsValid)
			{
				Team team = this.mapper.Map<Team>(model);
				List<User> teamMembers = new List<User>();
				User teamLeader = userService.GetUsers().FirstOrDefault(x => x.UserName == model.TeamLeaderUsername);
				List<string> usernames = model.DevelopersUsernames.Split(' ').ToList();
				foreach (string username in usernames)
				{
					teamMembers.Add(userService.GetUser(username));
				}
				team.TeamLeader = teamLeader;
				team.Developers = teamMembers;
				team.Project = projectService.GetProjects().FirstOrDefault(x => x.Name == model.ProjectName);
				teamService.AddTeam(team);

				return RedirectToAction("Index", "Team");
			}
			else
			{
				return View(model);
			}
		}
		#endregion

		[HttpGet]
		[Route("team/delete/{id}")]
		public IActionResult Delete([FromRoute] string id)
		{
			if (Logged.CEOAuth())
			{
				Team team = teamService.GetTeam(id);
				foreach (var user in userService.GetUsers())
				{
					if (user.TeamId == id) user.TeamId = null;
				}

				this.teamService.DeleteTeam(team);
				return RedirectToAction("Index", "Team");
			}
			else
			{
				return Unauthorized();
			}
		}

	}
}
