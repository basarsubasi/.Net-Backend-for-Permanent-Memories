using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class Miration3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "GUID",
                keyValue: new Guid("e05df6da-4fcc-44f0-bd95-20711bf6bea8"));

            migrationBuilder.DropColumn(
                name: "FilmBrand",
                table: "Items");

            migrationBuilder.AddColumn<string>(
                name: "Brand",
                table: "Items",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "GUID", "AdditionalImages", "Brand", "Description", "Discriminator", "FilmColorState", "FilmExposure", "FilmISO", "FilmSize", "IsAvailable", "Price", "Quantity", "Title", "TitleImage" },
                values: new object[] { new Guid("19af9082-074a-4588-a7c2-c1d3c09b6bf8"), "[]", "Default Brand", "Kodak Portra 400 is a color negative film great for portraits, fashion and commercial shoots. This film is known for its beautiful skin tones and natural colors.", "Film", 0, 36, 400, 35, true, 10.99m, 400, "Kodak Portra 400", null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "GUID",
                keyValue: new Guid("19af9082-074a-4588-a7c2-c1d3c09b6bf8"));

            migrationBuilder.DropColumn(
                name: "Brand",
                table: "Items");

            migrationBuilder.AddColumn<int>(
                name: "FilmBrand",
                table: "Items",
                type: "int",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "GUID", "AdditionalImages", "Description", "Discriminator", "FilmBrand", "FilmColorState", "FilmExposure", "FilmISO", "FilmSize", "IsAvailable", "Price", "Quantity", "Title", "TitleImage" },
                values: new object[] { new Guid("e05df6da-4fcc-44f0-bd95-20711bf6bea8"), "[]", "Kodak Portra 400 is a color negative film great for portraits, fashion and commercial shoots. This film is known for its beautiful skin tones and natural colors.", "Film", 0, 0, 36, 400, 35, true, 10.99m, 400, "Kodak Portra 400", null });
        }
    }
}
