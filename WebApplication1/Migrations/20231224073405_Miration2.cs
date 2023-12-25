using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class Miration2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Films");

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "GUID",
                keyValue: new Guid("ae222776-0367-4598-90a5-73ce0a07be03"));

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Items",
                type: "nvarchar(5)",
                maxLength: 5,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "FilmBrand",
                table: "Items",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FilmColorState",
                table: "Items",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FilmExposure",
                table: "Items",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FilmISO",
                table: "Items",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FilmSize",
                table: "Items",
                type: "int",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "GUID", "AdditionalImages", "Description", "Discriminator", "FilmBrand", "FilmColorState", "FilmExposure", "FilmISO", "FilmSize", "IsAvailable", "Price", "Quantity", "Title", "TitleImage" },
                values: new object[] { new Guid("e05df6da-4fcc-44f0-bd95-20711bf6bea8"), "[]", "Kodak Portra 400 is a color negative film great for portraits, fashion and commercial shoots. This film is known for its beautiful skin tones and natural colors.", "Film", 0, 0, 36, 400, 35, true, 10.99m, 400, "Kodak Portra 400", null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "GUID",
                keyValue: new Guid("e05df6da-4fcc-44f0-bd95-20711bf6bea8"));

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "FilmBrand",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "FilmColorState",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "FilmExposure",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "FilmISO",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "FilmSize",
                table: "Items");

            migrationBuilder.CreateTable(
                name: "Films",
                columns: table => new
                {
                    GUID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FilmBrand = table.Column<int>(type: "int", nullable: false),
                    FilmColorState = table.Column<int>(type: "int", nullable: false),
                    FilmExposure = table.Column<int>(type: "int", nullable: false),
                    FilmISO = table.Column<int>(type: "int", nullable: false),
                    FilmSize = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Films", x => x.GUID);
                    table.ForeignKey(
                        name: "FK_Films_Items_GUID",
                        column: x => x.GUID,
                        principalTable: "Items",
                        principalColumn: "GUID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "GUID", "AdditionalImages", "Description", "IsAvailable", "Price", "Quantity", "Title", "TitleImage" },
                values: new object[] { new Guid("ae222776-0367-4598-90a5-73ce0a07be03"), "[]", "Kodak Portra 400 is a color negative film great for portraits, fashion and commercial shoots. This film is known for its beautiful skin tones and natural colors.", true, 10.99m, 400, "Kodak Portra 400", null });

            migrationBuilder.InsertData(
                table: "Films",
                columns: new[] { "GUID", "FilmBrand", "FilmColorState", "FilmExposure", "FilmISO", "FilmSize" },
                values: new object[] { new Guid("ae222776-0367-4598-90a5-73ce0a07be03"), 0, 0, 36, 400, 35 });
        }
    }
}
