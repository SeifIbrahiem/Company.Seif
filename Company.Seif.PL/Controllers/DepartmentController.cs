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
       // private readonly IDepartmentRepository _departmentrepository;
        private readonly IUnitOfWork _unitOfWork;

        //ask clr create object from DepartmentRepository
        public DepartmentController(/*IDepartmentRepository departmentrepository*/ IUnitOfWork unitOfWork)
        {
           // _departmentrepository = departmentrepository;
            _unitOfWork = unitOfWork;
        }
        [HttpGet] //GET: /Department/Index
        public async Task<IActionResult> Index()
        {

            var departments = await _unitOfWork.DepartmentRepository.GetAllAsync();
            ViewBag.Message= "Hello From ViewBag";
            return View(departments);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateDepartmentDto model)
        {
            if (ModelState.IsValid) //server side validation
            {
                var department = new Department()
                {
                    Code = model.Code,
                    Name = model.Name,
                    CreateAt = model.CreateAt,

                };
               await _unitOfWork.DepartmentRepository.AddAsync(department);
                var count = await _unitOfWork.CompleteAsync();
                if (count > 0)
                {
                    TempData["Message"] = "Department is created !!";
                    return RedirectToAction(nameof(Index));
                }

            }

            return View(model);
        }

        [HttpGet]

        public async Task<IActionResult> Detailes(int? id )
        {
            if (id is null) return BadRequest("Invalid Id"); //400
            var department = await _unitOfWork.DepartmentRepository.GetAsync(id.Value);
            if (department is null) return NotFound(new { statueCode = 404, Message = $"Department With Id : {id} is not found" });
            return View(department);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int?id)
        {
            if (id is null) return BadRequest("Invalid Id"); //400
            var department = await _unitOfWork.DepartmentRepository.GetAsync(id.Value);
            if(department is null) return NotFound(new { statueCode = 404, Message = $"Department With Id : {id} is not found" });

            var dto = new CreateDepartmentDto()
            {
                Name = department.Name,
                Code = department.Code,
                CreateAt = department.CreateAt,
            };
            return View(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Edit([FromRoute]int id , CreateDepartmentDto model)
        {
          if (ModelState.IsValid) //server side validation
            {
                var department = new Department()
                {
                    Id = id,
                    Name = model.Name,
                    Code = model.Code,
                    CreateAt = model.CreateAt
                };
                _unitOfWork.DepartmentRepository.Update(department);
                var count = await _unitOfWork.CompleteAsync();
                if (count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
          return View(model);
        }
        [HttpGet]

        public async Task<IActionResult> Delete (int? id)
        {
            if (id is null) return BadRequest("Invalid Id"); //400
            var department = await _unitOfWork.DepartmentRepository.GetAsync(id.Value);
            if (department is null) return NotFound(new { statueCode = 404, Message = $"Department With Id : {id} is not found" });

            var dto = new CreateDepartmentDto()
            {
                Name = department.Name,
                Code = department.Code,
                CreateAt = department.CreateAt,
            };
            return View(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Delete ([FromRoute] int id, CreateDepartmentDto model)
        {
            if (ModelState.IsValid) //server side validation
            {
                var department = new Department()
                {
                    Id = id,
                    Name = model.Name,
                    Code = model.Code,
                    CreateAt = model.CreateAt
                };
                _unitOfWork.DepartmentRepository.Delete(department);
                var count =await _unitOfWork.CompleteAsync();
                if (count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(model);
        }

    }
}
