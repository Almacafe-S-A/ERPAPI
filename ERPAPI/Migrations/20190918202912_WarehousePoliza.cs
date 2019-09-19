using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class WarehousePoliza : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "CantidadPoliza",
                table: "Warehouse",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<long>(
                name: "CurrencyId",
                table: "Warehouse",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "CurrencyName",
                table: "Warehouse",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaEmisionPoliza",
                table: "Warehouse",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaVencimientoPoliza",
                table: "Warehouse",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "NoPoliza",
                table: "Warehouse",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CantidadPoliza",
                table: "Warehouse");

            migrationBuilder.DropColumn(
                name: "CurrencyId",
                table: "Warehouse");

            migrationBuilder.DropColumn(
                name: "CurrencyName",
                table: "Warehouse");

            migrationBuilder.DropColumn(
                name: "FechaEmisionPoliza",
                table: "Warehouse");

            migrationBuilder.DropColumn(
                name: "FechaVencimientoPoliza",
                table: "Warehouse");

            migrationBuilder.DropColumn(
                name: "NoPoliza",
                table: "Warehouse");
        }
    }
}
