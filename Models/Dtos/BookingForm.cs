using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace EventEase.Models.Dtos
{
    public class BookingForm
    {
        public int BookingId { get; set; }

        [Required(ErrorMessage = "Choose a booking start date and time.")]
        public DateTime? StartDateTime { get; set; }

        [Required(ErrorMessage = "Choose a booking end date and time.")]
        public DateTime? EndDateTime { get; set; }

        [Required(ErrorMessage = "Choose a booking status.")]
        public string Status { get; set; } = string.Empty;

        [Required(ErrorMessage = "Choose a customer.")]
        public int? CustomerId { get; set; }

        [Required(ErrorMessage = "Choose a venue.")]
        public int? VenueId { get; set; }

        [Required(ErrorMessage = "Choose an event.")]
        public int? EventId { get; set; }

        [Required(ErrorMessage = "Choose a booking specialist.")]
        public int? UserId { get; set; }

        public IEnumerable<SelectListItem> Customers { get; set; } = new List<SelectListItem>();
        public IEnumerable<SelectListItem> Venues { get; set; } = new List<SelectListItem>();
        public IEnumerable<SelectListItem> Events { get; set; } = new List<SelectListItem>();
        public IEnumerable<SelectListItem> Users { get; set; } = new List<SelectListItem>();
    }
}
