namespace EventEase.Models
{
    public class BookingDetailsView
    {
        public int BookingId { get; set; }
        public string Status { get; set; } = string.Empty;
        public DateTime BookingStartDateTime { get; set; }
        public DateTime BookingEndDateTime { get; set; }

        public int VenueId { get; set; }
        public string VenueName { get; set; } = string.Empty;
        public string VenueLocation { get; set; } = string.Empty;
        public int VenueCapacity { get; set; }
        public bool VenueIsAvailable { get; set; }
        public string? VenueImageUrl { get; set; }

        public int EventId { get; set; }
        public string EventName { get; set; } = string.Empty;
        public int EventTypeId { get; set; }
        public string EventTypeName { get; set; } = string.Empty;
        public DateTime EventStartDateTime { get; set; }
        public DateTime EventEndDateTime { get; set; }
        public string? EventDescription { get; set; }
        public string? EventImageUrl { get; set; }

        public int CustomerId { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public string CustomerEmail { get; set; } = string.Empty;

        public int UserId { get; set; }
        public string BookingSpecialistEmail { get; set; } = string.Empty;
    }
}
