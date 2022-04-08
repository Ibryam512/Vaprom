﻿using AutoMapper;
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

		private readonly IProjectService _projectService;
		private readonly IMapper _mapper;

		public ProjectController(IProjectService projectService, IMapper mapper)
		{
			this._projectService = projectService ?? throw new ArgumentNullException(nameof(projectService));
			this._mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
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
			search.Results = this._projects.GetProjects();
			if (search.Name is not null)
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

		[HttpPost]
		public IActionResult Create(ProjectViewModel viewModel)
		{
			var project = this._mapper.Map<Project>(viewModel);
			this._projectService.AddProject(project);
			return View();
		}

		[HttpGet]
		[Route("Project/Details/{id}")]
		public IActionResult Details([FromRoute] string id)
		{
			Project project = this._projectService.GetProject(id);

			ProjectViewModel model = new ProjectViewModel
			{
				Name = project.Name,
				Description = project.Description,
				Teams = project.Teams
			};

			return View(model);
		}

		[HttpGet("Project/Edit/{id}")]
		public IActionResult Edit(string id)
		{
			var project = this._projectService.GetProject(id);
			var projectViewModel = this._mapper.Map<ProjectViewModel>(project);

			return View(projectViewModel);
		}

		[HttpPost]
		public IActionResult Edit(ProjectViewModel viewModel)
		{
			var project = this._mapper.Map<Project>(viewModel);
			this._projectService.EditProject(project);

			return View();
		}
	}
}
