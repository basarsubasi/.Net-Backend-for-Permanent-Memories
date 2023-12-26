using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class InitialIdentitySetup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "GUID",
                keyValue: new Guid("9b573b61-e54f-43fa-a90e-780e1fa578ac"));

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "GUID",
                keyValue: new Guid("b064a7f0-5261-46e0-9080-61feb190b233"));

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "GUID", "AdditionalImageUrls", "Brand", "Description", "Discriminator", "FilmColorState", "FilmExposure", "FilmFormat", "FilmISO", "IsAvailable", "ItemBrandId", "ItemType", "Price", "Quantity", "Title", "TitleImageUrl" },
                values: new object[] { new Guid("05c40b86-021c-4f3c-83da-9cb0fb5fd39b"), "[\"https://www.bhphotovideo.com/images/images2500x2500/kodak_6031678_portra_400_color_negative_35mm_1038169.jpg\",\"https://www.bhphotovideo.com/images/images2500x2500/kodak_6031678_portra_400_color_negative_35mm_1038169.jpg\",\"https://www.bhphotovideo.com/images/images2500x2500/kodak_6031678_portra_400_color_negative_35mm_1038169.jpg\"]", "Kodak", "Kodak Portra 400 is a color negative film great for portraits, fashion and commercial shoots. This film is known for its beautiful skin tones and natural colors.", "Film", 0, 36, 35, 400, true, 2, 0, 10.99m, 400, "Kodak Portra 400", "https://www.bhphotovideo.com/images/images2500x2500/kodak_6031678_portra_400_color_negative_35mm_1038169.jpg" });

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "GUID", "AdditionalImageUrls", "Brand", "CameraFilmFormat", "CameraFocalLength", "CameraMaxShutterSpeed", "CameraMegapixel", "Description", "Discriminator", "IsAvailable", "ItemBrandId", "ItemType", "Price", "Quantity", "Title", "TitleImageUrl" },
                values: new object[] { new Guid("81ca3ae3-c2fb-435a-ad6d-63f8fbaa6496"), "[\"https://example.com/canon_eos_rebel_t7_1.jpg\",\"https://example.com/canon_eos_rebel_t7_2.jpg\"]", "Canon", 35, 50, 1000, 20, "A versatile and affordable entry-level DSLR camera.", "Camera", true, 0, 1, 499.99m, 100, "Canon EOS Rebel T7", "https://example.com/canon_eos_rebel_t7.jpg" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "GUID",
                keyValue: new Guid("05c40b86-021c-4f3c-83da-9cb0fb5fd39b"));

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "GUID",
                keyValue: new Guid("81ca3ae3-c2fb-435a-ad6d-63f8fbaa6496"));

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "GUID", "AdditionalImageUrls", "Brand", "Description", "Discriminator", "FilmColorState", "FilmExposure", "FilmFormat", "FilmISO", "IsAvailable", "ItemBrandId", "ItemType", "Price", "Quantity", "Title", "TitleImageUrl" },
                values: new object[] { new Guid("9b573b61-e54f-43fa-a90e-780e1fa578ac"), "[\"https://www.bhphotovideo.com/images/images2500x2500/kodak_6031678_portra_400_color_negative_35mm_1038169.jpg\",\"https://www.bhphotovideo.com/images/images2500x2500/kodak_6031678_portra_400_color_negative_35mm_1038169.jpg\",\"https://www.bhphotovideo.com/images/images2500x2500/kodak_6031678_portra_400_color_negative_35mm_1038169.jpg\"]", "Kodak", "Kodak Portra 400 is a color negative film great for portraits, fashion and commercial shoots. This film is known for its beautiful skin tones and natural colors.", "Film", 0, 36, 35, 400, true, 2, 0, 10.99m, 400, "Kodak Portra 400", "https://www.bhphotovideo.com/images/images2500x2500/kodak_6031678_portra_400_color_negative_35mm_1038169.jpg" });

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "GUID", "AdditionalImageUrls", "Brand", "CameraFilmFormat", "CameraFocalLength", "CameraMaxShutterSpeed", "CameraMegapixel", "Description", "Discriminator", "IsAvailable", "ItemBrandId", "ItemType", "Price", "Quantity", "Title", "TitleImageUrl" },
                values: new object[] { new Guid("b064a7f0-5261-46e0-9080-61feb190b233"), "[\"https://example.com/canon_eos_rebel_t7_1.jpg\",\"https://example.com/canon_eos_rebel_t7_2.jpg\"]", "Canon", 35, 50, 1000, 20, "A versatile and affordable entry-level DSLR camera.", "Camera", true, 0, 1, 499.99m, 100, "Canon EOS Rebel T7", "https://example.com/canon_eos_rebel_t7.jpg" });
        }
    }
}
