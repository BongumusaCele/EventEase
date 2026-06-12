using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventEase.Models
{
    public class Event
    {
        public int EventId { get; set; }

        [Required(ErrorMessage = "Event name is required")]
        [StringLength(200, ErrorMessage = "Event name cannot exceed 200 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Start date and time is required")]
        public DateTime StartDateTime { get; set; }

        [Required(ErrorMessage = "End date and time is required")]
        public DateTime EndDateTime { get; set; }

        [StringLength(1000, ErrorMessage = "Description cannot exceed 1000 characters")]
        public string? Description { get; set; }

        [StringLength(500, ErrorMessage = "Image URL cannot exceed 500 characters")]
        public string? ImageUrl { get; set; }

        [Required(ErrorMessage = "Event type is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Choose an event type")]
        public int EventTypeId { get; set; }

        [ForeignKey("EventTypeId")]
        public EventType? EventType { get; set; }

        public int? VenueId { get; set; }

        [ForeignKey("VenueId")]
        public Venue? Venue { get; set; }

        public List<Booking>? Bookings { get; set; }
    }
}
