using System.ComponentModel.DataAnnotations;

namespace ViewModels.Input
{
	public class RoleViewModel
	{
		[Required(ErrorMessage = "Името на ролята е задължително")]
		public string Name { get; set; }
	}
}