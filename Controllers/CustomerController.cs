using EventEase.Data;
using EventEase.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EventEase.Controllers
{
    public class CustomerController : Controller
    {
        private readonly EventEaseContext _contextEventEase;

        public CustomerController(EventEaseContext contextEventEase)
        {
            _contextEventEase = contextEventEase;
        }

        public async Task<IActionResult> Index()
        {
            var customers = await _contextEventEase.Customers.ToListAsync();
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
            if (!ModelState.IsValid) { return View(customer); }

            _contextEventEase.Add(customer);
            await _contextEventEase.SaveChangesAsync();
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

            if (!ModelState.IsValid) { return View(customer); }

            _contextEventEase.Update(customer);
            await _contextEventEase.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) { return NotFound(); }

            var item = await _contextEventEase.Customers.FirstOrDefaultAsync(c => c.CustomerId == id);

            if (item == null) { return NotFound(); }

            return View(item);
        }

        [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var item = await _contextEventEase.Customers.FindAsync(id);

            if (item == null) { return NotFound(); }

            _contextEventEase.Customers.Remove(item);
            await _contextEventEase.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
