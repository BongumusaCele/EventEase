using EventEase.Data;
using EventEase.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace EventEase.Pages.Dashboard
{
    [Authorize(Policy = "BookingSpecialistOnly")]
    public class IndexModel : PageModel
    {
        private readonly EventEaseContext _context;
        private readonly ILogger<IndexModel> _logger;

        public List<Booking> Bookings { get; set; } = new();
        public List<Venue> Venues { get; set; } = new();
        public List<Event> Events { get; set; } = new();
        
        public int TotalBookings { get; set; }
        public int ActiveEvents { get; set; }
        public int TotalVenues { get; set; }
        public int PendingBookings { get; set; }

        public IndexModel(EventEaseContext context, ILogger<IndexModel> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task OnGetAsync()
        {
            try
            {
                // Load dashboard data
                Bookings = await _context.Bookings
                    .Include(b => b.Event)
                    .Include(b => b.Venue)
                    .Include(b => b.Customer)
                    .OrderByDescending(b => b.StartDateTime)
                    .Take(10)
                    .ToListAsync();

                Venues = await _context.Venues
                    .Take(8)
                    .ToListAsync();

                Events = await _context.Events
                    .Where(e => e.StartDateTime >= DateTime.Now)
                    .OrderBy(e => e.StartDateTime)
                    .Take(10)
                    .ToListAsync();

                // Calculate KPIs
                TotalBookings = await _context.Bookings.CountAsync();
                ActiveEvents = await _context.Events
                    .Where(e => e.StartDateTime >= DateTime.Now)
                    .CountAsync();
                TotalVenues = await _context.Venues.CountAsync();
                PendingBookings = await _context.Bookings
                    .Where(b => b.Status == "Pending")
                    .CountAsync();

                _logger.LogInformation("Dashboard loaded successfully for specialist");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading dashboard data");
            }
        }
    }
}
