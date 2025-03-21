using Company.Seif.BLL.Interfaces;
using Company.Seif.BLL.Repositories;
using Company.Seif.DAL.Models;
using Company.Seif.PL.DTOS;
using Microsoft.AspNetCore.Mvc;

namespace Company.Seif.PL.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly  IEmployeeRepository _employeeRepository;
        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var employees = _employeeRepository.GetAll();
            return View(employees);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(CreateEmployeeDto model)
        {
            if (ModelState.IsValid) //server side validation
            {
                var employee = new Employee()
                {
                   Name = model.Name,
                   Adress = model.Adress,
                   Age = model.Age,
                   CreateAt=model.CreateAt,
                   HiringDate = model.HiringDate,
                   Email = model.Email,
                   IsActive = model.IsActive,
                   IsDeleted = model.IsDeleted,
                   Phone = model.Phone,
                   Salary = model.Salary,
                };
                var count = _employeeRepository.Add(employee);
                if (count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(model);
        }

        [HttpGet]

        public IActionResult Detailes(int? id, string viewName = "Detailes")
        {
            if (id is null) return BadRequest("Invalid Id"); //400
            var employee = _employeeRepository.Get(id.Value);
            if (employee is null) return NotFound(new { statueCode = 404, Message = $"Employee With Id : {id} is not found" });
            return View(viewName, employee);
        }
        [HttpGet]

        public IActionResult Edit(int? id)
        {
            if (id is null) return BadRequest( "Invalid Id");
            var employee = _employeeRepository.Get(id.Value);
            if (employee is null) return NotFound(new { statueCode = 404, Message = $"Employee With Id : {id} is not found" });
            var employeeDto = new CreateEmployeeDto()
            {
                Name = employee.Name,
                Adress = employee.Adress,
                Age = employee.Age,
                CreateAt = employee.CreateAt,
                HiringDate = employee.HiringDate,
                Email = employee.Email,
                IsActive = employee.IsActive,
                IsDeleted = employee.IsDeleted,
                Phone = employee.Phone,
                Salary = employee.Salary,
            };
            return View(employeeDto);
        }
        [HttpPost]
        public IActionResult Edit([FromRoute] int id, CreateEmployeeDto model)
        {
            if (ModelState.IsValid)
            {
                //if (id != model.Id) return BadRequest(); //400
                var employee = new Employee()
                {
                    Id = id,
                    Name = model.Name,
                    Adress = model.Adress,
                    Age = model.Age,
                    CreateAt = model.CreateAt,
                    HiringDate = model.HiringDate,
                    Email = model.Email,
                    IsActive = model.IsActive,
                    IsDeleted = model.IsDeleted,
                    Phone = model.Phone,
                    Salary = model.Salary,
                };
                var count = _employeeRepository.Update(employee);
                if (count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(model);

        }
        [HttpGet]

        public IActionResult Delete(int? id)
        {
            return Detailes(id, "Delete");
        }
        [HttpPost]
        public IActionResult Delete([FromRoute] int id, Employee model)
        {
            if (ModelState.IsValid)
            {
                if (id != model.Id) return BadRequest(); //400
                var count = _employeeRepository.Delete(model);
                if (count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(model);

        }

    }
}




















 
