using Company.Seif.BLL.Interfaces;
using Company.Seif.BLL.Repositories;
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
    }
}
