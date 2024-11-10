using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace api.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateHotel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.AlterColumn<Guid>(
                name: "HotelId",
                table: "GalleryModel",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "37b52a53-24e5-4874-8911-d4238b0e36f0", null, "Admin", "ADMIN" },
                    { "56c035a3-6bd3-4ab8-b281-cf9387b57e7c", null, "Customer", "CUSTOMER" },
                    { "bbcd38f5-79d6-48dc-bd45-eb6fe2a82b93", null, "Owner", "OWNER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.AlterColumn<Guid>(
                name: "HotelId",
                table: "GalleryModel",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "07f712e4-898c-407c-bc10-d5c154e28e60", null, "Admin", "ADMIN" },
                    { "87dc958a-e6a5-4960-a2d0-4831e98ab21b", null, "Owner", "OWNER" },
                    { "a8143ebf-cabc-48f6-a0f2-cfe38a02853f", null, "Customer", "CUSTOMER" }
                });
        }
    }
}
