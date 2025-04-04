using AutoMapper;
using Company.Seif.BLL.Interfaces;
using Company.Seif.BLL.Repositories;
using Company.Seif.DAL.Models;
using Company.Seif.PL.DTOS;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace Company.Seif.PL.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        // private readonly IEmployeeRepository _employeeRepository;
        //private readonly IDepartmentRepository _departmentRepository;
        private readonly IMapper _mapper;

        public EmployeeController(
           // IEmployeeRepository employeeRepository , 
          //  IDepartmentRepository departmentRepository,
           IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            // _employeeRepository = employeeRepository;
            //  _departmentRepository = departmentRepository;
            _mapper = mapper;
        }
        [HttpGet]
        public IActionResult Index(string?SearchInput)
        {
            IEnumerable<Employee> employees;
            if (string.IsNullOrEmpty(SearchInput))
            { 
             employees = _unitOfWork.EmployeeRepository.GetAll();
            }
            else
            {
             employees= _unitOfWork.EmployeeRepository.GetByName(SearchInput);
            }
            // ViewData["Message"] = "Hello From ViewData";
            return View(employees);
        }
        [HttpGet]
        public IActionResult Create()
        {
           var departments = _unitOfWork.DepartmentRepository.GetAll();
           ViewData["departments"] = departments;
            return View();
        }
        [HttpPost]
        public IActionResult Create(CreateEmployeeDto model)
        {
            if (ModelState.IsValid) //server side validation
            {
                //var employee = new Employee()
                //{
                //    Name = model.Name,
                //    Adress = model.Adress,
                //    Age = model.Age,
                //    CreateAt = model.CreateAt,
                //    HiringDate = model.HiringDate,
                //    Email = model.Email,
                //    IsActive = model.IsActive,
                //    IsDeleted = model.IsDeleted,
                //    Phone = model.Phone,
                //    Salary = model.Salary,
                //    DepartmentId = model.DepartmentId,
                //};
               var employee = _mapper.Map<Employee>(model);
                var count = _unitOfWork.EmployeeRepository.Add(employee);
                if (count > 0)
                {
                    TempData["Message"] = "Employee is created !!";
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(model);
        }

        [HttpGet]

        public IActionResult Detailes(int? id)
        {
            if (id is null) return BadRequest("Invalid Id"); //400
            var employee = _unitOfWork.EmployeeRepository.Get(id.Value);
            if (employee is null) return NotFound(new { statueCode = 404, Message = $"Employee With Id : {id} is not found" });
            var dto=_mapper.Map<CreateEmployeeDto>(employee);
            return View(dto);
        }
        [HttpGet]
        public IActionResult Edit(int? id , string viewName="Edit")
        {
            //var departments = _departmentRepository.GetAll();
            //ViewData["departments"] = departments;
            if (id is null) return BadRequest("Invalid Id");
            var employee = _unitOfWork.EmployeeRepository.Get(id.Value);
            var departments = _unitOfWork.DepartmentRepository.GetAll();
            if (employee is null) return NotFound(new { statueCode = 404, Message = $"Employee With Id : {id} is not found" });
            var dto = _mapper.Map<CreateEmployeeDto>(employee);
            return View(viewName, dto);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int id, CreateEmployeeDto model, string viewName = "Edit")
        {
            if (ModelState.IsValid)
            {
                // if (id != model.Id) return BadRequest(); //400
                //var employee = new Employee()
                //{
                //    Id = id,
                //    Name = model.Name,
                //    Adress = model.Adress,
                //    Age = model.Age,
                //    CreateAt = model.CreateAt,
                //    HiringDate = model.HiringDate,
                //    Email = model.Email,
                //    IsActive = model.IsActive,
                //    IsDeleted = model.IsDeleted,
                //    Phone = model.Phone,
                //    Salary = model.Salary,
                //};
                var employee = _mapper.Map<Employee>(model);
                employee.Id = id;
                var count = _unitOfWork.EmployeeRepository.Update(employee);
                if (count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(viewName,model);

        }
        [HttpGet]

        public IActionResult Delete(int? id)
        {
            return Edit (id, "Delete");
        }
        [HttpPost]
        public IActionResult Delete([FromRoute] int id, CreateEmployeeDto model)
        {
            if (ModelState.IsValid)
            {
                var employee = _mapper.Map<Employee>(model);
                employee.Id = id;
                var count = _unitOfWork.EmployeeRepository.Delete(employee);
                if (count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(model);

        }

    }
}  




















 
