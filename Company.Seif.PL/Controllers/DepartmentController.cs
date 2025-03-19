using Company.Seif.BLL.Interfaces;
using Company.Seif.BLL.Repositories;
using Company.Seif.DAL.Models;
using Company.Seif.PL.DTOS;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;

namespace Company.Seif.PL.Controllers
{
    //MVC Controller
    public class DepartmentController : Controller
    {
        private readonly IDepartmentRepository _departmentrepository;

        //ask clr create object from DepartmentRepository
        public DepartmentController(IDepartmentRepository departmentrepository)
        {
            _departmentrepository = departmentrepository;
        }
        [HttpGet] //GET: /Department/Index
        public IActionResult Index()
        {

            var departments = _departmentrepository.GetAll();

            return View(departments);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(CreateDepartmentDto model)
        {
            if (ModelState.IsValid) //server side validation
            {
                var department = new Department()
                {
                    Code = model.Code,
                    Name = model.Name,
                    CreateAt = model.CreateAt,

                };
                var count = _departmentrepository.Add(department);
                if (count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }

            }

            return View(model);
        }

        [HttpGet]

        public IActionResult Detailes(int? id , string viewName = "Detailes")
        {
            if (id is null) return BadRequest("Invalid Id"); //400
            var department = _departmentrepository.Get(id.Value);
            if (department is null) return NotFound(new { statueCode = 404, Message = $"Department With Id : {id} is not found" });
            return View(viewName , department);
        }


        [HttpGet]

        public IActionResult Edit(int? id)
        {
            //if (id is null) return BadRequest("Invalid Id"); //400
            //var department = _departmentrepository.Get(id.Value);
            //if (department is null) return NotFound(new { statueCode = 404, Message = $"Department With Id : {id} is not found" });
            return Detailes(id , "Edit");
        }

        [HttpPost]
        public IActionResult Edit([FromRoute] int id , Department department)
        { 
          if (ModelState.IsValid)
            {
                if (id != department.Id) return BadRequest(); //400
                var count = _departmentrepository.Update(department);
                if (count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
          return View(department);
        
        }

        [HttpGet]

        public IActionResult Delete(int? id)
        {
            //if (id is null) return BadRequest("Invalid Id"); //400
            //var department = _departmentrepository.Get(id.Value);
            //if (department is null) return NotFound(new { statueCode = 404, Message = $"Department With Id : {id} is not found" });
            return Detailes(id , "Delete");
        }


        [HttpPost]
        public IActionResult Delete([FromRoute] int id, Department department)
        {
            if (ModelState.IsValid)
            {
                if (id != department.Id) return BadRequest(); //400
                var count = _departmentrepository.Delete(department);
                if (count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(department);

        }
















    }
}
