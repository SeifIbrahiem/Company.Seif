using Company.Seif.DAL.Models;
using Company.Seif.PL.DTOS;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Company.Seif.PL.Controllers
{
    public class AcountController : Controller
    {
		private readonly UserManager<AppUser> _userManager;
		private readonly SignInManager<AppUser> _signInManager;

		public AcountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager) 
		{
			_userManager = userManager;
			_signInManager = signInManager;
		}




        #region SignUp
        [HttpGet]   //Get: /Acount/SignUp
		public IActionResult SignUp()
        {
            return View();
        }
		//P@ssW0rd
		[HttpPost]
		public async Task<IActionResult> SignUp(SignUpDto model)
		{
			if (ModelState.IsValid) //server side validation
			{

	             var user = await _userManager.FindByNameAsync(model.UserName);
				if (user is null) 
				{
				  user = await _userManager.FindByEmailAsync(model.Email);
					if (user is null)
					{
						//Register
						//mappng from signuodto to appuser
					      user = new AppUser()
						{
							UserName = model.UserName,
							FirstName = model.FirstName,
							LastName = model.LastName,
							Email = model.Email,
							IsAgree = model.IsAgree,

						};
						var result = await _userManager.CreateAsync(user, model.Password);
						if (result.Succeeded)
						{
							//send email to confirm email
							return RedirectToAction("SignIn");
						}
						foreach (var error in result.Errors)
						{
							ModelState.AddModelError("", error.Description);
						}

					}

				}
				ModelState.AddModelError("", "Invalid signup !!");
			}
			return View(model);
		}



		#endregion

		#region SignIn
		[HttpGet]
		public IActionResult SignIn() 
		{ 
			return View(); 
		}
		[HttpPost]
		public async Task<IActionResult> SignIn(SignInDto model)
		{
			if (ModelState.IsValid) 
			{
				var user = await _userManager.FindByEmailAsync(model.Email);
				if (user is not null) 
				{ 
					var flag = await _userManager.CheckPasswordAsync(user, model.Password);
					if (flag) 
					{
						//SignIn
					var result = await	_signInManager.PasswordSignInAsync( user, model.Password , model.RememberMe , false );
						if (result.Succeeded)
						{
							return RedirectToAction(nameof(HomeController.Index), "Home");
						}
					}
                    ModelState.AddModelError("", "Invalid Login !");
                }
				ModelState.AddModelError("", "Invalid Login !");
			}
			return View();
		}




		#endregion

		#region SignOut

		[HttpGet]
		public new  async Task<IActionResult> SignOut()
		{
			await _signInManager.SignOutAsync();
			return RedirectToAction(nameof(SignIn));
		}





		#endregion

	}
}
