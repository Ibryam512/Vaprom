using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Models;
using Repositories.Interfaces;
using Repositories.Mapper;
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
		[HttpGet]
		public IActionResult Index()
		{
			return View();
		}

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

		[HttpGet]
		[Route("post/delete/{id}")]
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
	}
}
