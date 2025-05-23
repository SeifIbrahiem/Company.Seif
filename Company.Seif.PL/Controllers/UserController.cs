﻿using Company.Seif.BLL;
using Company.Seif.BLL.Interfaces;
using Company.Seif.DAL.Models;
using Company.Seif.PL.DTOS;
using Company.Seif.PL.Helbers.Company.Seif.PL.Helbers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;

namespace Company.Seif.PL.Controllers
{
	public class UserController : Controller
	{
		private readonly UserManager<AppUser> _userManager;
		public UserController(UserManager<AppUser>userManager) 
		{ 
		   _userManager = userManager;
		}
		[HttpGet]
		public async Task<IActionResult> Index(string? SearchInput)
		{
			IEnumerable<UserToReturnDto> users;
			if (string.IsNullOrEmpty(SearchInput))
			{
				users =_userManager.Users.Select(U => new UserToReturnDto()
				{
					Id = U.Id,
					UserName=U.UserName,
					Email=U.Email,
					FirstName = U.FirstName,
					LastName = U.LastName,
					Roles = _userManager.GetRolesAsync(U).Result

				});
			}
			else
			{
				users = _userManager.Users.Select(U => new UserToReturnDto()
				{
					Id = U.Id,
					UserName = U.UserName,
					Email = U.Email,
					FirstName = U.FirstName,
					LastName = U.LastName,
					Roles = _userManager.GetRolesAsync(U).Result

				}).Where(U => U.FirstName.ToLower().Contains(SearchInput.ToLower()));
			}
			return View(users);
		}

        [HttpGet]

        public async Task<IActionResult> Detailes(string? id , string viewName = "Detailes")
        {
            if (id is null) return BadRequest("Invalid Id"); //400
            var user = await _userManager.FindByIdAsync(id);
            if (user is null) return NotFound(new { statueCode = 404, Message = $"User With Id : {id} is not found" });
            var dto = new UserToReturnDto()
            {
                Id = user.Id,
                UserName=user.UserName,
                Email=user.Email,
                FirstName=user.FirstName,
                LastName=user.LastName,
                Roles=_userManager.GetRolesAsync(user).Result
            };
            return View(viewName, dto);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(string?id)
        {
            return await Detailes(id , "Edit");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] string id, UserToReturnDto model)
        {
            if (ModelState.IsValid)
            {
                if (id != model.Id) return BadRequest("Invalid Operations !");
                var user = await _userManager.FindByIdAsync (id);
                if (user is null) return BadRequest("Invalid Operations !");
                user.UserName = model.UserName;
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.Email = model.Email;
                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(model);
        }
        [HttpGet]

        public async Task<IActionResult> Delete(string? id)
        {
            return await Detailes(id, "Delete");
        }
        [HttpPost]
        public async Task<IActionResult> Delete([FromRoute] string id, UserToReturnDto model)
        {
            if (ModelState.IsValid)
            {
                if (id != model.Id) return BadRequest("Invalid Operations !");
                var user = await _userManager.FindByIdAsync(id);
                if (user is null) return BadRequest("Invalid Operations !");
          
                var result = await _userManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(Index));
                }
            }

            return View(model);

        }
    }
}
