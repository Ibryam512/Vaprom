using Microsoft.AspNetCore.Mvc;
using Models.SearchModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Services.Interfaces;

namespace Web.Controllers
{
	public class RoleController : Controller
	{
		private IRoleService _roleService;
		public RoleController(IRoleService roleService)
        {
			this._roleService=roleService;
        }
		[HttpGet]
		public IActionResult Create()
		{
			return View();
		}
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
			if(model.Name is not null)
            {
				model.Roles = model.Roles.Where(x => x.Name.Contains(model.Name)).ToList();
			}
			return View("Index", model);
		}
	}
}
