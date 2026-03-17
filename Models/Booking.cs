using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventEase.Models
{
    /// <summary>
    /// Booking Model
    /// 
    /// Represents an event booking in the EventEase system.
    /// 
    /// Validation Pattern:
    /// Data Annotations are used for declarative validation.
    /// Reference: https://learn.microsoft.com/en-us/dotnet/api/system.componentmodel.dataannotations
    /// 
    /// Framework: Entity Framework Core (MIT License)
    /// Reference: https://github.com/dotnet/efcore
    /// 
    /// Attributes Used:
    /// - [Required]: Marks field as mandatory
    /// - [StringLength]: Constrains string field length
    /// - [ForeignKey]: Marks foreign key relationship
    /// 
    /// Author: EventEase Team
    /// Created: 2025
    /// </summary>
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