using login_and_singup.Data;
using login_and_singup.Models;
using login_and_singup.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace login_and_singup.Controllers
{
    public class DepartmentsController : Controller
    {
        private readonly AppDbContext db;
        public DepartmentsController(AppDbContext _db)
        {
            db = _db;
        }
        public async Task <IActionResult> Index()
        {
            var departments = await db.Departments.Include(c=>c.Employees).ToListAsync();
            return View(departments);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(DepartmentViewModel department)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var departmentData = new Department
                    {
                        DepartmentName =department.DepartmentName,
                    };
                    db.Departments.Add(departmentData);
                    await db.SaveChangesAsync();

                    TempData["SuccessMessage"] = "Department created successfully!";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "An error occurred while trying to create the department.");
            }
            return View(department);
        }
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            var department = db.Departments.Find(id);
            if (department != null)
            {
                return View(department);
            }
            return RedirectToAction("DepartmentNotFound");
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Department department)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Departments.Update(department);
                    await db.SaveChangesAsync();

                    TempData["SuccessMessage"] = "Department Update successfully!";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "An error occurred while trying to create the department.");
            }
            return View(department);
        }

        [HttpGet]
        public IActionResult DepartmentNotFound()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            var department = db.Departments.Find(id);
            if (department != null)
            {
                return View(department);
            }
            return RedirectToAction("DepartmentNotFound");
        }
        [HttpPost]
        public IActionResult ConfirmDelete(Department department, int DepartmentId)
        {
            db.Departments.Remove(department);
            db.SaveChanges();
            TempData["SuccessMessage"] = "  Department Delete successfully!";

            return RedirectToAction("Index");
        }
    }
}
