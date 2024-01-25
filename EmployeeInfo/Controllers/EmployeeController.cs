﻿using EmployeeInfo.Entities.Models;
using EmployeeInfo.Service.IService;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeInfo.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public  async Task<IActionResult> LoadDataTable(int page = 1, int pageSize = 10)
        {
            var model = await _employeeService.GetAllAsync(page, pageSize);
            return Json(model);
        }


        public IActionResult Create()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Employee obj)
        {
            if (ModelState.IsValid)
            {
                await _employeeService.AddAsync(obj);
                TempData["success"] = "Employee added successfully!";
                return RedirectToAction("Index");
            }
            return View(obj);
        }


        public async Task<IActionResult> Edit(int id)
        {
            if (id == 0)
                return NotFound();

            var employee = await _employeeService.GetByIdAsync(id);

            if (employee == null)
                return NotFound();

            return View(employee);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Employee obj)
        {
            if (ModelState.IsValid)
            {
                await _employeeService.EditAsync(obj);
                TempData["success"] = "Employee Data edited successfully!";
                return RedirectToAction("Index");
            }
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
