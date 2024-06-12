using BackendDeveloper_Suryadeep.Models;
using BackendDeveloper_Suryadeep.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BackendDeveloper_Suryadeep.Controllers
{

    //public class DepartmentsController : Controller
    //{
    //    private readonly IDepartmentRepository _repository;

    //    public DepartmentsController(IDepartmentRepository repository)
    //    {
    //        _repository = repository;
    //    }

    //    public async Task<IActionResult> Index()
    //    {
    //        var departments = await _repository.GetDepartmentsAsync();
    //        return View(departments);
    //    }

    //    public IActionResult Create()
    //    {
    //        return View();
    //    }

    //    [HttpPost]
    //    public async Task<IActionResult> Create(Department department)
    //    {
    //        if (ModelState.IsValid)
    //        {
    //            await _repository.AddDepartmentAsync(department);
    //            return RedirectToAction(nameof(Index));
    //        }
    //        return View(department);
    //    }

    //    public async Task<IActionResult> Edit(int id)
    //    {
    //        var department = await _repository.GetDepartmentByIdAsync(id);
    //        if (department == null)
    //        {
    //            return NotFound();
    //        }
    //        return View(department);
    //    }

    //    [HttpPost]
    //    public async Task<IActionResult> Edit(Department department)
    //    {
    //        if (ModelState.IsValid)
    //        {
    //            await _repository.UpdateDepartmentAsync(department);
    //            return RedirectToAction(nameof(Index));
    //        }
    //        return View(department);
    //    }

    //    public async Task<IActionResult> Delete(int id)
    //    {
    //        var department = await _repository.GetDepartmentByIdAsync(id);
    //        if (department == null)
    //        {
    //            return NotFound();
    //        }
    //        return View(department);
    //    }

    //    [HttpPost, ActionName("Delete")]
    //    public async Task<IActionResult> DeleteConfirmed(int id)
    //    {
    //        await _repository.DeleteDepartmentAsync(id);
    //        return RedirectToAction(nameof(Index));
    //    }

    //    public async Task<IActionResult> Details(int id)
    //    {
    //        var department = await _repository.GetDepartmentByIdAsync(id);
    //        if (department == null)
    //        {
    //            return NotFound();
    //        }
    //        return View(department);
    //    }
    //}

    public class DepartmentsController : Controller
    {
        private readonly IDepartmentRepository _repository;

        public DepartmentsController(IDepartmentRepository repository)
        {
            _repository = repository;
        }

        public async Task<IActionResult> Index()
        {
            var departments = await _repository.GetDepartmentsAsync();
            return View(departments);
        }

        public async Task<IActionResult> Details(int id)
        {
            var department = await _repository.GetDepartmentByIdAsync(id);
            if (department == null)
            {
                return NotFound();
            }
            return View(department);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Departments = await _repository.GetDepartmentsAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Department department)
        {
            if (ModelState.IsValid)
            {
                await _repository.AddDepartmentAsync(department);
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Departments = await _repository.GetDepartmentsAsync();
            return View(department);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var department = await _repository.GetDepartmentByIdAsync(id);
            if (department == null)
            {
                return NotFound();
            }
            ViewBag.Departments = await _repository.GetDepartmentsAsync();
            return View(department);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Department department)
        {
            if (id != department.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _repository.UpdateDepartmentAsync(department);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DepartmentExists(department.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Departments = await _repository.GetDepartmentsAsync();
            return View(department);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var department = await _repository.GetDepartmentByIdAsync(id);
            if (department == null)
            {
                return NotFound();
            }

            return View(department);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _repository.DeleteDepartmentAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private bool DepartmentExists(int id)
        {
            return _repository.GetDepartmentByIdAsync(id) != null;
        }
    }

}
