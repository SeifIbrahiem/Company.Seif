using System.ComponentModel.DataAnnotations;

namespace Company.Seif.PL.DTOS
{
	public class SignInDto
	{
		
		[EmailAddress]
		[Required(ErrorMessage = "Email is Required !!")]
		public string Email { get; set; }
		[Required(ErrorMessage = "Password is Required !!")]
		[DataType(DataType.Password)] //*****
		public string Password { get; set; }

		public bool RememberMe { get; set; }
	}
}
