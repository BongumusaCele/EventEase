using EventEase.Data;
using EventEase.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EventEase.Controllers
{
    [Authorize]
    public class CustomerController : Controller
    {
        private readonly EventEaseContext _contextEventEase;

        public CustomerController(EventEaseContext contextEventEase)
        {
            _contextEventEase = contextEventEase;
        }

        public async Task<IActionResult> Index(string? search)
        {
            var query = _contextEventEase.Customers.AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                var trimmedSearch = search.Trim();
                query = query.Where(c =>
                    c.Name.Contains(trimmedSearch) ||
                    c.Email.Contains(trimmedSearch) ||
                    (c.Phone != null && c.Phone.Contains(trimmedSearch)));
            }

            var customers = await query.OrderBy(c => c.Name).ToListAsync();
            ViewBag.SearchTerm = search;
            return View(customers);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) { return NotFound(); }

            var item = await _contextEventEase.Customers.FirstOrDefaultAsync(c => c.CustomerId == id);

            if (item == null) { return NotFound(); }

            return View(item);
        }

        public IActionResult Create() {  return View(); }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Customer customer)
        {
            await ValidateCustomerAsync(customer);
            if (!ModelState.IsValid) { return View(customer); }

            customer.Phone ??= string.Empty;
            _contextEventEase.Add(customer);
            await _contextEventEase.SaveChangesAsync();
            TempData["SuccessMessage"] = "Customer created successfully.";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) { return NotFound(); }

            var item = await _contextEventEase.Customers.FindAsync(id);

            if (item == null) { return NotFound(); }

            return View(item);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Customer customer)
        {
            if (id != customer.CustomerId) { return NotFound(); }

            await ValidateCustomerAsync(customer);
            if (!ModelState.IsValid) { return View(customer); }

            customer.Phone ??= string.Empty;
            _contextEventEase.Update(customer);
            await _contextEventEase.SaveChangesAsync();
            TempData["SuccessMessage"] = "Customer updated successfully.";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) { return NotFound(); }

            var item = await _contextEventEase.Customers.FirstOrDefaultAsync(c => c.CustomerId == id);

            if (item == null) { return NotFound(); }

            ViewBag.HasLinkedBookings = await HasLinkedBookingsAsync(item.CustomerId);
            return View(item);
        }

        [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var item = await _contextEventEase.Customers.FindAsync(id);

            if (item == null) { return NotFound(); }

            if (await HasLinkedBookingsAsync(id))
            {
                return RedirectToAction(nameof(Delete), new { id });
            }

            try
            {
                _contextEventEase.Customers.Remove(item);
                await _contextEventEase.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                return RedirectToAction(nameof(Delete), new { id });
            }

            TempData["SuccessMessage"] = "Customer deleted successfully.";
            return RedirectToAction(nameof(Index));
        }

        private async Task ValidateCustomerAsync(Customer customer)
        {
            if (!string.IsNullOrWhiteSpace(customer.Email))
            {
                var normalizedEmail = customer.Email.Trim();
                var emailExists = await _contextEventEase.Customers.AnyAsync(c =>
                    c.CustomerId != customer.CustomerId &&
                    c.Email == normalizedEmail);

                if (emailExists)
                {
                    ModelState.AddModelError(nameof(Customer.Email), "A customer with this email already exists.");
                }
            }
        }

        private async Task<bool> HasLinkedBookingsAsync(int customerId)
        {
            return await _contextEventEase.Bookings.AnyAsync(b => b.CustomerId == customerId);
        }
    }
}
