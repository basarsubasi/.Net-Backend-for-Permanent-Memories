﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations.AppDb
{
    /// <inheritdoc />
    public partial class Main1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    GUID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ItemType = table.Column<int>(type: "int", nullable: false),
                    TitleImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AdditionalImageUrls = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Brand = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ItemBrandId = table.Column<int>(type: "int", nullable: false),
                    IsAvailable = table.Column<bool>(type: "bit", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    CameraFocalLength = table.Column<int>(type: "int", nullable: true),
                    CameraMaxShutterSpeed = table.Column<int>(type: "int", nullable: true),
                    CameraMegapixel = table.Column<int>(type: "int", nullable: true),
                    CameraFilmFormat = table.Column<int>(type: "int", nullable: true),
                    FilmColorState = table.Column<int>(type: "int", nullable: true),
                    FilmFormat = table.Column<int>(type: "int", nullable: true),
                    FilmISO = table.Column<int>(type: "int", nullable: true),
                    FilmExposure = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.GUID);
                });

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "GUID", "AdditionalImageUrls", "Brand", "Description", "Discriminator", "FilmColorState", "FilmExposure", "FilmFormat", "FilmISO", "IsAvailable", "ItemBrandId", "ItemType", "Price", "Quantity", "Title", "TitleImageUrl" },
                values: new object[] { new Guid("965e9710-410b-4885-93af-c7e4596c7809"), "[\"https://www.bhphotovideo.com/images/images2500x2500/kodak_6031678_portra_400_color_negative_35mm_1038169.jpg\",\"https://www.bhphotovideo.com/images/images2500x2500/kodak_6031678_portra_400_color_negative_35mm_1038169.jpg\",\"https://www.bhphotovideo.com/images/images2500x2500/kodak_6031678_portra_400_color_negative_35mm_1038169.jpg\"]", "Kodak", "Kodak Portra 400 is a color negative film great for portraits, fashion and commercial shoots. This film is known for its beautiful skin tones and natural colors.", "Film", 0, 36, 35, 400, true, 2, 0, 10.99m, 400, "Kodak Portra 400", "https://www.bhphotovideo.com/images/images2500x2500/kodak_6031678_portra_400_color_negative_35mm_1038169.jpg" });

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "GUID", "AdditionalImageUrls", "Brand", "CameraFilmFormat", "CameraFocalLength", "CameraMaxShutterSpeed", "CameraMegapixel", "Description", "Discriminator", "IsAvailable", "ItemBrandId", "ItemType", "Price", "Quantity", "Title", "TitleImageUrl" },
                values: new object[] { new Guid("ed626f2b-5d74-4e45-a2be-18e437ade323"), "[\"https://example.com/canon_eos_rebel_t7_1.jpg\",\"https://example.com/canon_eos_rebel_t7_2.jpg\"]", "Canon", 35, 50, 1000, 20, "A versatile and affordable entry-level DSLR camera.", "Camera", true, 0, 1, 499.99m, 100, "Canon EOS Rebel T7", "https://example.com/canon_eos_rebel_t7.jpg" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Items");
        }
    }
}
