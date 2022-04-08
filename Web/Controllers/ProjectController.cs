using Microsoft.AspNetCore.Mvc;
using Models;
using Models.SearchModel;
using Repositories;
using Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ViewModels.Input;
using Web.Services.Interfaces;

namespace Web.Controllers
{
	public class ProjectController : Controller
	{
		private IProjectService _projects;

		public ProjectController(IProjectService projects)
		{
			this._projects = projects;
		}

		[HttpGet]
		public IActionResult Index()
		{
			ProjectSearch search = new ProjectSearch();
			search.Results = this._projects.GetProjects();
			return View(search);
		}
		[HttpGet]
		public IActionResult Search(ProjectSearch search)
        {
			if(search.Name is not null)
            {
				search.Results=search.Results.Where(x=>x.Name.Contains(search.Name)).ToList();
            }

			if(search.Description is not null)
            {
				search.Results = search.Results.Where(x => x.Description.Contains(search.Description)).ToList();
			}

			return View("Index", search);
		}

		[HttpGet]
		public IActionResult Create()
		{
			ProjectViewModel model = new ProjectViewModel();

			return View(model);
		}

		[HttpGet]
		[Route("project/details/{id}")]
		public IActionResult Details([FromRoute] string id)
		{
			Project project = this._projects.GetProject(id);

			ProjectViewModel model = new ProjectViewModel
			{
				Name = project.Name,
				Description = project.Description,
				Teams = project.Teams
			};

			return View(model);
		}
	}
}
