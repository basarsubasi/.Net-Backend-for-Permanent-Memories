using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class Miration5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "GUID",
                keyValue: new Guid("bdb1b88e-5618-4f72-9749-8889f0503e58"));

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "GUID", "AdditionalImages", "Brand", "Description", "Discriminator", "FilmColorState", "FilmExposure", "FilmISO", "FilmSize", "IsAvailable", "Price", "Quantity", "Title", "TitleImage" },
                values: new object[] { new Guid("8b22c553-d2ff-4b56-8bac-9b4a3b92de49"), "[]", "Ilford", "Kodak Portra 400 is a color negative film great for portraits, fashion and commercial shoots. This film is known for its beautiful skin tones and natural colors.", "Film", 0, 36, 400, 35, true, 10.99m, 400, "Kodak Portra 400", null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "GUID",
                keyValue: new Guid("8b22c553-d2ff-4b56-8bac-9b4a3b92de49"));

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "GUID", "AdditionalImages", "Brand", "Description", "Discriminator", "FilmColorState", "FilmExposure", "FilmISO", "FilmSize", "IsAvailable", "Price", "Quantity", "Title", "TitleImage" },
                values: new object[] { new Guid("bdb1b88e-5618-4f72-9749-8889f0503e58"), "[]", "Default Brand", "Kodak Portra 400 is a color negative film great for portraits, fashion and commercial shoots. This film is known for its beautiful skin tones and natural colors.", "Film", 0, 36, 400, 35, true, 10.99m, 400, "Kodak Portra 400", null });
        }
    }
}
