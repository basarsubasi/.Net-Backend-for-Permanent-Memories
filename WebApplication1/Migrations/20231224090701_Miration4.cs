using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class Miration4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "GUID",
                keyValue: new Guid("19af9082-074a-4588-a7c2-c1d3c09b6bf8"));

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "GUID", "AdditionalImages", "Brand", "Description", "Discriminator", "FilmColorState", "FilmExposure", "FilmISO", "FilmSize", "IsAvailable", "Price", "Quantity", "Title", "TitleImage" },
                values: new object[] { new Guid("bdb1b88e-5618-4f72-9749-8889f0503e58"), "[]", "Default Brand", "Kodak Portra 400 is a color negative film great for portraits, fashion and commercial shoots. This film is known for its beautiful skin tones and natural colors.", "Film", 0, 36, 400, 35, true, 10.99m, 400, "Kodak Portra 400", null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "GUID",
                keyValue: new Guid("bdb1b88e-5618-4f72-9749-8889f0503e58"));

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "GUID", "AdditionalImages", "Brand", "Description", "Discriminator", "FilmColorState", "FilmExposure", "FilmISO", "FilmSize", "IsAvailable", "Price", "Quantity", "Title", "TitleImage" },
                values: new object[] { new Guid("19af9082-074a-4588-a7c2-c1d3c09b6bf8"), "[]", "Default Brand", "Kodak Portra 400 is a color negative film great for portraits, fashion and commercial shoots. This film is known for its beautiful skin tones and natural colors.", "Film", 0, 36, 400, 35, true, 10.99m, 400, "Kodak Portra 400", null });
        }
    }
}
