using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class MigrationNameFormain10 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "GUID",
                keyValue: new Guid("0301a315-e852-43a3-a2e2-77d8518dfb38"));

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "GUID",
                keyValue: new Guid("33a8bf00-5fea-4a49-9a39-d21c6ee5fb34"));

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "GUID", "AdditionalImageUrls", "Brand", "Description", "Discriminator", "FilmColorState", "FilmExposure", "FilmFormat", "FilmISO", "IsAvailable", "ItemBrandId", "ItemType", "Price", "Quantity", "Title", "TitleImageUrl" },
                values: new object[] { new Guid("0ac0d4b2-7fa4-49d8-9d68-954a36ea84ef"), "[\"https://www.bhphotovideo.com/images/images2500x2500/kodak_6031678_portra_400_color_negative_35mm_1038169.jpg\",\"https://www.bhphotovideo.com/images/images2500x2500/kodak_6031678_portra_400_color_negative_35mm_1038169.jpg\",\"https://www.bhphotovideo.com/images/images2500x2500/kodak_6031678_portra_400_color_negative_35mm_1038169.jpg\"]", "Kodak", "Kodak Portra 400 is a color negative film great for portraits, fashion and commercial shoots. This film is known for its beautiful skin tones and natural colors.", "Film", 0, 36, 35, 400, true, 2, 0, 10.99m, 400, "Kodak Portra 400", "https://www.bhphotovideo.com/images/images2500x2500/kodak_6031678_portra_400_color_negative_35mm_1038169.jpg" });

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "GUID", "AdditionalImageUrls", "Brand", "CameraFilmFormat", "CameraFocalLength", "CameraMaxShutterSpeed", "CameraMegapixel", "Description", "Discriminator", "IsAvailable", "ItemBrandId", "ItemType", "Price", "Quantity", "Title", "TitleImageUrl" },
                values: new object[] { new Guid("c882e459-7a91-4e44-853f-8ba4b9bf5b5a"), "[\"https://example.com/canon_eos_rebel_t7_1.jpg\",\"https://example.com/canon_eos_rebel_t7_2.jpg\"]", "Canon", 35, 50, 1000, 20, "A versatile and affordable entry-level DSLR camera.", "Camera", true, 0, 1, 499.99m, 100, "Canon EOS Rebel T7", "https://example.com/canon_eos_rebel_t7.jpg" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "GUID",
                keyValue: new Guid("0ac0d4b2-7fa4-49d8-9d68-954a36ea84ef"));

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "GUID",
                keyValue: new Guid("c882e459-7a91-4e44-853f-8ba4b9bf5b5a"));

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "GUID", "AdditionalImageUrls", "Brand", "Description", "Discriminator", "FilmColorState", "FilmExposure", "FilmFormat", "FilmISO", "IsAvailable", "ItemBrandId", "ItemType", "Price", "Quantity", "Title", "TitleImageUrl" },
                values: new object[] { new Guid("0301a315-e852-43a3-a2e2-77d8518dfb38"), "[\"https://www.bhphotovideo.com/images/images2500x2500/kodak_6031678_portra_400_color_negative_35mm_1038169.jpg\",\"https://www.bhphotovideo.com/images/images2500x2500/kodak_6031678_portra_400_color_negative_35mm_1038169.jpg\",\"https://www.bhphotovideo.com/images/images2500x2500/kodak_6031678_portra_400_color_negative_35mm_1038169.jpg\"]", "Kodak", "Kodak Portra 400 is a color negative film great for portraits, fashion and commercial shoots. This film is known for its beautiful skin tones and natural colors.", "Film", 0, 36, 35, 400, true, 2, 0, 10.99m, 400, "Kodak Portra 400", "https://www.bhphotovideo.com/images/images2500x2500/kodak_6031678_portra_400_color_negative_35mm_1038169.jpg" });

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "GUID", "AdditionalImageUrls", "Brand", "CameraFilmFormat", "CameraFocalLength", "CameraMaxShutterSpeed", "CameraMegapixel", "Description", "Discriminator", "IsAvailable", "ItemBrandId", "ItemType", "Price", "Quantity", "Title", "TitleImageUrl" },
                values: new object[] { new Guid("33a8bf00-5fea-4a49-9a39-d21c6ee5fb34"), "[\"https://example.com/canon_eos_rebel_t7_1.jpg\",\"https://example.com/canon_eos_rebel_t7_2.jpg\"]", "Canon", 35, 50, 1000, 20, "A versatile and affordable entry-level DSLR camera.", "Camera", true, 0, 1, 499.99m, 100, "Canon EOS Rebel T7", "https://example.com/canon_eos_rebel_t7.jpg" });
        }
    }
}
