using System.ComponentModel.DataAnnotations;

namespace Company.Seif.PL.DTOS
{
	public class ForgetPasswordDto
	{
		[Required(ErrorMessage ="Email is Required !")]
		[EmailAddress]
		public string Email { get; set; }
	}
}
