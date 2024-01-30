using AutoMapper;
using EmployeeInfo.Entities.Domain;
using EmployeeInfo.Service.IService;
using EmployeeInfo.Web.Factories;
using EmployeeInfo.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeInfo.Web.Controllers
{
    public class ProjectController : Controller
    {
        private readonly IProjectService _projectService;
        private readonly IProjectFactory _projectFactory;
        private readonly IEmployeeService _employeeService;
        private readonly IMapper _mapper;

        public ProjectController(IProjectService projectService, IProjectFactory projectFactory, IEmployeeService employeeService, IMapper mapper)
        {
            _projectService = projectService;
            _projectFactory = projectFactory;
            _employeeService = employeeService;
            _mapper = mapper;
        }

        public async Task<ActionResult> Index()
        {
            return View();
        }

        public async Task<IActionResult> LoadDataTable(int page = 1, int pageSize = 10)
        {
            var model = await _projectFactory.GetProjectsWithEmployees(page, pageSize);
            return Json(model);
        }

        public async Task<IActionResult> Create()
        {
            var employees = await _employeeService.GetAllAsync();
            ViewBag.Employees = _mapper.Map<List<EmployeeModel>>(employees);
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProjectModel model)
        {
            if (ModelState.IsValid)
            {
                await _projectFactory.AddProjectAsync(model);
                TempData["success"] = "Project added successfully!";
                return RedirectToAction("Index");
            }

            return View(model);
        }


        public async Task<IActionResult> Edit(int id)
        {
            if (id == 0)
                return NotFound();

            var project = await _projectFactory.GetProjectByIdWithEmployees(id);
            if (project == null)
                return NotFound();

            var employees = await _employeeService.GetAllAsync();
            ViewBag.Employees = _mapper.Map<List<EmployeeModel>>(employees);
            return View(project);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ProjectModel model)
        {
            if (ModelState.IsValid)
            {
                await _projectFactory.EditProjectAsync(model);
                TempData["success"] = "Project Data edited successfully!";
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0)
                return NotFound();

            var result = await _projectService.DeleteAsync(id);
            return Ok(result);
        }
    }
}
