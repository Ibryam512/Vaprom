using System.ComponentModel.DataAnnotations;

namespace ViewModels.Input
{
	public class LoginUserViewModel
	{
		[Required(ErrorMessage = "Потребителското име е задължително")]
		public string Username { get; set; }

		[Required(ErrorMessage = "Паролата е задължителна")]
		public string Password { get; set; }
	}
}
