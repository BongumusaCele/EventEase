using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventEase.Migrations
{
    /// <inheritdoc />
    public partial class onetomanyfix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: 2);

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "EventId", "Description", "EndDateTime", "Name", "StartDateTime", "VenueId" },
                values: new object[] { 3, "Spring Bash October", new DateTime(2026, 2, 26, 14, 30, 0, 0, DateTimeKind.Unspecified), "Spring Bash", new DateTime(2026, 2, 25, 14, 30, 0, 0, DateTimeKind.Unspecified), 1 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: 3);

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "EventId", "Description", "EndDateTime", "Name", "StartDateTime", "VenueId" },
                values: new object[] { 2, "Spring Bash October", new DateTime(2026, 2, 26, 14, 30, 0, 0, DateTimeKind.Unspecified), "Spring Bash", new DateTime(2026, 2, 25, 14, 30, 0, 0, DateTimeKind.Unspecified), 1 });
        }
    }
}
