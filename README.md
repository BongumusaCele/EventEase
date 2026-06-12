# EventEase

EventEase is an ASP.NET Core MVC event management web app for managing venues, events, customers, users, and bookings. It includes role-based login, Azure SQL storage, Azure Blob image uploads, booking validation, enriched booking search, and a South Africa-limited venue address search helper.

## Features

- User authentication with cookie-based login.
- Role-based access for Admin, Booking Specialist, and Customer users.
- Venue, event, customer, user, and booking management.
- Event type classification with predefined lookup categories.
- Advanced booking filters by event type, booking date range, and venue availability.
- Venue availability tracking for booking/search workflows.
- Azure Blob Storage image uploads for venue and event images.
- Friendly validation messages across forms.
- Double-booking prevention for venues.
- Delete protection for records linked to active bookings.
- Enhanced booking list with joined venue, event, customer, and booking details.
- Search/filter support on listing pages.
- Venue location search suggestions limited to South Africa.

## Tools and Technologies

- .NET 10
- ASP.NET Core MVC
- Entity Framework Core 10.0.3
- Microsoft SQL Server / Azure SQL Database
- Azure Storage Blobs
- Azure App Service
- BCrypt.Net-Next for password hashing
- Razor Views
- HTML, CSS, JavaScript
- Font Awesome icons
- Bootstrap, jQuery, and jQuery Validation client libraries
- OpenStreetMap Nominatim API for venue address suggestions

## Requirements

- .NET 10 SDK
- SQL Server or Azure SQL Database
- Azure Storage Account with a Blob container
- Visual Studio 2026, Visual Studio Code, or another .NET editor
- Azure CLI or Azure Portal access for deployment configuration

## Configuration

The app needs a database connection string and Azure Storage settings.

For local development, use `appsettings.Development.json`, user secrets, or environment variables.

Required settings:

```text
ConnectionStrings__DefaultConnectionString
AzureStorage__ConnectionString
AzureStorage__ContainerName
```

Example local environment variables:

```powershell
$env:ConnectionStrings__DefaultConnectionString="Server=tcp:<server>.database.windows.net,1433;Initial Catalog=EventEase;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;Authentication=Active Directory Default;"
$env:AzureStorage__ConnectionString="DefaultEndpointsProtocol=https;AccountName=<storage-account>;AccountKey=<key>;EndpointSuffix=core.windows.net"
$env:AzureStorage__ContainerName="eventease-images"
```

For Azure App Service, add these under:

```text
App Service > Settings > Environment variables > App settings
```

Use these names exactly:

```text
AzureStorage__ConnectionString
AzureStorage__ContainerName
```

The database connection can be added under App Service connection strings as:

```text
DefaultConnectionString
```

## Database Setup

Apply Entity Framework migrations before running the app:

```powershell
dotnet ef database update
```

If `dotnet ef` is not installed:

```powershell
dotnet tool install --global dotnet-ef
```

The app uses the connection named:

```text
DefaultConnectionString
```

## Run Locally

Restore packages:

```powershell
dotnet restore
```

Build the project:

```powershell
dotnet build
```

Run the app:

```powershell
dotnet run
```

Then open the local URL shown in the terminal, usually:

```text
https://localhost:<port>
```

The default route opens the login page:

```text
/Auth/Login
```

## Azure Storage Notes

Venue and event images are uploaded to Azure Blob Storage.

The storage account must allow the app to create/use the configured container. The app stores image URLs in the database after upload.

If image uploads fail, check:

- `AzureStorage__ConnectionString` is set correctly.
- `AzureStorage__ContainerName` is set.
- The storage account key is valid.
- Blob public access is configured according to how images should be displayed.
- The app was restarted after changing environment variables.

## Address Search

Venue create/edit forms include address suggestions for South African locations. This uses the public OpenStreetMap Nominatim search endpoint and writes the selected address into the existing `Venue.Location` field.

If the lookup service is unavailable, users can still type the address manually.

## Deployment

1. Publish the app from Visual Studio or with `dotnet publish`.
2. Deploy to Azure App Service.
3. Configure App Service environment variables:
   - `AzureStorage__ConnectionString`
   - `AzureStorage__ContainerName`
4. Configure the database connection string:
   - `DefaultConnectionString`
5. Run EF migrations against the Azure SQL database.
6. Restart the App Service.
7. Test login, venue image upload, event image upload, booking creation, search, and delete validation.

## Useful Commands

```powershell
dotnet restore
dotnet build
dotnet run
dotnet ef database update
```

## Project Structure

```text
Controllers/    MVC controllers
Data/           Entity Framework database context
Migrations/     EF Core database migrations
Models/         Domain and view models
Services/       Azure Blob Storage service
Views/          Razor MVC views
wwwroot/        Static CSS, JavaScript, images, and client libraries
Program.cs      Application startup and service configuration
```

## Notes

- Do not commit real Azure Storage keys or production connection strings.
- Restart Azure App Service after changing environment variables.
- The generated `bin/`, `obj/`, `.vs/`, `.dotnet/`, and `.nuget/` folders are not required in source control.
