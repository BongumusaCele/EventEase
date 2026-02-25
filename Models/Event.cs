using System.ComponentModel.DataAnnotations.Schema;

namespace EventEase.Models
{
    public class Event
    {
        public int EventId { get; set; }
        public string Name { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public string Description { get; set; }
        public int? VenueId { get; set; }
        [ForeignKey("VenueId")]
        public Venue? Venue { get; set; }

        public List<Booking>? Bookings { get; set; }
    }
}