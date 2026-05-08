using EventEase.Data;
using EventEase.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace EventEase.Controllers
{
    /// <summary>
    /// Authentication Controller
    /// 
    /// Handles user login, registration, logout, and session management.
    /// 
    /// Authentication Pattern: Cookie-based Authentication
    /// Reference: https://learn.microsoft.com/en-us/aspnet/core/security/authentication
    /// 
    /// Password Hashing: BCrypt.Net-Next (v4.0.3)
    /// License: MIT
    /// Reference: https://github.com/BcryptNet/bcrypt.net
    /// 
    /// Framework: ASP.NET Core (MIT License)
    /// Database: Entity Framework Core (MIT License)
    /// 
    /// Author: EventEase Team
    /// Created: 2025
    /// </summary>
    public class AuthController : Controller
    {
        private readonly EventEaseContext _context;
        private readonly ILogger<AuthController> _logger;

        public AuthController(EventEaseContext context, ILogger<AuthController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: Auth/Login
        public IActionResult Login()
        {
            if (User?.Identity?.IsAuthenticated ?? false)
                return RedirectToAction("Index", "Dashboard");
            return View();
        }

        // POST: Auth/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string email, string password)
        {
            try
            {
                if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                {
                    ViewData["Email"] = email;
                    ModelState.AddModelError("", "Email and password are required.");
                    return View();
                }

                var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

                if (user == null || !VerifyPassword(password, user.Password))
                {
                    ViewData["Email"] = email;
                    ModelState.AddModelError("", "Invalid email or password.");
                    _logger.LogWarning($"Failed login attempt for email: {email}");
                    return View();
                }

                // Create claims
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                    new Claim(ClaimTypes.Name, user.Email),
                    new Claim(ClaimTypes.Role, user.Role ?? "Customer"),
                    new Claim("UserId", user.UserId.ToString())
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = true,
                    ExpiresUtc = DateTimeOffset.UtcNow.AddDays(7)
                };

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties);

                _logger.LogInformation($"User {email} logged in successfully");

                // Redirect based on role
                return user.Role switch
                {
                    "Admin" or "BookingSpecialist" => RedirectToAction("Index", "Dashboard"),
                    "Customer" => RedirectToAction("CustomerDashboard", "Dashboard"),
                    _ => RedirectToAction("Index", "Home")
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during login");
                ViewData["Email"] = email;
                ModelState.AddModelError("", "An error occurred during login. Please try again.");
                return View();
            }
        }

        // GET: Auth/Register
        public IActionResult Register()
        {
            if (User?.Identity?.IsAuthenticated ?? false)
                return RedirectToAction("Index", "Dashboard");
            return View();
        }

        // POST: Auth/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(string email, string password, string confirmPassword, string fullName, string role)
        {
            try
            {
                if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                {
                    ModelState.AddModelError("", "Email and password are required.");
                    return View();
                }

                if (password != confirmPassword)
                {
                    ModelState.AddModelError("", "Passwords do not match.");
                    return View();
                }

                if (password.Length < 6)
                {
                    ModelState.AddModelError("", "Password must be at least 6 characters.");
                    return View();
                }

                if (string.IsNullOrEmpty(role))
                {
                    ModelState.AddModelError("", "Please select a role.");
                    return View();
                }

                // Validate role
                var validRoles = new[] { "Admin", "BookingSpecialist", "Customer" };
                if (!validRoles.Contains(role))
                {
                    ModelState.AddModelError("", "Invalid role selected.");
                    return View();
                }

                var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
                if (existingUser != null)
                {
                    ModelState.AddModelError("", "This email is already registered.");
                    return View();
                }

                var newUser = new User
                {
                    Email = email,
                    Password = HashPassword(password),
                    Role = role  // Use selected role instead of default
                };

                _context.Users.Add(newUser);
                await _context.SaveChangesAsync();

                // Create customer profile (if role is Customer)
                if (role == "Customer")
                {
                    var customer = new Customer
                    {
                        Name = fullName ?? email,
                        Email = email,
                        Phone = ""
                    };

                    _context.Customers.Add(customer);
                    await _context.SaveChangesAsync();
                }

                _logger.LogInformation($"New user registered with role {role}: {email}");

                ViewBag.Success = "Account created successfully! Redirecting to login...";
                return RedirectToAction("Login");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during registration");
                ModelState.AddModelError("", "An error occurred during registration.");
                return View();
            }
        }

        // POST: Auth/Logout
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            _logger.LogInformation($"User {User.Identity?.Name} logged out");
            return RedirectToAction("Login", "Auth");
        }

        // GET: Auth/AccessDenied
        public IActionResult AccessDenied()
        {
            return View();
        }

        // Helper methods
        private string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        // GET: Auth/Error
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = System.Diagnostics.Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private bool VerifyPassword(string password, string hash)
        {
            try
            {
                return BCrypt.Net.BCrypt.Verify(password, hash);
            }
            catch
            {
                return false;
            }
        }
    }
}
