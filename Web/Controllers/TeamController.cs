using Microsoft.AspNetCore.Mvc;
using ViewModels.Input;

namespace Web.Controllers
{
	public class TeamController : Controller
	{
		[HttpGet]
		public IActionResult Create()
		{
			TeamViewModel model = new TeamViewModel();

			return View(model);
		}
	}
}
