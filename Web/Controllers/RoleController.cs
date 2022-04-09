﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Models;
using Repositories.Interfaces;
using Repositories.Mapper;
using Models.SearchModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ViewModels.Input;
using Web.Services.Interfaces;

namespace Web.Controllers
{
	public class RoleController : Controller
	{

		private IMapper _mapper;
		private IRoleService _roleService;
		public RoleController(IMapper mapper, IRoleService roleService)
		{
			this._mapper = mapper;
			this._roleService = roleService;
		}

		#region CreateRole
		[HttpGet]
		public IActionResult Create()
		{
			RoleViewModel model = new RoleViewModel();
			return View(model);
		}

		[HttpPost]
		public IActionResult Create(RoleViewModel model)
		{
			if (ModelState.IsValid)
			{
				Role role = this._mapper.Map<Role>(model);
				_roleService.AddRole(role);
				return RedirectToAction("Index", "Role");
			}
			else
			{
				return View(model);
			}
		}
		#endregion

		#region EditRole
		[HttpGet]
		[Route("post/edit/{id}")]
		public IActionResult Edit([FromRoute] string id)
		{
			if (string.IsNullOrEmpty(id))
			{
				return NotFound();
			}
			EditRoleViewModel model = new EditRoleViewModel();
			model.Id = id;
			return View(model);
		}

		[HttpPost]
		public IActionResult Edit(RoleViewModel model)
		{
			if (ModelState.IsValid)
			{
				Role role = this._mapper.Map<Role>(model);
				_roleService.DeleteRole(_roleService.GetRole(role.Id));
				_roleService.AddRole(role);
				return RedirectToAction("Index", "Role");
			}
			else
			{
				return View(model);
			}
		}
		#endregion

		#region DeleteRole
		[HttpGet]
		[Route("role/delete/{id}")]
		public IActionResult Delete([FromRoute] string id)
		{
			if (id is null)
			{
				return NotFound();
			}
			Role deleteRole = _roleService.GetRole(id);
			if (deleteRole is null)
			{
				return NotFound();
			}
			_roleService.DeleteRole(deleteRole);
			return RedirectToAction("Index", "Role");

		}
		#endregion


		[HttpGet]
		public IActionResult Index()
		{
			RoleSearch search = new RoleSearch();
			search.Roles = this._roleService.GetRoles();
			return View(search);
		}
		[HttpGet]
		public IActionResult Search(RoleSearch model)
		{
			model.Roles = this._roleService.GetRoles();
			if (model.Name is not null)
			{
				model.Roles = model.Roles.Where(x => x.Name.Contains(model.Name)).ToList();
			}
			return View("Index", model);
		}
	}
}

