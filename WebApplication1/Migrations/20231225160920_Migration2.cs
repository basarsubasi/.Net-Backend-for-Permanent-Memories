using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class Migration2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "GUID",
                keyValue: new Guid("df84a630-9511-4864-9ad2-01c8fd69b0ba"));

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "GUID", "AdditionalImages", "Brand", "Description", "Discriminator", "FilmColorState", "FilmExposure", "FilmISO", "FilmSize", "IsAvailable", "ItemType", "Price", "Quantity", "Title", "TitleImage" },
                values: new object[] { new Guid("274a7041-3aaf-41f5-8da5-23d69235feb7"), "[]", "Ilford", "Kodak Portra 400 is a color negative film great for portraits, fashion and commercial shoots. This film is known for its beautiful skin tones and natural colors.", "Film", 0, 36, 400, 35, true, 0, 10.99m, 400, "Kodak Portra 400", null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "GUID",
                keyValue: new Guid("274a7041-3aaf-41f5-8da5-23d69235feb7"));

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "GUID", "AdditionalImages", "Brand", "Description", "Discriminator", "FilmColorState", "FilmExposure", "FilmISO", "FilmSize", "IsAvailable", "ItemType", "Price", "Quantity", "Title", "TitleImage" },
                values: new object[] { new Guid("df84a630-9511-4864-9ad2-01c8fd69b0ba"), "[]", "Ilford", "Kodak Portra 400 is a color negative film great for portraits, fashion and commercial shoots. This film is known for its beautiful skin tones and natural colors.", "Film", 0, 36, 400, 35, true, 0, 10.99m, 400, "Kodak Portra 400", null });
        }
    }
}
