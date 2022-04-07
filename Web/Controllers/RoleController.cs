using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Controllers
{
	public class RoleController : Controller
	{
		[HttpGet]
		public IActionResult Create()
		{
			return View();
		}
	}
}
