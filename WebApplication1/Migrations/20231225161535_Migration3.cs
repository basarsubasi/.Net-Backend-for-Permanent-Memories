using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class Migration3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "GUID",
                keyValue: new Guid("274a7041-3aaf-41f5-8da5-23d69235feb7"));

            migrationBuilder.DropColumn(
                name: "TitleImage",
                table: "Items");

            migrationBuilder.RenameColumn(
                name: "AdditionalImages",
                table: "Items",
                newName: "TitleImageUrl");

            migrationBuilder.AddColumn<string>(
                name: "AdditionalImageUrls",
                table: "Items",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "GUID", "AdditionalImageUrls", "Brand", "Description", "Discriminator", "FilmColorState", "FilmExposure", "FilmISO", "FilmSize", "IsAvailable", "ItemType", "Price", "Quantity", "Title", "TitleImageUrl" },
                values: new object[] { new Guid("0ad5f087-44ac-4489-bd34-5551db859bdb"), "[\"https://www.bhphotovideo.com/images/images2500x2500/kodak_6031678_portra_400_color_negative_35mm_1038169.jpg\",\"https://www.bhphotovideo.com/images/images2500x2500/kodak_6031678_portra_400_color_negative_35mm_1038169.jpg\",\"https://www.bhphotovideo.com/images/images2500x2500/kodak_6031678_portra_400_color_negative_35mm_1038169.jpg\"]", "Ilford", "Kodak Portra 400 is a color negative film great for portraits, fashion and commercial shoots. This film is known for its beautiful skin tones and natural colors.", "Film", 0, 36, 400, 35, true, 0, 10.99m, 400, "Kodak Portra 400", "https://www.bhphotovideo.com/images/images2500x2500/kodak_6031678_portra_400_color_negative_35mm_1038169.jpg" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "GUID",
                keyValue: new Guid("0ad5f087-44ac-4489-bd34-5551db859bdb"));

            migrationBuilder.DropColumn(
                name: "AdditionalImageUrls",
                table: "Items");

            migrationBuilder.RenameColumn(
                name: "TitleImageUrl",
                table: "Items",
                newName: "AdditionalImages");

            migrationBuilder.AddColumn<byte[]>(
                name: "TitleImage",
                table: "Items",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "GUID", "AdditionalImages", "Brand", "Description", "Discriminator", "FilmColorState", "FilmExposure", "FilmISO", "FilmSize", "IsAvailable", "ItemType", "Price", "Quantity", "Title", "TitleImage" },
                values: new object[] { new Guid("274a7041-3aaf-41f5-8da5-23d69235feb7"), "[]", "Ilford", "Kodak Portra 400 is a color negative film great for portraits, fashion and commercial shoots. This film is known for its beautiful skin tones and natural colors.", "Film", 0, 36, 400, 35, true, 0, 10.99m, 400, "Kodak Portra 400", null });
        }
    }
}
