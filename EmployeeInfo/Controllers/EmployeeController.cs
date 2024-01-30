using AutoMapper;
using EmployeeInfo.Service.IService;
using EmployeeInfo.Web.Factories.Interfaces;
using EmployeeInfo.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeInfo.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;
        private readonly IEmployeeFactory _employeeFactory;
        private readonly IProjectService _projectService;
        private readonly IMapper _mapper;

        public EmployeeController(IEmployeeService employeeService, IEmployeeFactory employeeFactory, IProjectService projectService, IMapper mapper)
        {
            _employeeService = employeeService;
            _employeeFactory = employeeFactory;
            _projectService = projectService;
            _mapper = mapper;
        }

        public async Task<ActionResult> Index()
        {
            return View();
        }

        public async Task<IActionResult> LoadDataTable(int page = 1, int pageSize = 10)
        {
            var model = await _employeeFactory.GetAllAsync(page, pageSize);
            return Json(model);
        }

        public async Task<IActionResult> Create()
        {
            var projects = await _projectService.GetAllAsync();
            ViewBag.Projects = _mapper.Map<List<ProjectModel>>(projects);
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(EmployeeModel model)
        {
            if (ModelState.IsValid)
            {
                await _employeeFactory.AddEmployeeAsync(model);
                TempData["success"] = "Employee added successfully!";
                return RedirectToAction("Index");
            }

            var projects = await _projectService.GetAllAsync();
            ViewBag.Projects = _mapper.Map<List<ProjectModel>>(projects);
            return View(model);
        }


        public async Task<IActionResult> Edit(int id)
        {
            if (id == 0)
                return NotFound();

            var employee = await _employeeFactory.GetByIdAsync(id);
            if (employee == null)
                return NotFound();

            var projects = await _projectService.GetAllAsync();
            ViewBag.Projects = _mapper.Map<List<ProjectModel>>(projects);
            return View(employee);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EmployeeModel obj)
        {
            if (ModelState.IsValid)
            {
                await _employeeFactory.EditAsync(obj);
                TempData["success"] = "Employee Data edited successfully!";
                return RedirectToAction("Index");
            }

            var projects = await _projectService.GetAllAsync();
            ViewBag.Projects = _mapper.Map<List<ProjectModel>>(projects);
            return View(obj);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0)
                return NotFound();

            var result = await _employeeService.DeleteAsync(id);
            return Ok(result);
        }
    }
}
