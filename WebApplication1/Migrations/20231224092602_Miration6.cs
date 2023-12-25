using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class Miration6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "GUID",
                keyValue: new Guid("8b22c553-d2ff-4b56-8bac-9b4a3b92de49"));

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "GUID", "AdditionalImages", "Brand", "Description", "Discriminator", "FilmColorState", "FilmExposure", "FilmISO", "FilmSize", "IsAvailable", "Price", "Quantity", "Title", "TitleImage" },
                values: new object[] { new Guid("453d6324-7233-4411-966b-62fa969ef0d2"), "[]", "Ilford", "Kodak Portra 400 is a color negative film great for portraits, fashion and commercial shoots. This film is known for its beautiful skin tones and natural colors.", "Film", 0, 36, 400, 35, true, 10.99m, 400, "Kodak Portra 400", null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "GUID",
                keyValue: new Guid("453d6324-7233-4411-966b-62fa969ef0d2"));

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "GUID", "AdditionalImages", "Brand", "Description", "Discriminator", "FilmColorState", "FilmExposure", "FilmISO", "FilmSize", "IsAvailable", "Price", "Quantity", "Title", "TitleImage" },
                values: new object[] { new Guid("8b22c553-d2ff-4b56-8bac-9b4a3b92de49"), "[]", "Ilford", "Kodak Portra 400 is a color negative film great for portraits, fashion and commercial shoots. This film is known for its beautiful skin tones and natural colors.", "Film", 0, 36, 400, 35, true, 10.99m, 400, "Kodak Portra 400", null });
        }
    }
}
