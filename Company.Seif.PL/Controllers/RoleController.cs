﻿using Company.Seif.DAL.Models;
using Company.Seif.PL.DTOS;
using Company.Seif.PL.Helbers.Company.Seif.PL.Helbers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Company.Seif.PL.Controllers
{
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        public RoleController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }
        [HttpGet]
        public async Task<IActionResult> Index(string? SearchInput)
        {
            IEnumerable<RoleToReturnDto> roles;
            if (string.IsNullOrEmpty(SearchInput))
            {
                roles = _roleManager.Roles.Select(U => new RoleToReturnDto()
                {
                    Id = U.Id,
                    Name = U.Name,

                });
            }
            else
            {
                roles = _roleManager.Roles.Select(U => new RoleToReturnDto()
                {
                    Id = U.Id,
                    Name = U.Name,

                }).Where(R => R.Name.ToLower().Contains(SearchInput.ToLower()));
            }
            return View(roles);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(RoleToReturnDto model)
        {
            if (ModelState.IsValid) //server side validation
            {
                var role = await _roleManager.FindByNameAsync(model.Name);
                if (role is null)
                {
                    role = new IdentityRole()
                    {
                        Name = model.Name,
                    };
                  var result =await  _roleManager.CreateAsync(role);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }
                }
            }
            return View(model);
        }

        [HttpGet]

        public async Task<IActionResult> Detailes(string? id, string viewName = "Detailes")
        {
            if (id is null) return BadRequest("Invalid Id"); //400
            var role = await _roleManager.FindByIdAsync(id);
            if (role is null) return NotFound(new { statueCode = 404, Message = $"User With Id : {id} is not found" });
            var dto = new RoleToReturnDto()
            {
                Id = role.Id,
                Name = role.Name,

            };
            return View(viewName, dto);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(string? id)
        {
            return await Detailes(id, "Edit");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] string id, RoleToReturnDto model)
        {
            if (ModelState.IsValid)
            {
                if (id != model.Id) return BadRequest("Invalid Operations !");
                var role = await _roleManager.FindByIdAsync(id);
                if (role is null) return BadRequest("Invalid Operations !");
                var roleResult = await _roleManager.FindByNameAsync(model.Name);
                if (roleResult is null)
                {
                    role.Name = model.Name;
                    var result = await _roleManager.UpdateAsync(role);
                    if (result.Succeeded)
                    {
                        return RedirectToAction(nameof(Index));
                    }

                }
                ModelState.AddModelError("", "Imvalid operation !");
            }
            return View(model);
        }
        [HttpGet]

        public async Task<IActionResult> Delete(string? id)
        {
            return await Detailes(id, "Delete");
        }
        [HttpPost]
        public async Task<IActionResult> Delete([FromRoute] string id, RoleToReturnDto model)
        {
            if (ModelState.IsValid)
            {
                if (id != model.Id) return BadRequest("Invalid Operations !");
                var role = await _roleManager.FindByIdAsync(id);
                if (role is null) return BadRequest("Invalid Operations !");
                var result = await _roleManager.DeleteAsync(role);
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError("", "Imvalid operation !");
            }
            return View(model);
          
        }
    }
 }
   

