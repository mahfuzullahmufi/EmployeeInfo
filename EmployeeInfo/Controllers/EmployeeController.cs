﻿using EmployeeInfo.Data;
using EmployeeInfo.Models;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeInfo.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly ApplicationDbContext _db;

        public EmployeeController(ApplicationDbContext db)
        {
            _db = db;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult LoadDataTable(int page = 1, int pageSize = 10)
        {
            var model = GetDataTableViewModel(page, pageSize);
            return Json(model);
        }

        private PagedViewModel<Employee> GetDataTableViewModel(int page, int pageSize)
        {
            var model = new PagedViewModel<Employee>();
            var query = _db.Employees.OrderBy(e => e.EmployeeName).ThenBy(x => x.Salary);

            model.CurrentPage = page;
            model.PageSize = pageSize;
            model.TotalCount = query.Count();
            model.TotalPages = (int)Math.Ceiling((double)model.TotalCount / pageSize);
            model.PagedData = query.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            return model;
        }


        public IActionResult Create()
        {

            return View();
        }

        [HttpPost]
        public IActionResult Create(Employee obj)
        {
            if (ModelState.IsValid)
            {
                _db.Employees.Add(obj);
                _db.SaveChanges();
                TempData["success"] = "Employee added successfully!";
                return RedirectToAction("Index");
            }
            return View(obj);
        }


        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var employeeFromDb = _db.Employees.Find(id);

            if (employeeFromDb == null)
            {
                return NotFound();
            }

            return View(employeeFromDb);
        }

        [HttpPost]
        public IActionResult Edit(Employee obj)
        {
            if (ModelState.IsValid)
            {
                _db.Employees.Update(obj);
                _db.SaveChanges();
                TempData["success"] = "Employee Data edited successfully!";
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        [HttpPost]
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var employeeFromDb = _db.Employees.Find(id);

            if (employeeFromDb == null)
            {
                return NotFound();
            }

            _db.Employees.Remove(employeeFromDb);
            _db.SaveChanges();
            return Ok(true);
        }
    }
}
