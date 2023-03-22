using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class CustomerFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "AFND",
                table: "Customer",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaBaja",
                table: "Customer",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "PEP",
                table: "Customer",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AFND",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "FechaBaja",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "PEP",
                table: "Customer");
        }
    }
}
