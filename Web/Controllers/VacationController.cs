using Microsoft.AspNetCore.Mvc;
using Models.Enums;
using Repositories.Helpers;
using Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ViewModels.DTO;
using ViewModels.Input;

namespace Web.Controllers
{
	public class VacationController : Controller
	{
		private IVacationRepository _vacation;
		private Dictionary<string, VacationType> vacationTypes = new Dictionary<string, VacationType>();

		public VacationController(IVacationRepository vacation)
		{
			this._vacation = vacation;
			vacationTypes.Add("Болничен", VacationType.Sick);
			vacationTypes.Add("Платен", VacationType.Paid);
			vacationTypes.Add("Неплатен", VacationType.Unpaid);
		}

		[HttpGet]
		public IActionResult Create()
		{
			VacationViewModel model = new VacationViewModel
			{
				ApplicantUsername = Logged.User.UserName,
				ApplicantName = Logged.User.FirstName,
				ApplicantSurname = Logged.User.LastName,
				ApplicantTeam = Logged.User.Team.Name,
				FromDate = DateTime.Today,
				ToDate = DateTime.Today
			};

			return View(model);
		}

		[HttpPost]
		public IActionResult Create(VacationViewModel model)
		{
			VacationDTO vacation = new VacationDTO
			{
				VacationType = this.vacationTypes[model.VacationType],
				FromDate = model.FromDate,
				ToDate = model.ToDate,
				IsApproved = false,
				IsHalfDay = model.IsHalfDay,
				ApplicantUsername = model.ApplicantUsername,
				FilePath = model.FilePath
			};

			return View();
		}
	}
}
