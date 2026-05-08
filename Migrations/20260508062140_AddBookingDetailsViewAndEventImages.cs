using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventEase.Migrations
{
    /// <inheritdoc />
    public partial class AddBookingDetailsViewAndEventImages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Events",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.Sql(@"
CREATE OR ALTER VIEW [dbo].[BookingDetailsView] AS
SELECT
    b.[BookingId],
    b.[Status],
    b.[StartDateTime] AS [BookingStartDateTime],
    b.[EndDateTime] AS [BookingEndDateTime],
    v.[VenueId],
    v.[Name] AS [VenueName],
    v.[Location] AS [VenueLocation],
    v.[Capacity] AS [VenueCapacity],
    v.[ImageUrl] AS [VenueImageUrl],
    e.[EventId],
    e.[Name] AS [EventName],
    e.[StartDateTime] AS [EventStartDateTime],
    e.[EndDateTime] AS [EventEndDateTime],
    e.[Description] AS [EventDescription],
    e.[ImageUrl] AS [EventImageUrl],
    c.[CustomerId],
    c.[Name] AS [CustomerName],
    c.[Email] AS [CustomerEmail],
    u.[UserId],
    u.[Email] AS [BookingSpecialistEmail]
FROM [dbo].[Bookings] b
INNER JOIN [dbo].[Venues] v ON b.[VenueId] = v.[VenueId]
INNER JOIN [dbo].[Events] e ON b.[EventId] = e.[EventId]
INNER JOIN [dbo].[Customers] c ON b.[CustomerId] = c.[CustomerId]
INNER JOIN [dbo].[Users] u ON b.[UserId] = u.[UserId];
");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP VIEW IF EXISTS [dbo].[BookingDetailsView];");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Events");
        }
    }
}
