using Microsoft.AspNetCore.Mvc.Rendering;

namespace EventEase.Models.Dtos
{
    public class BookingForm
    {
        public int BookingId { get; set; }

        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public string Status { get; set; }

        public int CustomerId { get; set; }
        public int VenueId { get; set; }
        public int EventId { get; set; }
        public int UserId { get; set; }

        public IEnumerable<SelectListItem> Customers { get; set; } = new List<SelectListItem>();
        public IEnumerable<SelectListItem> Venues { get; set; } = new List<SelectListItem>();
        public IEnumerable<SelectListItem> Events { get; set; } = new List<SelectListItem>();
        public IEnumerable<SelectListItem> Users { get; set; } = new List<SelectListItem>();
    }
}
