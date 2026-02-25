using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventEase.Models
{
    public class Booking
    {
        public int BookingId { get; set; }

        [Required(ErrorMessage = "Status is required")]
        [StringLength(50, ErrorMessage = "Status cannot exceed 50 characters")]
        public string Status { get; set; }

        [Required(ErrorMessage = "Start date and time is required")]
        public DateTime StartDateTime { get; set; }

        [Required(ErrorMessage = "End date and time is required")]
        public DateTime EndDateTime { get; set; }

        [Required(ErrorMessage = "Customer is required")]
        public int CustomerId { get; set; }

        [ForeignKey("CustomerId")]
        public Customer? Customer { get; set; }

        [Required(ErrorMessage = "Venue is required")]
        public int VenueId { get; set; }

        [ForeignKey("VenueId")]
        public Venue? Venue { get; set; }

        [Required(ErrorMessage = "Event is required")]
        public int EventId { get; set; }

        [ForeignKey("EventId")]
        public Event? Event { get; set; }

        [Required(ErrorMessage = "User is required")]
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public User? User { get; set; }
    }
}