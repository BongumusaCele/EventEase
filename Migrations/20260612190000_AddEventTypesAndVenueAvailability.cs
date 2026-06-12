using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventEase.Migrations
{
    /// <inheritdoc />
    public partial class AddEventTypesAndVenueAvailability : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP VIEW IF EXISTS [dbo].[BookingDetailsView];");

            migrationBuilder.AddColumn<bool>(
                name: "IsAvailable",
                table: "Venues",
                type: "bit",
                nullable: false,
                defaultValue: true);

            migrationBuilder.CreateTable(
                name: "EventTypes",
                columns: table => new
                {
                    EventTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventTypes", x => x.EventTypeId);
                });

            migrationBuilder.Sql(@"
SET IDENTITY_INSERT [dbo].[EventTypes] ON;
INSERT INTO [dbo].[EventTypes] ([EventTypeId], [Name])
VALUES
    (1, N'General'),
    (2, N'Conference'),
    (3, N'Wedding'),
    (4, N'Concert'),
    (5, N'Workshop'),
    (6, N'Seminar'),
    (7, N'Exhibition'),
    (8, N'Corporate'),
    (9, N'Social');
SET IDENTITY_INSERT [dbo].[EventTypes] OFF;
");

            migrationBuilder.AddColumn<int>(
                name: "EventTypeId",
                table: "Events",
                type: "int",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.CreateIndex(
                name: "IX_Events_EventTypeId",
                table: "Events",
                column: "EventTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_EventTypes_EventTypeId",
                table: "Events",
                column: "EventTypeId",
                principalTable: "EventTypes",
                principalColumn: "EventTypeId",
                onDelete: ReferentialAction.Restrict);

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
    v.[IsAvailable] AS [VenueIsAvailable],
    v.[ImageUrl] AS [VenueImageUrl],
    e.[EventId],
    e.[Name] AS [EventName],
    e.[EventTypeId],
    et.[Name] AS [EventTypeName],
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
INNER JOIN [dbo].[EventTypes] et ON e.[EventTypeId] = et.[EventTypeId]
INNER JOIN [dbo].[Customers] c ON b.[CustomerId] = c.[CustomerId]
INNER JOIN [dbo].[Users] u ON b.[UserId] = u.[UserId];
");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP VIEW IF EXISTS [dbo].[BookingDetailsView];");

            migrationBuilder.DropForeignKey(
                name: "FK_Events_EventTypes_EventTypeId",
                table: "Events");

            migrationBuilder.DropIndex(
                name: "IX_Events_EventTypeId",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "EventTypeId",
                table: "Events");

            migrationBuilder.DropTable(
                name: "EventTypes");

            migrationBuilder.DropColumn(
                name: "IsAvailable",
                table: "Venues");

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
    }
}
