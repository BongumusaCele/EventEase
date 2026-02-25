using EventEase.Data;
using EventEase.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EventEase.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly EventEaseContext _context;
        private readonly ILogger<RegisterModel> _logger;

        [BindProperty]
        public string Email { get; set; }

        [BindProperty]
        public string Password { get; set; }

        [BindProperty]
        public string ConfirmPassword { get; set; }

        public string ErrorMessage { get; set; }
        public string SuccessMessage { get; set; }

        public RegisterModel(EventEaseContext context, ILogger<RegisterModel> logger)
        {
            _context = context;
            _logger = logger;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Password) || string.IsNullOrEmpty(ConfirmPassword))
            {
                ErrorMessage = "All fields are required.";
                return Page();
            }

            if (Password != ConfirmPassword)
            {
                ErrorMessage = "Passwords do not match.";
                return Page();
            }

            if (Password.Length < 8)
            {
                ErrorMessage = "Password must be at least 8 characters long.";
                return Page();
            }

            if (_context.Users.Any(u => u.Email == Email))
            {
                ErrorMessage = "Email is already registered.";
                return Page();
            }

            try
            {
                var ph = new PasswordHasher<User>();
                var newUser = new User
                {
                    Email = Email,
                    Role = "Customer" // Default role for registration
                };
                newUser.Password = ph.HashPassword(newUser, Password);

                _context.Users.Add(newUser);
                await _context.SaveChangesAsync();

                _logger.LogInformation("New user registered: {Email}", Email);
                SuccessMessage = "Account created successfully! You can now log in.";

                // Redirect to login after delay
                return RedirectToPage("Login");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during registration");
                ErrorMessage = "An error occurred during registration. Please try again.";
                return Page();
            }
        }
    }
}
