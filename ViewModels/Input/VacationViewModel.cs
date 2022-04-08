using Microsoft.AspNetCore.Http;
using Models.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace ViewModels.Input
{
	public class VacationViewModel
	{
		[Required(ErrorMessage = "Типа на отпуската не може да е празен")]
		public string VacationTypeText { get; set; }
		public VacationType VacationType { get; set; }
		
		[Required(ErrorMessage = "Датата за начало на отпуската е задължителна")]
		public DateTime FromDate { get; set; }

		[Required(ErrorMessage = "Датата за края на отпуската е задължителна")]
		public DateTime ToDate { get; set; }

		public bool IsHalfDay { get; set; }

		public ApprovalStatus Status { get; set; }

		public string ApplicantUsername { get; set; }

		public string ApplicantName { get; set; }

		public string ApplicantSurname { get; set; }

		public string ApplicantTeam { get; set; }

		public string FilePath { get; set; }
		public IFormFile File { get; set; }
	}
}
