using EventEase.Data;
using EventEase.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EventEase.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UsersController : Controller
    {
        private readonly EventEaseContext _contextEventEase;

        public UsersController(EventEaseContext contextEventEase)
        {
            _contextEventEase = contextEventEase;
        }

        private static readonly string[] ValidRoles = { "Admin", "BookingSpecialist", "Customer" };

        public async Task<IActionResult> Index(string? search, string? role)
        {
            var query = _contextEventEase.Users.AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                var trimmedSearch = search.Trim();
                query = query.Where(u => u.Email.Contains(trimmedSearch));
            }

            if (!string.IsNullOrWhiteSpace(role))
            {
                query = query.Where(u => u.Role == role);
            }

            ViewBag.SearchTerm = search;
            ViewBag.SelectedRole = role;
            return View(await query.OrderBy(u => u.Email).ToListAsync());
        }

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
            await ValidateUserAsync(user);
            if (!ModelState.IsValid) { return View(user); }

            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
            _contextEventEase.Add(user);
            await _contextEventEase.SaveChangesAsync();
            TempData["SuccessMessage"] = "User created successfully.";
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

            var existingUser = await _contextEventEase.Users.AsNoTracking().FirstOrDefaultAsync(u => u.UserId == id);
            if (existingUser == null) { return NotFound(); }

            var passwordWasLeftBlank = string.IsNullOrWhiteSpace(user.Password);
            if (passwordWasLeftBlank)
            {
                ModelState.Remove("Password");
            }

            await ValidateUserAsync(user);
            if (!ModelState.IsValid) { return View(user); }

            user.Password = passwordWasLeftBlank
                ? existingUser.Password
                : BCrypt.Net.BCrypt.HashPassword(user.Password);

            _contextEventEase.Update(user);
            await _contextEventEase.SaveChangesAsync();
            TempData["SuccessMessage"] = "User updated successfully.";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) { return NotFound(); }

            var item = await _contextEventEase.Users.FirstOrDefaultAsync(u => u.UserId == id);

            if (item == null) { return NotFound(); }

            ViewBag.HasLinkedBookings = await HasLinkedBookingsAsync(item.UserId);
            return View(item);
        }

        [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var item = await _contextEventEase.Users.FindAsync(id);
            if (item == null) { return NotFound(); }

            if (await HasLinkedBookingsAsync(id))
            {
                return RedirectToAction(nameof(Delete), new { id });
            }

            try
            {
                _contextEventEase.Users.Remove(item);
                await _contextEventEase.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                return RedirectToAction(nameof(Delete), new { id });
            }

            TempData["SuccessMessage"] = "User deleted successfully.";
            return RedirectToAction(nameof(Index));
        }

        private async Task ValidateUserAsync(User user)
        {
            if (!string.IsNullOrWhiteSpace(user.Email))
            {
                var normalizedEmail = user.Email.Trim();
                var emailExists = await _contextEventEase.Users.AnyAsync(u =>
                    u.UserId != user.UserId &&
                    u.Email == normalizedEmail);

                if (emailExists)
                {
                    ModelState.AddModelError("Email", "A user with this email already exists.");
                }
            }

            if (!string.IsNullOrWhiteSpace(user.Role) && !ValidRoles.Contains(user.Role))
            {
                ModelState.AddModelError("Role", "Choose a valid user role.");
            }
        }

        private async Task<bool> HasLinkedBookingsAsync(int userId)
        {
            return await _contextEventEase.Bookings.AnyAsync(b => b.UserId == userId);
        }
    }
}
