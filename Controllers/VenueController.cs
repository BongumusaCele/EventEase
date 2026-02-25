using EventEase.Data;
using EventEase.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EventEase.Controllers
{
    public class VenueController : Controller
    {
        private readonly EventEaseContext _contextEventEase;

        public VenueController(EventEaseContext contextEventEase)
        {
            _contextEventEase = contextEventEase;
        }

        public async Task<IActionResult> Index()
        {
            var venues = await _contextEventEase.Venues.ToListAsync();
            return View(venues);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) { return NotFound(); }

            var venue = await _contextEventEase.Venues.FirstOrDefaultAsync(v => v.VenueId == id);

            if (venue == null) { return NotFound(); }

            return View(venue);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Venue venue)
        {
            if (!ModelState.IsValid) { return View(venue); }

            _contextEventEase.Add(venue);
            await _contextEventEase.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) { return NotFound(); }

            var venue = await _contextEventEase.Venues.FindAsync(id);

            if (venue == null) { return NotFound(); }

            return View(venue);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Venue venue)
        {
            if (id != venue.VenueId) { return NotFound(); }

            if (!ModelState.IsValid) { return View(venue); }

            _contextEventEase.Update(venue);
            await _contextEventEase.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) { return NotFound(); }

            var venue = await _contextEventEase.Venues.FirstOrDefaultAsync(v => v.VenueId == id);

            if (venue == null) { return NotFound(); }

            return View(venue);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var venue = await _contextEventEase.Venues.FindAsync(id);

            if (venue == null) { return NotFound(); }

            _contextEventEase.Venues.Remove(venue);
            await _contextEventEase.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
