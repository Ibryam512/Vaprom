using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Web.Services.Interfaces;

namespace Web.Controllers
{
	public class VacationController : Controller
	{
		private readonly IVacationDocumentService _documentService;

		public VacationController(IVacationDocumentService documentService)
		{
			this._documentService = documentService ?? throw new ArgumentNullException(nameof(documentService));
		}

		public IActionResult Index()
		{
			return View();
		}

		public IActionResult Create()
		{
			return View();
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
