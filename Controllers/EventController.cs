using Microsoft.AspNetCore.Mvc;
using EventEase.Models;
using EventEase.Data;
using Microsoft.EntityFrameworkCore;

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

        public IActionResult Create() {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("EventId, Name, StartDateTime, EndDateTime, Description, VenueId")] Event item)
        {
            if (ModelState.IsValid)
            {
                _contextEventEase.Add(item);
                await _contextEventEase.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(item);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var item = await _contextEventEase.Events.FirstOrDefaultAsync(x => x.EventId == id);

            return View(item);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int EventId, [Bind("EventId, Name, StartDateTime, EndDateTime, Description, VenueId")] Event item)
        {
            if (ModelState.IsValid)
            {
                _contextEventEase.Update(item);
                await _contextEventEase.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(item);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var item = await _contextEventEase.Events.FirstOrDefaultAsync(x => x.EventId == id);
            return View(item);
        }

        [HttpPost , ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var item = await _contextEventEase.Events.FindAsync(id);

            if(item != null)
            {
                _contextEventEase.Events.Remove(item);
                await _contextEventEase.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }
    }
}