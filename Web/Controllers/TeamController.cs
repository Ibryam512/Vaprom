using Microsoft.AspNetCore.Mvc;
using Models.SearchModel;
using System.Linq;
using ViewModels.Input;
using Web.Services.Interfaces;

namespace Web.Controllers
{
	public class TeamController : Controller
	{
		private ITeamService teamService;
		public IUserService userService;

		public TeamController(ITeamService _teamService, IUserService _userService)
		{
			this.teamService = _teamService;
			this.userService = _userService;
		}


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
				search.Results = search.Results.Where(x => x.TeamLeader.FirstName.Contains(search.TeamLeadNames) || x.TeamLeader.LastName.Contains(search.TeamLeadNames)).ToList();
			}

			return View("Index", search);
		}

		[HttpGet]
		public IActionResult Create()
		{
			TeamViewModel model = new TeamViewModel();

			return View(model);
		}
	}
}
