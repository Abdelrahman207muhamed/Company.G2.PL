using Company.G2.BLL.Interfaces;
using Company.G2.DAL.Models;
using Company.G2.PL.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Company.G2.PL.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;

        //Ask CLR Create Object From IEmployeeRepository
        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        [HttpGet] //Get : /Department/Index
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
        public IActionResult Create(CreateEmployeeDtos model)
        {

            if (ModelState.IsValid) //Server Side Validation 
            {
                var employee = new Employee()
                {
                    Name=model.Name,
                    Address = model.Address,
                    Age=model.Age,
                    CreateAt=model.CreateAt,
                    HiringDate =model.HiringDate,
                    Email=model.Email,
                    IsActive=model.IsActive,
                    IsDeleted=model.IsDeleted,
                    Phone=model.Phone,
                    Salary=model.Salary,
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
        public IActionResult Details(int? id, string viewName = "Details")
        {
            if (id is null) return BadRequest("Invalid ID"); //400

            var employee = _employeeRepository.Get(id.Value);
            if (employee is null) return NotFound(new { StatusCode = 404, message = $"Employee with Id :{id} is not Found" });

            return View(viewName, employee);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id is null) return BadRequest("Invalid ID"); //400
            
            var employee = _employeeRepository.Get(id.Value);

            if (employee is null) return NotFound(new { StatusCode = 404, message = $"Employee with Id :{id} is not Found" });
            var employeeDto = new CreateEmployeeDtos()
            {
                Name = employee.Name,
                Address = employee.Address,
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
        [ValidateAntiForgeryToken]

        public IActionResult Edit([FromRoute] int id, CreateEmployeeDtos model)
        {
            ArgumentNullException.ThrowIfNull(model);
            if (ModelState.IsValid)
            {
                //if (id != model.Id) return BadRequest(); //400
                var employee = new Employee()
                {
                    Id=id,
                    Name = model.Name,
                    Address = model.Address,
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

        //[HttpPost]
        //[ValidateAntiForgeryToken] //Any Action Work For Post Perefer That
        //public IActionResult Edit([FromRoute] int id, UpdateDepartmentDto model)
        //{

        //    if (ModelState.IsValid)
        //    {
        //        var department = new Department()
        //        {
        //            Id =id,
        //            Name=model.Name,
        //            Code=model.Code,
        //            CreateAt=model.CreateAt
        //        };
        //        var count = employeeRepository.Update(department);
        //        if (count > 0)
        //        {
        //            return RedirectToAction(nameof(Index));
        //        }
        //    }
        //    return View(model);
        //}

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            //    if (id is null) return BadRequest("Invalid ID"); //400
            //    var department = employeeRepository.Get(id.Value);
            //    if (department is null) return NotFound(new { StatusCode = 404, message = $"Department with Id :{id} is not Found" });

            return Details(id, "Delete");
        }



        [HttpPost]
        [ValidateAntiForgeryToken]

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
