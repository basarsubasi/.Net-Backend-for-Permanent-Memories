using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class Migration32 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "GUID",
                keyValue: new Guid("ce1a891f-32b0-4072-a750-b5b357dbba2d"));

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "GUID", "AdditionalImageUrls", "Brand", "Description", "Discriminator", "FilmColorState", "FilmExposure", "FilmFormat", "FilmISO", "IsAvailable", "ItemType", "Price", "Quantity", "Title", "TitleImageUrl" },
                values: new object[] { new Guid("0eb400ca-3983-4eca-a1ad-71b67432746a"), "[\"https://www.bhphotovideo.com/images/images2500x2500/kodak_6031678_portra_400_color_negative_35mm_1038169.jpg\",\"https://www.bhphotovideo.com/images/images2500x2500/kodak_6031678_portra_400_color_negative_35mm_1038169.jpg\",\"https://www.bhphotovideo.com/images/images2500x2500/kodak_6031678_portra_400_color_negative_35mm_1038169.jpg\"]", "Ilford", "Kodak Portra 400 is a color negative film great for portraits, fashion and commercial shoots. This film is known for its beautiful skin tones and natural colors.", "Film", 0, 36, 35, 400, true, 0, 10.99m, 400, "Kodak Portra 400", "https://www.bhphotovideo.com/images/images2500x2500/kodak_6031678_portra_400_color_negative_35mm_1038169.jpg" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "GUID",
                keyValue: new Guid("0eb400ca-3983-4eca-a1ad-71b67432746a"));

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "GUID", "AdditionalImageUrls", "Brand", "Description", "Discriminator", "FilmColorState", "FilmExposure", "FilmFormat", "FilmISO", "IsAvailable", "ItemType", "Price", "Quantity", "Title", "TitleImageUrl" },
                values: new object[] { new Guid("ce1a891f-32b0-4072-a750-b5b357dbba2d"), "[\"https://www.bhphotovideo.com/images/images2500x2500/kodak_6031678_portra_400_color_negative_35mm_1038169.jpg\",\"https://www.bhphotovideo.com/images/images2500x2500/kodak_6031678_portra_400_color_negative_35mm_1038169.jpg\",\"https://www.bhphotovideo.com/images/images2500x2500/kodak_6031678_portra_400_color_negative_35mm_1038169.jpg\"]", "Ilford", "Kodak Portra 400 is a color negative film great for portraits, fashion and commercial shoots. This film is known for its beautiful skin tones and natural colors.", "Film", 0, 36, 35, 400, true, 0, 10.99m, 400, "Kodak Portra 400", "https://www.bhphotovideo.com/images/images2500x2500/kodak_6031678_portra_400_color_negative_35mm_1038169.jpg" });
        }
    }
}
