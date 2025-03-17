using Company.Seif.BLL.Interfaces;
using Company.Seif.BLL.Repositories;
using Company.Seif.DAL.Models;
using Company.Seif.PL.DTOS;
using Microsoft.AspNetCore.Mvc;

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

    }
}
