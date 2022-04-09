using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.Enums;
using Models.SearchModel;
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
			this._webHostEnv = webHostEnvironment;

			vacationTypes.Add("Болничен", VacationType.Sick);
			vacationTypes.Add("Платен", VacationType.Paid);
			vacationTypes.Add("Неплатен", VacationType.Unpaid);
		}

		[HttpGet]
		public IActionResult Index()
		{
			VacationSearch search = new VacationSearch();
			search.Result = _vacationService.GetVacations();
			return View(search);
		}
		[HttpGet]
		public IActionResult Search(VacationSearch search)
        {
			if (search.FromDate != default(DateTime))
            {
				search.Result = search.Result.Where(x => x.CreationDate < search.FromDate).ToList();
			}
			else
            {
				search.Result = new List<Vacation>();
            }
			return View("Index", search);

		}
    
    	[HttpGet]
		public IActionResult Create()
		{
			VacationViewModel model = new VacationViewModel
			{
				ApplicantUsername = Logged.User.UserName,
				ApplicantName = Logged.User.FirstName,
				ApplicantSurname = Logged.User.LastName,
				//ApplicantTeam = Logged.User.Team.Name ?? "No Team",
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
			vacation.ApplicantId = Logged.User.Id;

			if (model.File != null)
            {
				string Pathern = Path.Combine(_webHostEnv.WebRootPath, "Files");
				string fileName = Guid.NewGuid() + "-" + model.File.FileName;
				string filePathern = Path.Combine(Pathern, fileName);

				using (var fileStream = new FileStream(filePathern, FileMode.Create))
				{
					model.File.CopyTo(fileStream);
				}
				vacation.FilePath = Path.Combine("Files", fileName);
			}
			else
            {
				vacation.FilePath = null;
            }
			
			this._vacationService.AddVacation(vacation);
			return RedirectToAction("Index", "Home");
		}

		[HttpGet("Vacation/Edit/{id}")]
		public IActionResult Edit(string id)
		{
			var vacation = this._vacationService.GetVacation(id);

			VacationViewModel model = new VacationViewModel
			{
				VacationType = vacation.VacationType,
				IsHalfDay = vacation.IsHalfDay,
				Status = vacation.Status,
				ApplicantUsername = Logged.User.UserName,
				ApplicantName = Logged.User.FirstName,
				ApplicantSurname = Logged.User.LastName,
				//ApplicantTeam = Logged.User.Team.Name ?? "No Team",
				FromDate = vacation.FromDate,
				ToDate = vacation.ToDate,
				FilePath = vacation.FilePath
			};

			return View(model);
		}

		[HttpPost("Vacation/Edit/{id}")]
		public IActionResult Edit(VacationViewModel model, string id)
		{
			model.VacationType = vacationTypes[model.VacationTypeText];
			Vacation vacation = this._mapper.Map<Vacation>(model);
			vacation.Id = id;
			vacation.ApplicantId = Logged.User.Id;
					
			this._vacationService.EditVacation(vacation);
			return RedirectToAction("Index", "Vacation");
		}

		[HttpGet("Vacation/approval/{id}")]
		public IActionResult Approval(string id)
		{
			if (Logged.CEOAuth())
			{	
				var vacation = this._vacationService.GetVacation(id);
				if(vacation.Applicant.Team!=null)
                {
					if(vacation.Applicant.Team.TeamLeader==Logged.User)
                    {
						var vacationDto = _mapper.Map<VacationDTO>(vacation);
						vacationDto.ApplicantUsername = vacation.Applicant.UserName;
						vacationDto.ApplicantName = vacation.Applicant.FirstName;
						vacationDto.ApplicantSurname = vacation.Applicant.LastName;
						//vacationDto.ApplicantTeam = vacation.Applicant.Team.Name ?? "No team";
						return View(vacationDto);
					}
				}
			}
				return Unauthorized();
			
		}

		[HttpPost]
		[Route("Vacation/Approve/{id}")]
		public IActionResult Approve(VacationDTO vacationDto)
		{
			var vacation = this._mapper.Map<Vacation>(vacationDto);
			vacation.Status = Models.Enums.ApprovalStatus.Approved;

			return Index();
		}

		[HttpPost]
		[Route("Vacation/Disapprove/{id}")]
		public IActionResult Disapprove(VacationDTO vacationDto)
		{
			var vacation = this._mapper.Map<Vacation>(vacationDto);
			vacation.Status = Models.Enums.ApprovalStatus.Disapproved;

			return Index();
		}

		[HttpGet]
		[Route("Vacation/Delete/{id}")]
		public IActionResult Delete([FromRoute] string id)
		{
			if (Logged.CEOAuth())
			{
				Vacation vacation = this._vacationService.GetVacation(id);

				this._vacationService.DeleteVacation(vacation);
				return RedirectToAction("Index", "Vacation");
			 }
			else
			 {
				return Unauthorized();
			 }
		}
    
		[Route("Vacation/{id}/Download")]
		public FileResult DownloadFile(string id, string fileName)
        {
			var vacation = this._vacationService.GetVacation(id);
            this._documentService.GenerateDocument(Logged.User, vacation);
            string path = Path.Combine(_webHostEnv.WebRootPath,"Files",fileName);
			
            byte[] bytes = System.IO.File.ReadAllBytes(path);
            System.IO.File.Delete(path);
 
            return File(bytes, "application/octet-stream", "document.docx");
        }
	}
}
