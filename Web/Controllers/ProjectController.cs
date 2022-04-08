using Microsoft.AspNetCore.Mvc;
using Models;
using Repositories;
using Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ViewModels.Input;

namespace Web.Controllers
{
	public class ProjectController : Controller
	{
		private IProjectRepository _projects;

		public ProjectController(IProjectRepository projects)
		{
			this._projects = projects;
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
