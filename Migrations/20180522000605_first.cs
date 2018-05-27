using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace MyBrands.Migrations
{
    public partial class first : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MenuItems",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 200, nullable: true),
                    Timer = table.Column<byte[]>(type: "timestamp", maxLength: 8, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuItems", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 50, nullable: false),
                    BrandId = table.Column<int>(nullable: false),
                    CostPrice = table.Column<decimal>(type: "money", nullable: false),
                    Description = table.Column<string>(maxLength: 2000, nullable: false),
                    GraphicName = table.Column<string>(maxLength: 50, nullable: false),
                    MSRP = table.Column<decimal>(type: "money", nullable: false),
                    ProductName = table.Column<string>(maxLength: 50, nullable: false),
                    QtyOnBackOrder = table.Column<int>(nullable: false),
                    QtyOnHand = table.Column<int>(nullable: false),
                    Time = table.Column<byte[]>(type: "timestamp", maxLength: 8, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Categories_MenuItems_BrandId",
                        column: x => x.BrandId,
                        principalTable: "MenuItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Categories_BrandId",
                table: "Categories",
                column: "BrandId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "MenuItems");
        }
    }
}
