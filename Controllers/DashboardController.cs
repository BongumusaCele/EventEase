using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using EventEase.Data;
using Microsoft.EntityFrameworkCore;

namespace EventEase.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        private readonly EventEaseContext _context;
        private readonly ILogger<DashboardController> _logger;

        public DashboardController(EventEaseContext context, ILogger<DashboardController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [Authorize(Roles = "Admin,BookingSpecialist")]
        public async Task<IActionResult> Index()
        {
            try
            {
                var userRole = User.FindFirst(System.Security.Claims.ClaimTypes.Role)?.Value;
                var totalBookings = await _context.Bookings.CountAsync();
                var totalEvents = await _context.Events.CountAsync();
                var totalVenues = await _context.Venues.CountAsync();
                var totalCustomers = await _context.Customers.CountAsync();

                var dashboardData = new
                {
                    TotalBookings = totalBookings,
                    TotalEvents = totalEvents,
                    TotalVenues = totalVenues,
                    TotalCustomers = totalCustomers,
                    UserRole = userRole,
                    RecentBookings = await _context.Bookings
                        .Include(b => b.Customer)
                        .Include(b => b.Venue)
                        .Include(b => b.Event)
                        .OrderByDescending(b => b.BookingId)
                        .Take(5)
                        .ToListAsync()
                };

                ViewBag.DashboardData = dashboardData;
                return View();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading dashboard");
                return RedirectToAction("Index", "Home");
            }
        }

        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> CustomerDashboard()
        {
            try
            {
                var userEmail = User.Identity?.Name;
                var customerBookings = await _context.Bookings
                    .Include(b => b.Venue)
                    .Include(b => b.Event)
                    .Where(b => b.Customer.Email == userEmail)
                    .OrderByDescending(b => b.BookingId)
                    .ToListAsync();

                ViewBag.Bookings = customerBookings;
                return View();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading customer dashboard");
                return RedirectToAction("Index", "Home");
            }
        }
    }
}
