using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace api.Data.Migrations
{
    /// <inheritdoc />
    public partial class CreateRoomAndBooking : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "51149c3d-e6a4-4997-bf34-0850c9bd83f5");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "84bf0340-5b79-4c87-89dd-73d80716ff95");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "991153d2-d24a-4a12-83eb-6d291bc93f0c");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "GalleryModel",
                newName: "Url");

            migrationBuilder.CreateTable(
                name: "RoomModel",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Capacity = table.Column<int>(type: "integer", nullable: false),
                    PricePerNight = table.Column<decimal>(type: "numeric", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    HotelId = table.Column<Guid>(type: "uuid", nullable: false),
                    Type = table.Column<string>(type: "text", nullable: true),
                    Status = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastUpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoomModel_Hotels_HotelId",
                        column: x => x.HotelId,
                        principalTable: "Hotels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BookingModel",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CheckInDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CheckOutDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    NumberOfGuests = table.Column<int>(type: "integer", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "numeric", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false),
                    RoomId = table.Column<Guid>(type: "uuid", nullable: false),
                    CustomerId = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastUpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookingModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BookingModel_AspNetUsers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookingModel_RoomModel_RoomId",
                        column: x => x.RoomId,
                        principalTable: "RoomModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "07f712e4-898c-407c-bc10-d5c154e28e60", null, "Admin", "ADMIN" },
                    { "87dc958a-e6a5-4960-a2d0-4831e98ab21b", null, "Owner", "OWNER" },
                    { "a8143ebf-cabc-48f6-a0f2-cfe38a02853f", null, "Customer", "CUSTOMER" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookingModel_CustomerId",
                table: "BookingModel",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_BookingModel_RoomId",
                table: "BookingModel",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_RoomModel_HotelId",
                table: "RoomModel",
                column: "HotelId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookingModel");

            migrationBuilder.DropTable(
                name: "RoomModel");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "07f712e4-898c-407c-bc10-d5c154e28e60");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "87dc958a-e6a5-4960-a2d0-4831e98ab21b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a8143ebf-cabc-48f6-a0f2-cfe38a02853f");

            migrationBuilder.RenameColumn(
                name: "Url",
                table: "GalleryModel",
                newName: "Name");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "51149c3d-e6a4-4997-bf34-0850c9bd83f5", null, "Customer", "CUSTOMER" },
                    { "84bf0340-5b79-4c87-89dd-73d80716ff95", null, "Owner", "OWNER" },
                    { "991153d2-d24a-4a12-83eb-6d291bc93f0c", null, "Admin", "ADMIN" }
                });
        }
    }
}
