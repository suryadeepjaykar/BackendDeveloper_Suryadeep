using BackendDeveloper_Suryadeep.Models;
using BackendDeveloper_Suryadeep.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BackendDeveloper_Suryadeep.Controllers
{
    public class RemindersController : Controller
    {
        private readonly IReminderRepository _repository;

        public RemindersController(IReminderRepository repository)
        {
            _repository = repository;
        }

        public async Task<IActionResult> Index()
        {
            var reminders = await _repository.GetRemindersAsync();
            return View(reminders);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Reminder reminder)
        {
            if (ModelState.IsValid)
            {
                await _repository.AddReminderAsync(reminder);
                return RedirectToAction(nameof(Index));
            }
            return View(reminder);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var reminder = await _repository.GetReminderByIdAsync(id);
            if (reminder == null)
            {
                return NotFound();
            }
            return View(reminder);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Reminder reminder)
        {
            if (ModelState.IsValid)
            {
                await _repository.UpdateReminderAsync(reminder);
                return RedirectToAction(nameof(Index));
            }
            return View(reminder);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var reminder = await _repository.GetReminderByIdAsync(id);
            if (reminder == null)
            {
                return NotFound();
            }
            return View(reminder);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _repository.DeleteReminderAsync(id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int id)
        {
            var reminder = await _repository.GetReminderByIdAsync(id);
            if (reminder == null)
            {
                return NotFound();
            }
            return View(reminder);
        }
    }

}
