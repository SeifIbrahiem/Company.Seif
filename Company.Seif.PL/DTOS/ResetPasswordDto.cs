using System.ComponentModel.DataAnnotations;

namespace Company.Seif.PL.DTOS
{
	public class ResetPasswordDto
	{
		[Required(ErrorMessage = "Password is Required !!")]
		[DataType(DataType.Password)] //*****
		public string NewPassword {  get; set; }
		[DataType(DataType.Password)]  //*****
		[Required(ErrorMessage = "Password is Required !!")]
		[Compare(nameof(NewPassword), ErrorMessage = "Confirm Password does not match the password !!")]
		public string ConfirmPassword { get; set; }
	}
}
