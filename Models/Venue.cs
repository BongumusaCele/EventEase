namespace EventEase.Models
{
    public class Venue
    {
        public int VenueId { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public int Capacity { get; set; }
        public string ImageUrl { get; set; }
        public List<Event> Events { get; set; }
        public List<Booking> Bookings { get; set; }
    }
}