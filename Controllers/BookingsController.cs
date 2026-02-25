using EventEase.Data;
using EventEase.Models;
using EventEase.Models.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace EventEase.Controllers
{
    public class BookingsController : Controller
    {
        private readonly EventEaseContext _contextEventEase;

        public BookingsController(EventEaseContext contextEventEase)
        {
            _contextEventEase = contextEventEase;
        }

        public async Task<IActionResult> Index()
        {
            var items = await _contextEventEase.Bookings
                .Include(b => b.Venue)
                .Include(b => b.Event)
                .Include(b => b.Customer)
                .Include(b => b.User)
                .ToListAsync();

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
            if (vm.EndDateTime <= vm.StartDateTime)
                ModelState.AddModelError("", "End date/time must be after Start date/time.");

            if (!ModelState.IsValid)
            { return View(await BuildVmAsync(vm)); }

            var booking = new Booking
            {
                StartDateTime = vm.StartDateTime,
                EndDateTime = vm.EndDateTime,
                Status = vm.Status,
                CustomerId = vm.CustomerId,
                VenueId = vm.VenueId,
                EventId = vm.EventId,
                UserId = vm.UserId
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

            if (vm.EndDateTime <= vm.StartDateTime)
            { ModelState.AddModelError("", "End date/time must be after Start date/time."); }

            if (!ModelState.IsValid)
            { return View(await BuildVmAsync(vm)); }

            var booking = await _contextEventEase.Bookings.FindAsync(id);

            if (booking == null) { return NotFound(); }

            booking.StartDateTime = vm.StartDateTime;
            booking.EndDateTime = vm.EndDateTime;
            booking.Status = vm.Status;
            booking.CustomerId = vm.CustomerId;
            booking.VenueId = vm.VenueId;
            booking.EventId = vm.EventId;
            booking.UserId = vm.UserId;

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
    }
}
