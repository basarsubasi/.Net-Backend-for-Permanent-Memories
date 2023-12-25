using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class Miration1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    GUID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TitleImage = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    AdditionalImages = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsAvailable = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.GUID);
                });

            migrationBuilder.CreateTable(
                name: "Films",
                columns: table => new
                {
                    GUID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FilmColorState = table.Column<int>(type: "int", nullable: false),
                    FilmSize = table.Column<int>(type: "int", nullable: false),
                    FilmBrand = table.Column<int>(type: "int", nullable: false),
                    FilmISO = table.Column<int>(type: "int", nullable: false),
                    FilmExposure = table.Column<int>(type: "int", nullable: false)
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Films");

            migrationBuilder.DropTable(
                name: "Items");
        }
    }
}
