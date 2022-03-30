using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ViewModels.Input;

namespace Web.Controllers
{
	public class ProjectController : Controller
	{
		[HttpGet]
		public IActionResult Create()
		{
			ProjectViewModel model = new ProjectViewModel();

			return View(model);
		}
	}
}
