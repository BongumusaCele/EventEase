using EventEase.Data;
using EventEase.Models;
using EventEase.Models.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace EventEase.Controllers
{
    [Authorize]
    public class BookingsController : Controller
    {
        private readonly EventEaseContext _contextEventEase;

        public BookingsController(EventEaseContext contextEventEase)
        {
            _contextEventEase = contextEventEase;
        }

        public async Task<IActionResult> Index(string? search, int? eventTypeId, DateTime? startDate, DateTime? endDate, bool? venueAvailable)
        {
            var query = _contextEventEase.BookingDetailsView.AsNoTracking();

            if (!string.IsNullOrWhiteSpace(search))
            {
                var trimmedSearch = search.Trim();
                query = int.TryParse(trimmedSearch, out var bookingId)
                    ? query.Where(b => b.BookingId == bookingId || b.EventName.Contains(trimmedSearch))
                    : query.Where(b => b.EventName.Contains(trimmedSearch));
            }

            if (eventTypeId.HasValue)
            {
                query = query.Where(b => b.EventTypeId == eventTypeId.Value);
            }

            if (startDate.HasValue && endDate.HasValue && endDate.Value.Date < startDate.Value.Date)
            {
                ViewBag.FilterError = "End date must be on or after start date.";
            }
            else
            {
                if (startDate.HasValue)
                {
                    query = query.Where(b => b.BookingEndDateTime >= startDate.Value.Date);
                }

                if (endDate.HasValue)
                {
                    var exclusiveEndDate = endDate.Value.Date.AddDays(1);
                    query = query.Where(b => b.BookingStartDateTime < exclusiveEndDate);
                }
            }

            if (venueAvailable.HasValue)
            {
                query = query.Where(b => b.VenueIsAvailable == venueAvailable.Value);
            }

            var items = await query
                .OrderByDescending(b => b.BookingId)
                .ToListAsync();

            ViewBag.SearchTerm = search;
            ViewBag.SelectedEventTypeId = eventTypeId;
            ViewBag.StartDate = startDate?.ToString("yyyy-MM-dd");
            ViewBag.EndDate = endDate?.ToString("yyyy-MM-dd");
            ViewBag.SelectedVenueAvailable = venueAvailable?.ToString().ToLowerInvariant();
            ViewBag.EventTypes = new SelectList(
                await _contextEventEase.EventTypes.OrderBy(et => et.Name).ToListAsync(),
                "EventTypeId",
                "Name",
                eventTypeId);
            return View(items);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) { return NotFound(); }

            var item = await _contextEventEase.Bookings
                .Include(b => b.Venue)
                .Include(b => b.Event)
                .Include(b => b.Customer)
                .Include(b => b.User)
                .FirstOrDefaultAsync(b => b.BookingId == id);

            if (item == null) { return NotFound(); }

            return View(item);
        }

        public async Task<IActionResult> Create()
        {
            return View(await BuildVmAsync(new BookingForm
            {
                StartDateTime = DateTime.Now,
                EndDateTime = DateTime.Now.AddHours(1),
                Status = "Confirmed"
            }));
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BookingForm vm)
        {
            if (vm.StartDateTime.HasValue && vm.EndDateTime.HasValue && vm.EndDateTime <= vm.StartDateTime)
                ModelState.AddModelError("", "End date/time must be after Start date/time.");

            if (await HasVenueConflictAsync(vm.VenueId, vm.StartDateTime, vm.EndDateTime))
                ModelState.AddModelError("", "This venue is already booked during the selected date and time.");

            if (!ModelState.IsValid)
            { return View(await BuildVmAsync(vm)); }

            var booking = new Booking
            {
                StartDateTime = vm.StartDateTime.GetValueOrDefault(),
                EndDateTime = vm.EndDateTime.GetValueOrDefault(),
                Status = vm.Status,
                CustomerId = vm.CustomerId.GetValueOrDefault(),
                VenueId = vm.VenueId.GetValueOrDefault(),
                EventId = vm.EventId.GetValueOrDefault(),
                UserId = vm.UserId.GetValueOrDefault()
            };

            _contextEventEase.Add(booking);
            await _contextEventEase.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) { return NotFound(); } 

            var booking = await _contextEventEase.Bookings.FindAsync(id);

            if (booking == null) { return NotFound(); }

            var vm = new BookingForm
            {
                BookingId = booking.BookingId,
                StartDateTime = booking.StartDateTime,
                EndDateTime = booking.EndDateTime,
                Status = booking.Status,
                CustomerId = booking.CustomerId,
                VenueId = booking.VenueId,
                EventId = booking.EventId,
                UserId = booking.UserId
            };

            return View(await BuildVmAsync(vm));
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, BookingForm vm)
        {
            if (id != vm.BookingId) { return NotFound(); }

            if (vm.StartDateTime.HasValue && vm.EndDateTime.HasValue && vm.EndDateTime <= vm.StartDateTime)
            { ModelState.AddModelError("", "End date/time must be after Start date/time."); }

            if (await HasVenueConflictAsync(vm.VenueId, vm.StartDateTime, vm.EndDateTime, id))
            { ModelState.AddModelError("", "This venue is already booked during the selected date and time."); }

            if (!ModelState.IsValid)
            { return View(await BuildVmAsync(vm)); }

            var booking = await _contextEventEase.Bookings.FindAsync(id);

            if (booking == null) { return NotFound(); }

            booking.StartDateTime = vm.StartDateTime.GetValueOrDefault();
            booking.EndDateTime = vm.EndDateTime.GetValueOrDefault();
            booking.Status = vm.Status;
            booking.CustomerId = vm.CustomerId.GetValueOrDefault();
            booking.VenueId = vm.VenueId.GetValueOrDefault();
            booking.EventId = vm.EventId.GetValueOrDefault();
            booking.UserId = vm.UserId.GetValueOrDefault();

            await _contextEventEase.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) { return NotFound(); }
            var item = await _contextEventEase.Bookings
                .Include(b => b.Venue)
                .Include(b => b.Event)
                .Include(b => b.Customer)
                .Include(b => b.User)
                .FirstOrDefaultAsync(b => b.BookingId == id);

            if (item == null) { return NotFound(); }
            return View(item);
        }

        [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var item = await _contextEventEase.Bookings.FindAsync(id);
            if (item == null) { return NotFound(); }

            _contextEventEase.Bookings.Remove(item);
            await _contextEventEase.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private async Task<BookingForm> BuildVmAsync(BookingForm vm)
        {
            vm.Customers = (await _contextEventEase.Customers.ToListAsync())
                .Select(c => new SelectListItem { Value = c.CustomerId.ToString(), Text = c.Name });

            vm.Venues = (await _contextEventEase.Venues.ToListAsync())
                .Select(v => new SelectListItem { Value = v.VenueId.ToString(), Text = v.Name });

            vm.Events = (await _contextEventEase.Events.ToListAsync())
                .Select(e => new SelectListItem { Value = e.EventId.ToString(), Text = e.Name });

            vm.Users = (await _contextEventEase.Users.ToListAsync())
                .Select(u => new SelectListItem { Value = u.UserId.ToString(), Text = u.Email });

            return vm;
        }

        private async Task<bool> HasVenueConflictAsync(int? venueId, DateTime? startDateTime, DateTime? endDateTime, int? ignoredBookingId = null)
        {
            if (!venueId.HasValue || !startDateTime.HasValue || !endDateTime.HasValue || venueId.Value <= 0)
            {
                return false;
            }

            return await _contextEventEase.Bookings.AnyAsync(b =>
                b.VenueId == venueId.Value &&
                (!ignoredBookingId.HasValue || b.BookingId != ignoredBookingId.Value) &&
                b.Status != "Cancelled" &&
                startDateTime.Value < b.EndDateTime &&
                b.StartDateTime < endDateTime.Value);
        }
    }
}
