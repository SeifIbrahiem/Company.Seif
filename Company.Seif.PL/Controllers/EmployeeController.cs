using AutoMapper;
using Company.Seif.BLL.Interfaces;
using Company.Seif.BLL.Repositories;
using Company.Seif.DAL.Models;
using Company.Seif.PL.DTOS;
using Company.Seif.PL.Helbers.Company.Seif.PL.Helbers;
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
        public async Task <IActionResult> Index(string?SearchInput)
        {
            IEnumerable<Employee> employees;
            if (string.IsNullOrEmpty(SearchInput))
            {
                employees = await _unitOfWork.EmployeeRepository.GetAllAsync();
            }
            else
            {
              employees= await  _unitOfWork.EmployeeRepository.GetByNameAsync(SearchInput);
            }
            // ViewData["Message"] = "Hello From ViewData";
            return View(employees);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
           var departments = await _unitOfWork.DepartmentRepository.GetAllAsync();
           ViewData["departments"] = departments;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateEmployeeDto model)
        {
            if (ModelState.IsValid) //server side validation
            {

                if (model.Image is not null)
                {
                    model.ImageName = DocumentSetting.UploadFile(model.Image, "images");
                }

                var employee = _mapper.Map<Employee>(model);
               await _unitOfWork.EmployeeRepository.AddAsync(employee);
                var count = await _unitOfWork.CompleteAsync();
                if (count > 0)
                {
                    TempData["Message"] = "Employee is created !!";
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(model);
        }

        [HttpGet]

        public async Task<IActionResult> Detailes(int? id)
        {
            if (id is null) return BadRequest("Invalid Id"); //400
            var employee = await _unitOfWork.EmployeeRepository.GetAsync(id.Value);
            if (employee is null) return NotFound(new { statueCode = 404, Message = $"Employee With Id : {id} is not found" });
            var dto=_mapper.Map<CreateEmployeeDto>(employee);
            return View(dto);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int? id , string viewName="Edit")
        {
            //var departments = _departmentRepository.GetAll();
            //ViewData["departments"] = departments;
            if (id is null) return BadRequest("Invalid Id");
            var employee = await _unitOfWork.EmployeeRepository.GetAsync(id.Value);
            var departments =await _unitOfWork.DepartmentRepository.GetAllAsync();
            if (employee is null) return NotFound(new { statueCode = 404, Message = $"Employee With Id : {id} is not found" });
            var dto = _mapper.Map<CreateEmployeeDto>(employee);
            return View(viewName, dto);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] int id, CreateEmployeeDto model, string viewName = "Edit")
        {
            if (ModelState.IsValid)
            {

                if (model.ImageName is not null && model.Image is not null)
                {
                    DocumentSetting.DeleteFile(model.ImageName, "images");
                }

                if (model.Image is not null)
                {

                    model.ImageName = DocumentSetting.UploadFile(model.Image, "images");
                }


                var employee = _mapper.Map<Employee>(model);
                employee.Id = id;
                _unitOfWork.EmployeeRepository.Update(employee);
                var count = await _unitOfWork.CompleteAsync();
                if (count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(viewName,model);

        }
        [HttpGet]

        public async Task<IActionResult> Delete(int? id)
        {
            return await Edit (id, "Delete");
        }
        [HttpPost]
        public async Task<IActionResult> Delete([FromRoute] int id, CreateEmployeeDto model)
        {
            if (ModelState.IsValid)
            {
                var employee = _mapper.Map<Employee>(model);
                employee.Id = id;
                _unitOfWork.EmployeeRepository.Delete(employee);
                var count = await _unitOfWork.CompleteAsync();
                if (count > 0)
                {
                    if (model.ImageName is not null)
                    {
                        DocumentSetting.DeleteFile(model.ImageName, "images");
                    }

                    return RedirectToAction(nameof(Index));
                }
            }
            return View(model);

        }

    }
}  




















 
