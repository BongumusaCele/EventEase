using EventEase.Data;
using EventEase.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EventEase.Controllers
{
    public class UsersController : Controller
    {
        private readonly EventEaseContext _contextEventEase;

        public UsersController(EventEaseContext contextEventEase)
        {
            _contextEventEase = contextEventEase;
        }

        public async Task<IActionResult> Index() => View(await _contextEventEase.Users.ToListAsync());

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) { return NotFound(); }

            var item = await _contextEventEase.Users.FirstOrDefaultAsync(u => u.UserId == id);

            if (item == null) { return NotFound(); }

            return View(item);
        }

        public IActionResult Create() => View();

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(User user)
        {
            if (!ModelState.IsValid) { return View(user); }

            _contextEventEase.Add(user);
            await _contextEventEase.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) { return NotFound(); }

            var item = await _contextEventEase.Users.FindAsync(id);

            if (item == null) { return NotFound(); }

            return View(item);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, User user)
        {
            if (id != user.UserId) { return NotFound(); }

            if (!ModelState.IsValid) { return View(user); }

            _contextEventEase.Update(user);
            await _contextEventEase.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) { return NotFound(); }

            var item = await _contextEventEase.Users.FirstOrDefaultAsync(u => u.UserId == id);

            if (item == null) { return NotFound(); }

            return View(item);
        }

        [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var item = await _contextEventEase.Users.FindAsync(id);
            if (item == null) { return NotFound(); }

            _contextEventEase.Users.Remove(item);
            await _contextEventEase.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
