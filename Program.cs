/* ===== EVENTEASE APPLICATION STARTUP ===== */
/*
  EventEase Event Management System

  Framework: ASP.NET Core 10 (MIT License)
  - Reference: https://github.com/dotnet/aspnetcore

  Third-Party Packages:

  1. Entity Framework Core (MIT License)
     - NuGet: Microsoft.EntityFrameworkCore v10.0.3
     - Reference: https://github.com/dotnet/efcore
     - Usage: Object-Relational Mapping (ORM)

  2. SQL Server Provider (MIT License)
     - NuGet: Microsoft.EntityFrameworkCore.SqlServer v10.0.3
     - Reference: https://learn.microsoft.com/en-us/ef/core/providers/sql-server/
     - Usage: SQL Server database connectivity

  3. BCrypt.Net-Next (MIT License)
     - NuGet: BCrypt.Net-Next v4.0.3
     - Reference: https://github.com/BcryptNet/bcrypt.net
     - Usage: Password hashing and verification

  Architecture:
  - MVC Pattern: Model-View-Controller architecture
  - Authentication: Cookie-based authentication with role-based authorization
  - Database: SQL Server with Entity Framework Core

  Reference Documentation:
  - https://learn.microsoft.com/en-us/aspnet/core/
  - https://learn.microsoft.com/en-us/aspnet/core/security/authentication
  - https://learn.microsoft.com/en-us/ef/core/

  Author: EventEase Team
  Created: 2025
*/

using EventEase.Data;
using EventEase.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews(options =>
{
    options.ModelBindingMessageProvider.SetAttemptedValueIsInvalidAccessor(
        (value, fieldName) => "Please enter a valid value.");
    options.ModelBindingMessageProvider.SetMissingBindRequiredValueAccessor(
        fieldName => "This field is required.");
    options.ModelBindingMessageProvider.SetMissingKeyOrValueAccessor(
        () => "This field is required.");
    options.ModelBindingMessageProvider.SetValueMustNotBeNullAccessor(
        fieldName => "This field is required.");
});
builder.Services.AddDbContext<EventEaseContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionString")));
builder.Services.AddScoped<IBlobStorageService, BlobStorageService>();

// Add Authentication
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
    {
        options.LoginPath = "/Auth/Login";
        options.LogoutPath = "/Auth/Logout";
        options.AccessDeniedPath = "/Auth/AccessDenied";
        options.ExpireTimeSpan = TimeSpan.FromDays(7);
    });

// Add Authorization
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
    options.AddPolicy("BookingSpecialistOnly", policy => policy.RequireRole("BookingSpecialist"));
    options.AddPolicy("CustomerOnly", policy => policy.RequireRole("Customer"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Auth/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Auth}/{action=Login}/{id?}")
    .WithStaticAssets();


app.Run();
