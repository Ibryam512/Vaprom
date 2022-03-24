﻿using Models.Enums;
using System;

namespace ViewModels.DTO
{
	public class VacationDTO
	{
		public VacationType VacationType { get; set; }
		
		public DateTime FromDate { get; set; }

		public DateTime ToDate { get; set; }

		public bool IsHalfDay { get; set; }

		public bool IsApproved { get; set; }

		public string ApplicantUsername { get; set; }

		public string FilePath { get; set; }
	}
}