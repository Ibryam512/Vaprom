using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Models;
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
		private readonly IVacationService _vacationService;
		private readonly IVacationDocumentService _documentService;
		private readonly IMapper _mapper;
		private IWebHostEnvironment _webHostEnv;
		private Dictionary<string, VacationType> vacationTypes = new Dictionary<string, VacationType>();

		public VacationController(IVacationService vacationService, IVacationDocumentService documentService, IMapper mapper, IWebHostEnvironment webHostEnvironment)
		{
			this._vacationService = vacationService ?? throw new ArgumentNullException(nameof(vacationService));
			this._documentService = documentService ?? throw new ArgumentNullException(nameof(documentService));
			this._mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

			vacationTypes.Add("Болничен", VacationType.Sick);
			vacationTypes.Add("Платен", VacationType.Paid);
			vacationTypes.Add("Неплатен", VacationType.Unpaid);
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
				//ApplicantTeam = Logged.User.Team.Name,
				FromDate = DateTime.Today,
				ToDate = DateTime.Today
			};

			return View(model);
		}

		[HttpPost]
		public IActionResult Create(VacationViewModel model)
		{
			model.VacationType = vacationTypes[model.VacationTypeText];
			Vacation vacation = this._mapper.Map<Vacation>(model);
			vacation.Applicant = Logged.User;

			if (model.File != null)
            {
				string Pathern = Path.Combine(_webHostEnv.WebRootPath, "Files");
				string fileName = Guid.NewGuid() + "-" + model.File.FileName;
				string filePathern = Path.Combine(Pathern, fileName);

				using (var fileStream = new FileStream(filePathern, FileMode.Create))
				{
					model.File.CopyTo(fileStream);
				}
				vacation.FilePath = filePathern;
			}
			else
            {
				vacation.FilePath = null;
            }
					
			this._vacationService.AddVacation(vacation);
			return RedirectToAction("Index", "Home");
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

		[HttpPost]
		public IActionResult Edit(VacationViewModel model)
		{
			Vacation vacation = this._mapper.Map<Vacation>(model);

			if (model.File!=null)
            {
				string Pathern = Path.Combine(_webHostEnv.WebRootPath, "Files");
				string fileName = Guid.NewGuid() + "-" + model.File.FileName;
				string filePathern = Path.Combine(Pathern, fileName);

				using (var fileStream = new FileStream(filePathern, FileMode.Create))
				{
					model.File.CopyTo(fileStream);
				}
				vacation.FilePath = filePathern;
			}
			else
            {
				vacation.FilePath = null;
            }
					
			this._vacationService.EditVacation(vacation);
			return RedirectToAction("Index", "Home");
		}
    
		public FileResult DownloadFile(string fileName)
        {
            //this._documentService.GenerateDocument(Logged.User);

            string path = Path.Combine("Files/") + fileName;

            byte[] bytes = System.IO.File.ReadAllBytes(path);
            System.IO.File.Delete(path);
 
            return File(bytes, "application/pdf", "document.pdf");
        }
	}
}
