using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace MyBrands.Migrations
{
    public partial class BranchMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.AlterColumn<byte[]>(
            //    name: "Timer",
            //    table: "Orders",
            //    type: "timestamp",
            //    maxLength: 8,
            //    nullable: true,
            //    oldClrType: typeof(byte[]),
            //    oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Branches",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    City = table.Column<string>(maxLength: 150, nullable: true),
                    Distance = table.Column<double>(nullable: true),
                    Latitude = table.Column<double>(nullable: true),
                    Longitude = table.Column<double>(nullable: true),
                    Region = table.Column<string>(maxLength: 2, nullable: true),
                    Street = table.Column<string>(maxLength: 150, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Branches", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Branches");

            migrationBuilder.AlterColumn<byte[]>(
                name: "Timer",
                table: "Orders",
                nullable: true,
                oldClrType: typeof(byte[]),
                oldType: "timestamp",
                oldMaxLength: 8,
                oldNullable: true);
        }
    }
}
