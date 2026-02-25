using EventEase.Data;
using EventEase.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

namespace EventEase.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly EventEaseContext _context;
        private readonly ILogger<LoginModel> _logger;

        [BindProperty]
        public string Email { get; set; }

        [BindProperty]
        public string Password { get; set; }

        public string ErrorMessage { get; set; }

        public LoginModel(EventEaseContext context, ILogger<LoginModel> logger)
        {
            _context = context;
            _logger = logger;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Password))
            {
                ErrorMessage = "Email and password are required.";
                return Page();
            }

            var user = _context.Users.FirstOrDefault(u => u.Email == Email);
            if (user == null)
            {
                ErrorMessage = "Invalid email or password.";
                return Page();
            }

            var ph = new Microsoft.AspNetCore.Identity.PasswordHasher<User>();
            var result = ph.VerifyHashedPassword(user, user.Password, Password);
            if (result == Microsoft.AspNetCore.Identity.PasswordVerificationResult.Failed)
            {
                ErrorMessage = "Invalid email or password.";
                return Page();
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.Role, user.Role ?? string.Empty)
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties { IsPersistent = true };

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity), authProperties);

            return RedirectToPage("/Dashboard/Index");
        }
    }
}
