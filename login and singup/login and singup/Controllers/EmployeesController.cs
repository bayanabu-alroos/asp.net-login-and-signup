using login_and_singup.Data;
using login_and_singup.Models;
using login_and_singup.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace login_and_singup.Controllers
{
    public class EmployeesController : Controller
    {
        public AppDbContext db;
        public EmployeesController(AppDbContext _db)
        {
            db = _db;
        }
        public async Task<IActionResult> Index(string searchTerm)
        {
            var employeesQuery = db.Employees.Include(x => x.Department).AsQueryable();
            if (!String.IsNullOrEmpty(searchTerm))
            {
                employeesQuery = employeesQuery.Where(c => c.Department.DepartmentName.Contains(searchTerm)
                                                || c.EmployeeName.Contains(searchTerm)
                                                || c.City.Contains(searchTerm));

            }
            var employeesData = await employeesQuery.ToListAsync();
            return View(employeesData);
        }
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.DepartmentsName = new SelectList(db.Departments, "DepartmentId", "DepartmentName");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(EmployeeViewModel employee)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var employeeData = new Employee
                    {
                        EmployeeName = employee.EmployeeName,
                        DepartmentId = employee.DepartmentId,
                        City = employee.City,
                        Phone = employee.Phone,
                        Email = employee.Email,
                        HiringData = employee.HiringData,
                        Salary = employee.Salary,
                        Gender = employee.Gender,
                        Postion = employee.Postion,
                    };
                    db.Employees.Add(employeeData);
                    await db.SaveChangesAsync();

                    TempData["SuccessMessage"] = "Employee created successfully!";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "An error occurred while trying to create the employee.");
            }
            ViewBag.DepartmentsName = new SelectList(db.Departments, "DepartmentId", "DepartmentName");
            return View(employee);
        }
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }

            var employee = db.Employees.Find(id);

            if (employee != null)
            {
                var employeeViewModel = new EmployeeViewModel
                {
                    EmployeeId = employee.EmployeeId,
                    Email = employee.Email,
                    City = employee.City,
                    Salary = employee.Salary,
                    Gender = employee.Gender,
                    Postion = employee.Postion,
                    DepartmentId = employee.DepartmentId,
                    HiringData = employee.HiringData,
                    EmployeeName = employee.EmployeeName,
                    Phone= employee.Phone,
                };

                ViewBag.DepartmentsName = new SelectList(db.Departments, "DepartmentId", "DepartmentName");
                return View(employeeViewModel);
            }
            return RedirectToAction("EmployeeNotFound");
        }
        [HttpPost]
        public async Task<IActionResult> Edit(EmployeeViewModel employeeViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // Get the actual Employee entity from the database
                    var employee = db.Employees.Find(employeeViewModel.EmployeeId);

                    if (employee != null)
                    {
                        employee.EmployeeName = employeeViewModel.EmployeeName;
                        employee.Email = employeeViewModel.Email;
                        employee.City  = employeeViewModel.City;
                        employee.Salary = employeeViewModel.Salary; 
                        employee.Gender = employeeViewModel.Gender;
                        employee.Postion = employeeViewModel.Postion;
                        employee.DepartmentId = employeeViewModel.DepartmentId;
                        employee.HiringData = employeeViewModel.HiringData;
                        employee.EmployeeId = employeeViewModel.EmployeeId;
                        employee.Phone = employeeViewModel.Phone;

                        db.Employees.Update(employee);
                        await db.SaveChangesAsync();

                        TempData["SuccessMessage"] = "Employee updated successfully!";
                        return RedirectToAction("Index");
                    }
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "An error occurred while trying to update the employee.");
            }

            // If you reached here, it means there was an error, so repopulate the dropdown and return the view.
            ViewBag.DepartmentsName = new SelectList(db.Departments, "DepartmentId", "DepartmentName");
            return View(employeeViewModel);
        }
        [HttpGet]
        public IActionResult Details(int? id)
        {

            if (id == null)
            {
                return RedirectToAction("Index");
            }
            var employee = db.Employees.Include(x => x.Department).SingleOrDefault(e => e.EmployeeId == id);

            if (employee != null)
            {
                return View(employee);
            }

            return RedirectToAction("EmployeeNotFound");
        }
        [HttpGet]
        public IActionResult EmployeeNotFound()
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
            var employee = db.Employees.Find(id);
            if (employee != null)
            {
                return View(employee);
            }
            return RedirectToAction("EmployeeNotFound");
        }
        [HttpPost]
        public IActionResult ConfirmDelete(Employee employee, int EmployeeId)
        {
            db.Employees.Remove(employee);
            db.SaveChanges();
            TempData["SuccessMessage"] = "Employee Delete successfully!";

            return RedirectToAction("Index");
        }

    }
}
