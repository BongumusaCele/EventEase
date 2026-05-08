using EventEase.Data;
using EventEase.Models;
using EventEase.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EventEase.Controllers
{
    [Authorize]
    public class VenueController : Controller
    {
        private readonly EventEaseContext _contextEventEase;
        private readonly IBlobStorageService _blobStorageService;

        public VenueController(EventEaseContext contextEventEase, IBlobStorageService blobStorageService)
        {
            _contextEventEase = contextEventEase;
            _blobStorageService = blobStorageService;
        }

        public async Task<IActionResult> Index(string? search)
        {
            var query = _contextEventEase.Venues.AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                var trimmedSearch = search.Trim();
                query = int.TryParse(trimmedSearch, out var capacity)
                    ? query.Where(v =>
                        v.Name.Contains(trimmedSearch) ||
                        v.Location.Contains(trimmedSearch) ||
                        v.Capacity == capacity)
                    : query.Where(v =>
                        v.Name.Contains(trimmedSearch) ||
                        v.Location.Contains(trimmedSearch));
            }

            var venues = await query.OrderBy(v => v.Name).ToListAsync();
            ViewBag.SearchTerm = search;
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
        public async Task<IActionResult> Create(Venue venue, IFormFile? imageFile)
        {
            if (!ModelState.IsValid) { return View(venue); }

            if (imageFile == null || imageFile.Length == 0)
            {
                ModelState.AddModelError("", "Please upload a venue image before creating the venue.");
                ModelState.AddModelError(nameof(Venue.ImageUrl), "Venue image is required.");
                return View(venue);
            }

            try
            {
                var uploadedImageUrl = await _blobStorageService.UploadImageAsync(imageFile, "venues");
                if (!string.IsNullOrEmpty(uploadedImageUrl))
                {
                    venue.ImageUrl = uploadedImageUrl;
                }
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(venue);
            }

            try
            {
                _contextEventEase.Add(venue);
                await _contextEventEase.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "We could not save this venue. Please check the venue details and try again.");
                return View(venue);
            }

            TempData["SuccessMessage"] = "Venue created successfully.";
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
        public async Task<IActionResult> Edit(int id, Venue venue, IFormFile? imageFile)
        {
            if (id != venue.VenueId) { return NotFound(); }

            var existingVenue = await _contextEventEase.Venues
                .AsNoTracking()
                .FirstOrDefaultAsync(v => v.VenueId == id);

            if (existingVenue == null) { return NotFound(); }

            if (!ModelState.IsValid) { return View(venue); }

            try
            {
                var uploadedImageUrl = await _blobStorageService.UploadImageAsync(imageFile, "venues");
                if (!string.IsNullOrEmpty(uploadedImageUrl))
                {
                    venue.ImageUrl = uploadedImageUrl;
                }
                else
                {
                    venue.ImageUrl = existingVenue.ImageUrl;
                }
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(venue);
            }

            if (string.IsNullOrWhiteSpace(venue.ImageUrl))
            {
                ModelState.AddModelError("", "Please upload a venue image before saving this venue.");
                ModelState.AddModelError(nameof(Venue.ImageUrl), "Venue image is required.");
                return View(venue);
            }

            try
            {
                _contextEventEase.Update(venue);
                await _contextEventEase.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "We could not update this venue. Please check the venue details and try again.");
                return View(venue);
            }

            TempData["SuccessMessage"] = "Venue updated successfully.";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) { return NotFound(); }

            var venue = await _contextEventEase.Venues.FirstOrDefaultAsync(v => v.VenueId == id);

            if (venue == null) { return NotFound(); }

            ViewBag.HasLinkedBookings = await HasLinkedBookingsAsync(venue.VenueId);
            return View(venue);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var venue = await _contextEventEase.Venues.FindAsync(id);

            if (venue == null) { return NotFound(); }

            if (await HasLinkedBookingsAsync(id))
            {
                return RedirectToAction(nameof(Delete), new { id });
            }

            try
            {
                var linkedEvents = await _contextEventEase.Events
                    .Where(e => e.VenueId == id)
                    .ToListAsync();

                foreach (var linkedEvent in linkedEvents)
                {
                    linkedEvent.VenueId = null;
                }

                _contextEventEase.Venues.Remove(venue);
                await _contextEventEase.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                return RedirectToAction(nameof(Delete), new { id });
            }

            TempData["SuccessMessage"] = "Venue deleted successfully.";
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> HasLinkedBookingsAsync(int venueId)
        {
            return await _contextEventEase.Bookings.AnyAsync(b => b.VenueId == venueId);
        }
    }
}
