using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ViewModels.Input
{
	public class TeamViewModel
	{
		[Required(ErrorMessage = "Името на екипа е задължително")]
		public string Name { get; set; }

		[Required(ErrorMessage = "Името на проекта е задължително")]
		public string ProjectName { get; set; }

		public string DevelopersUsernames { get; set; }

		[Required(ErrorMessage = "Името на лидера на отбора е задължително")]
		public string TeamLeaderUsername { get; set; }
	}
}
