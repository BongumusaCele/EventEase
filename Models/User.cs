using System.ComponentModel.DataAnnotations;

namespace EventEase.Models
{
    public class User
    {
        public int UserId { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address")]
        [StringLength(500, ErrorMessage = "Email cannot exceed 500 characters")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(255, MinimumLength = 6, ErrorMessage = "Password must be between 6 and 255 characters")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Role is required")]
        [StringLength(50, ErrorMessage = "Role cannot exceed 50 characters")]
        public string Role { get; set; }

        public List<Booking>? Bookings { get; set; }
    }
}