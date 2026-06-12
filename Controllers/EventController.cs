using EventEase.Data;
using EventEase.Models;
using EventEase.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace EventEase.Controllers
{
    [Authorize]
    public class EventController : Controller
    {
        private readonly EventEaseContext _contextEventEase;
        private readonly IBlobStorageService _blobStorageService;

        public EventController(EventEaseContext contextEventEase, IBlobStorageService blobStorageService)
        {
            _contextEventEase = contextEventEase;
            _blobStorageService = blobStorageService;
        }

        public async Task<IActionResult> Index(string? search, int? venueId)
        {
            var query = _contextEventEase.Events
                .Include(e => e.Venue)
                .Include(e => e.EventType)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                var trimmedSearch = search.Trim();
                query = query.Where(e =>
                    e.Name.Contains(trimmedSearch) ||
                    (e.Description != null && e.Description.Contains(trimmedSearch)) ||
                    (e.EventType != null && e.EventType.Name.Contains(trimmedSearch)) ||
                    (e.Venue != null && e.Venue.Name.Contains(trimmedSearch)));
            }

            if (venueId.HasValue)
            {
                query = query.Where(e => e.VenueId == venueId.Value);
            }

            var items = await query.OrderByDescending(e => e.StartDateTime).ToListAsync();
            ViewBag.SearchTerm = search;
            ViewBag.SelectedVenueId = venueId;
            ViewBag.Venues = new SelectList(await _contextEventEase.Venues.OrderBy(v => v.Name).ToListAsync(), "VenueId", "Name", venueId);
            return View(items);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) { return NotFound(); }

            var myEvents = await _contextEventEase.Events
                .Include(e => e.Venue)
                .Include(e => e.EventType)
                .FirstOrDefaultAsync(e => e.EventId == id);

            if (myEvents == null) { return NotFound(); }

            return View(myEvents);
        }

        public async Task<IActionResult> CreateAsync()
        {
            await PopulateEventFormListsAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EventId, Name, StartDateTime, EndDateTime, Description, ImageUrl, EventTypeId, VenueId")] Event item, IFormFile? imageFile)
        {
            if (item.EndDateTime <= item.StartDateTime)
            {
                ModelState.AddModelError("", "End date/time must be after Start date/time.");
            }

            if (imageFile == null || imageFile.Length == 0)
            {
                ModelState.AddModelError("", "Please upload an event image before creating the event.");
                ModelState.AddModelError(nameof(Event.ImageUrl), "Event image is required.");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var uploadedImageUrl = await _blobStorageService.UploadImageAsync(imageFile, "events");
                    if (!string.IsNullOrEmpty(uploadedImageUrl))
                    {
                        item.ImageUrl = uploadedImageUrl;
                    }
                }
                catch (InvalidOperationException ex)
                {
                    ModelState.AddModelError("", ex.Message);
                    await PopulateEventFormListsAsync(item.VenueId, item.EventTypeId);
                    return View(item);
                }

                try
                {
                    _contextEventEase.Add(item);
                    await _contextEventEase.SaveChangesAsync();
                }
                catch (DbUpdateException)
                {
                    ModelState.AddModelError("", "We could not save this event. Please check the event details and try again.");
                    await PopulateEventFormListsAsync(item.VenueId, item.EventTypeId);
                    return View(item);
                }

                TempData["SuccessMessage"] = "Event created successfully.";
                return RedirectToAction(nameof(Index));
            }

            await PopulateEventFormListsAsync(item.VenueId, item.EventTypeId);
            return View(item);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) { return NotFound(); }

            var ev = await _contextEventEase.Events.FindAsync(id);

            if (ev == null) { return NotFound(); }

            await PopulateEventFormListsAsync(ev.VenueId, ev.EventTypeId);

            return View(ev);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Event item, IFormFile? imageFile)
        {
            if (id != item.EventId) { return NotFound(); }

            var existingEvent = await _contextEventEase.Events
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.EventId == id);

            if (existingEvent == null) { return NotFound(); }

            if (item.EndDateTime <= item.StartDateTime)
            {
                ModelState.AddModelError("", "End date/time must be after Start date/time.");
            }

            if (!ModelState.IsValid)
            {
                await PopulateEventFormListsAsync(item.VenueId, item.EventTypeId);
                return View(item);
            }

            try
            {
                var uploadedImageUrl = await _blobStorageService.UploadImageAsync(imageFile, "events");
                if (!string.IsNullOrEmpty(uploadedImageUrl))
                {
                    item.ImageUrl = uploadedImageUrl;
                }
                else
                {
                    item.ImageUrl = existingEvent.ImageUrl;
                }
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError("", ex.Message);
                await PopulateEventFormListsAsync(item.VenueId, item.EventTypeId);
                return View(item);
            }

            if (string.IsNullOrWhiteSpace(item.ImageUrl))
            {
                ModelState.AddModelError("", "Please upload an event image before saving this event.");
                ModelState.AddModelError(nameof(Event.ImageUrl), "Event image is required.");
                await PopulateEventFormListsAsync(item.VenueId, item.EventTypeId);
                return View(item);
            }

            try
            {
                _contextEventEase.Update(item);
                await _contextEventEase.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "We could not update this event. Please check the event details and try again.");
                await PopulateEventFormListsAsync(item.VenueId, item.EventTypeId);
                return View(item);
            }

            TempData["SuccessMessage"] = "Event updated successfully.";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var item = await _contextEventEase.Events.Include(e => e.Venue).FirstOrDefaultAsync(x => x.EventId == id);
            if (item == null) { return NotFound(); }

            ViewBag.HasLinkedBookings = await HasLinkedBookingsAsync(id);
            return View(item);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var item = await _contextEventEase.Events.FindAsync(id);

            if (item != null)
            {
                if (await HasLinkedBookingsAsync(id))
                {
                    return RedirectToAction(nameof(Delete), new { id });
                }

                try
                {
                    _contextEventEase.Events.Remove(item);
                    await _contextEventEase.SaveChangesAsync();
                }
                catch (DbUpdateException)
                {
                    return RedirectToAction(nameof(Delete), new { id });
                }

                TempData["SuccessMessage"] = "Event deleted successfully.";
            }
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> HasLinkedBookingsAsync(int eventId)
        {
            return await _contextEventEase.Bookings.AnyAsync(b => b.EventId == eventId);
        }

        private async Task PopulateEventFormListsAsync(int? venueId = null, int? eventTypeId = null)
        {
            ViewBag.Venues = new SelectList(await _contextEventEase.Venues.OrderBy(v => v.Name).ToListAsync(), "VenueId", "Name", venueId);
            ViewBag.EventTypes = new SelectList(await _contextEventEase.EventTypes.OrderBy(et => et.Name).ToListAsync(), "EventTypeId", "Name", eventTypeId);
        }
    }
}
