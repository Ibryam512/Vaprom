using System.ComponentModel.DataAnnotations;

namespace ViewModels
{
	public class RoleDTO
	{
		[Required(ErrorMessage = "Името на ролята е задължително")]
		public string Name { get; set; }
	}
}