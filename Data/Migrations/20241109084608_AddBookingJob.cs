using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace api.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddBookingJob : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "45d04850-d142-4f5e-8ef8-9fae69cb6dbd");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "dabcfc00-afda-4881-9c00-cf001cfe827d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f84caf27-287c-4e7a-9b01-08f4d0e370e1");

            migrationBuilder.CreateTable(
                name: "BookingJobs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    IdCheckInJob = table.Column<string>(type: "text", nullable: false),
                    IdCheckOutJob = table.Column<string>(type: "text", nullable: false),
                    BookingId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookingJobs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BookingJobs_Bookings_BookingId",
                        column: x => x.BookingId,
                        principalTable: "Bookings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "2e09d283-9699-4b29-a23f-4fc39d9c75fc", null, "Admin", "ADMIN" },
                    { "93fd9d9e-eff1-4708-a426-29cf22683313", null, "Customer", "CUSTOMER" },
                    { "9a0fb262-0208-4646-b424-1c72ed346729", null, "Owner", "OWNER" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_CheckInDate_CheckOutDate",
                table: "Bookings",
                columns: new[] { "CheckInDate", "CheckOutDate" });

            migrationBuilder.CreateIndex(
                name: "IX_BookingJobs_BookingId",
                table: "BookingJobs",
                column: "BookingId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookingJobs");

            migrationBuilder.DropIndex(
                name: "IX_Bookings_CheckInDate_CheckOutDate",
                table: "Bookings");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2e09d283-9699-4b29-a23f-4fc39d9c75fc");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "93fd9d9e-eff1-4708-a426-29cf22683313");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9a0fb262-0208-4646-b424-1c72ed346729");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "45d04850-d142-4f5e-8ef8-9fae69cb6dbd", null, "Admin", "ADMIN" },
                    { "dabcfc00-afda-4881-9c00-cf001cfe827d", null, "Owner", "OWNER" },
                    { "f84caf27-287c-4e7a-9b01-08f4d0e370e1", null, "Customer", "CUSTOMER" }
                });
        }
    }
}
