using Microsoft.AspNetCore.Mvc;
using EventEase.Models;
using EventEase.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EventEase.Controllers
{
    public class EventController : Controller
    {
        private readonly EventEaseContext _contextEventEase;

        public EventController(EventEaseContext contextEventEase)
        {
            _contextEventEase = contextEventEase;
        }

        public async Task<IActionResult> Index() {

           List<Event> items = await _contextEventEase.Events.Include(v => v.Venue).ToListAsync();

            return View(items);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) { return NotFound(); }

            var myEvents = await _contextEventEase.Events.Include(e => e.Venue).FirstOrDefaultAsync(e => e.EventId == id);

            if (myEvents == null) { return NotFound(); }

            return View(myEvents);
        }

        public async Task<IActionResult> CreateAsync() 
        {
            ViewBag.Venues = new SelectList(await _contextEventEase.Venues.ToListAsync(), "VenueId", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EventId, Name, StartDateTime, EndDateTime, Description, VenueId")] Event item)
        {
            if (ModelState.IsValid)
            {
                _contextEventEase.Add(item);
                await _contextEventEase.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(item);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) { return NotFound(); }

            var ev = await _contextEventEase.Events.FindAsync(id);

            if (ev == null) { return NotFound(); }

            ViewBag.Venues = new SelectList(await _contextEventEase.Venues.ToListAsync(), "VenueId", "Name", ev.VenueId);

            return View(ev);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Event item)
        {
            if (id != item.EventId) { return NotFound(); }

            if (!ModelState.IsValid)
            {
                ViewBag.Venues = new SelectList(await _contextEventEase.Venues.ToListAsync(), "VenueId", "Name", item.VenueId);
                return View(item);
            }

            _contextEventEase.Update(item);
            await _contextEventEase.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var item = await _contextEventEase.Events.FirstOrDefaultAsync(x => x.EventId == id);
            return View(item);
        }

        [HttpPost , ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var item = await _contextEventEase.Events.FindAsync(id);

            if(item != null)
            {
                _contextEventEase.Events.Remove(item);
                await _contextEventEase.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}