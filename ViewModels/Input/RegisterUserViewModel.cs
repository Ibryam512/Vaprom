using System.ComponentModel.DataAnnotations;
using ViewModels.DTO;

namespace ViewModels.Input
{
	public class RegisterUserViewModel
	{
		[Required(ErrorMessage = "Потребителското име е задължително")]
		public string Username { get; set; }

		[Required(ErrorMessage = "Паролата е задължителна")]
		public string Password { get; set; }

		[Required(ErrorMessage = "Собственото име е задължително")]
		public string FirstName { get; set; }

		[Required(ErrorMessage = "Фамилията е задължителна")]
		public string LastName { get; set; }

		[Required(ErrorMessage = "Ролята е задължителна")]
		public RoleDTO Role { get; set; }

		public TeamDTO Team { get; set; }
	}
}
