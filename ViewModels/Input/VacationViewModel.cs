using Models.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace ViewModels.Input
{
	public class VacationViewModel
	{
		[Required(ErrorMessage = "Типа на отпуската не може да е празен")]
		public VacationType VacationType { get; set; }
		
		[Required(ErrorMessage = "Датата за начало на отпуската е задължителна")]
		public DateTime FromDate { get; set; }

		[Required(ErrorMessage = "Датата за края на отпуската е задължителна")]
		public DateTime ToDate { get; set; }

		public bool IsHalfDay { get; set; }

		public bool IsApproved { get; set; }

		[Required(ErrorMessage = "Заявителят е задължителен")]
		public string ApplicantUsername { get; set; }

		public string FilePath { get; set; }
	}
}
