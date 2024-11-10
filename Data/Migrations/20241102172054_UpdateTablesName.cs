using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace api.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTablesName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookingModel_AspNetUsers_CustomerId",
                table: "BookingModel");

            migrationBuilder.DropForeignKey(
                name: "FK_BookingModel_RoomModel_RoomId",
                table: "BookingModel");

            migrationBuilder.DropForeignKey(
                name: "FK_GalleryModel_Hotels_HotelId",
                table: "GalleryModel");

            migrationBuilder.DropForeignKey(
                name: "FK_RoomModel_Hotels_HotelId",
                table: "RoomModel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RoomModel",
                table: "RoomModel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GalleryModel",
                table: "GalleryModel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BookingModel",
                table: "BookingModel");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "37b52a53-24e5-4874-8911-d4238b0e36f0");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "56c035a3-6bd3-4ab8-b281-cf9387b57e7c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bbcd38f5-79d6-48dc-bd45-eb6fe2a82b93");

            migrationBuilder.RenameTable(
                name: "RoomModel",
                newName: "Rooms");

            migrationBuilder.RenameTable(
                name: "GalleryModel",
                newName: "Gellarys");

            migrationBuilder.RenameTable(
                name: "BookingModel",
                newName: "Bookings");

            migrationBuilder.RenameIndex(
                name: "IX_RoomModel_HotelId",
                table: "Rooms",
                newName: "IX_Rooms_HotelId");

            migrationBuilder.RenameIndex(
                name: "IX_GalleryModel_HotelId",
                table: "Gellarys",
                newName: "IX_Gellarys_HotelId");

            migrationBuilder.RenameIndex(
                name: "IX_BookingModel_RoomId",
                table: "Bookings",
                newName: "IX_Bookings_RoomId");

            migrationBuilder.RenameIndex(
                name: "IX_BookingModel_CustomerId",
                table: "Bookings",
                newName: "IX_Bookings_CustomerId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastUpdatedAt",
                table: "Hotels",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Hotels",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastUpdatedAt",
                table: "Rooms",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Rooms",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastUpdatedAt",
                table: "Bookings",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Bookings",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CheckOutDate",
                table: "Bookings",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CheckInDate",
                table: "Bookings",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Rooms",
                table: "Rooms",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Gellarys",
                table: "Gellarys",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Bookings",
                table: "Bookings",
                column: "Id");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "45d04850-d142-4f5e-8ef8-9fae69cb6dbd", null, "Admin", "ADMIN" },
                    { "dabcfc00-afda-4881-9c00-cf001cfe827d", null, "Owner", "OWNER" },
                    { "f84caf27-287c-4e7a-9b01-08f4d0e370e1", null, "Customer", "CUSTOMER" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Locations_Country_City",
                table: "Locations",
                columns: new[] { "Country", "City" });

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_AspNetUsers_CustomerId",
                table: "Bookings",
                column: "CustomerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Rooms_RoomId",
                table: "Bookings",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Gellarys_Hotels_HotelId",
                table: "Gellarys",
                column: "HotelId",
                principalTable: "Hotels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_Hotels_HotelId",
                table: "Rooms",
                column: "HotelId",
                principalTable: "Hotels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_AspNetUsers_CustomerId",
                table: "Bookings");

            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Rooms_RoomId",
                table: "Bookings");

            migrationBuilder.DropForeignKey(
                name: "FK_Gellarys_Hotels_HotelId",
                table: "Gellarys");

            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_Hotels_HotelId",
                table: "Rooms");

            migrationBuilder.DropIndex(
                name: "IX_Locations_Country_City",
                table: "Locations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Rooms",
                table: "Rooms");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Gellarys",
                table: "Gellarys");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Bookings",
                table: "Bookings");

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

            migrationBuilder.RenameTable(
                name: "Rooms",
                newName: "RoomModel");

            migrationBuilder.RenameTable(
                name: "Gellarys",
                newName: "GalleryModel");

            migrationBuilder.RenameTable(
                name: "Bookings",
                newName: "BookingModel");

            migrationBuilder.RenameIndex(
                name: "IX_Rooms_HotelId",
                table: "RoomModel",
                newName: "IX_RoomModel_HotelId");

            migrationBuilder.RenameIndex(
                name: "IX_Gellarys_HotelId",
                table: "GalleryModel",
                newName: "IX_GalleryModel_HotelId");

            migrationBuilder.RenameIndex(
                name: "IX_Bookings_RoomId",
                table: "BookingModel",
                newName: "IX_BookingModel_RoomId");

            migrationBuilder.RenameIndex(
                name: "IX_Bookings_CustomerId",
                table: "BookingModel",
                newName: "IX_BookingModel_CustomerId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastUpdatedAt",
                table: "Hotels",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Hotels",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastUpdatedAt",
                table: "RoomModel",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "RoomModel",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastUpdatedAt",
                table: "BookingModel",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "BookingModel",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CheckOutDate",
                table: "BookingModel",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CheckInDate",
                table: "BookingModel",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RoomModel",
                table: "RoomModel",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GalleryModel",
                table: "GalleryModel",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BookingModel",
                table: "BookingModel",
                column: "Id");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "37b52a53-24e5-4874-8911-d4238b0e36f0", null, "Admin", "ADMIN" },
                    { "56c035a3-6bd3-4ab8-b281-cf9387b57e7c", null, "Customer", "CUSTOMER" },
                    { "bbcd38f5-79d6-48dc-bd45-eb6fe2a82b93", null, "Owner", "OWNER" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_BookingModel_AspNetUsers_CustomerId",
                table: "BookingModel",
                column: "CustomerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BookingModel_RoomModel_RoomId",
                table: "BookingModel",
                column: "RoomId",
                principalTable: "RoomModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GalleryModel_Hotels_HotelId",
                table: "GalleryModel",
                column: "HotelId",
                principalTable: "Hotels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RoomModel_Hotels_HotelId",
                table: "RoomModel",
                column: "HotelId",
                principalTable: "Hotels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
