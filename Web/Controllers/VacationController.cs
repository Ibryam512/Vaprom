using Microsoft.AspNetCore.Mvc;
using Models.Enums;
using Repositories.Helpers;
using Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ViewModels.DTO;
using ViewModels.Input;
using Web.Services.Interfaces;

namespace Web.Controllers
{
	public class VacationController : Controller
	{
		private IVacationRepository _vacation;
		private Dictionary<string, VacationType> vacationTypes = new Dictionary<string, VacationType>();
    private readonly IVacationDocumentService _documentService;


		public VacationController(IVacationRepository vacation, IVacationDocumentService documentService)
		{
			this._vacation = vacation;
			vacationTypes.Add("Болничен", VacationType.Sick);
			vacationTypes.Add("Платен", VacationType.Paid);
			vacationTypes.Add("Неплатен", VacationType.Unpaid);
      
      this._documentService = documentService ?? throw new ArgumentNullException(nameof(documentService));
		}

		[HttpGet]
		public IActionResult Index()
		{
			return View();
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

		[HttpGet]
		public IActionResult Edit()
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
    
		public FileResult DownloadFile(string fileName)
        {
			//когато е готов LoggedUser-а ще се махне коментара
            //this._documentService.GenerateDocument();

            string path = Path.Combine("Files/") + fileName;

            byte[] bytes = System.IO.File.ReadAllBytes(path);
            System.IO.File.Delete(path);
 
            return File(bytes, "application/pdf", "document.pdf");
        }
	}
}
