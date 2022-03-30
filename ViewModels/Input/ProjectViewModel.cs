using Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ViewModels.Input
{
	public class ProjectViewModel
	{
		[Required(ErrorMessage = "Името на проекта не може да е празно")]
		public string Name { get; set; }

		[Required(ErrorMessage = "Описанието на проекта не може да е празно")]
		public string Description { get; set; }

		public List<Team> Teams { get; set; }
	}
}
