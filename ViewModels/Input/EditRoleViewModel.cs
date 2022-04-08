using System.ComponentModel.DataAnnotations;

namespace ViewModels.Input
{
	public class EditRoleViewModel
	{
		[Required(ErrorMessage = "Името на ролята е задължително")]
		public string Name { get; set; }
		public string Id { get; set; }
	}
}