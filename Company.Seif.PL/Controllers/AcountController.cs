﻿using Company.Seif.DAL.Models;
using Company.Seif.PL.DTOS;
using Company.Seif.PL.Helbers;
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

		#region Forget password
		[HttpGet]

		public IActionResult ForgetPassword()
		{ 
			return View(); 
	    }
		[HttpPost]
        public async Task<IActionResult> SendResetPasswordUrl(ForgetPasswordDto model)
        {
			if (ModelState.IsValid)
			{
				var user = await _userManager.FindByEmailAsync(model.Email);
				if (user is not null)
				{
					//Generate Token
					var token =await _userManager.GeneratePasswordResetTokenAsync(user);

					//Create Url

    var url =  Url.Action("ResetPassword" , "Account", new { email = model.Email, token } , Request.Scheme);

					// Create Email
					var email = new Email()
					{
						To = model.Email,
						Subject = "Reset Password",
						Body = url
					};
					//Send Email
					var flag = EmailSetting.SendEmail(email);
					if (flag)
					{
						//Cheeck your Inbox
						return RedirectToAction("CheeckyourInbox");
					}
				}
			}
				ModelState.AddModelError("", "Invalid Reset Password Operation !!");
               return View("ForgetPassword",model);
        }
		[HttpGet]
		public IActionResult CheeckyourInbox()
		{
			return View();
		}


		#endregion


		#region  Reset Password
		[HttpGet]
		public IActionResult ResetPassword(string email , string token)
		{
			TempData["email"]=email;
			TempData["token"]=token;
			return View();
		}

		[HttpPost]
		public async Task <IActionResult> ResetPassword(ResetPasswordDto model)
		{
			if (ModelState.IsValid)
			{
				var email = TempData["email"] as string;
				var token = TempData["token"] as string;
				if (email is null || token is null) return BadRequest("Invalid Operation");
				var user = await _userManager.FindByNameAsync(email);
				if (user is not null)
				{
					var result = await _userManager.ResetPasswordAsync(user,token,model.NewPassword);
					if (result.Succeeded)
					{
						return RedirectToAction("SignIn");
					}
				}
				ModelState.AddModelError("", "Invalid Reset Password Operation");
			}
			return View();
		}






		#endregion

	}
}
