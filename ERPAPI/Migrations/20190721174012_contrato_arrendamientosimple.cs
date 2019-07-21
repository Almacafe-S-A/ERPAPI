using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class contrato_arrendamientosimple : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CustomerManagerProfesionNacionalidad",
                table: "CustomerContract",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "DelegateSalary",
                table: "CustomerContract",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaContrato",
                table: "CustomerContract",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Mercancias",
                table: "CustomerContract",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "MontaCargas",
                table: "CustomerContract",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "MulasHidraulicas",
                table: "CustomerContract",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Papeleria",
                table: "CustomerContract",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "Resolution",
                table: "CustomerContract",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WarehouseRequirements",
                table: "CustomerContract",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CustomerManagerProfesionNacionalidad",
                table: "CustomerContract");

            migrationBuilder.DropColumn(
                name: "DelegateSalary",
                table: "CustomerContract");

            migrationBuilder.DropColumn(
                name: "FechaContrato",
                table: "CustomerContract");

            migrationBuilder.DropColumn(
                name: "Mercancias",
                table: "CustomerContract");

            migrationBuilder.DropColumn(
                name: "MontaCargas",
                table: "CustomerContract");

            migrationBuilder.DropColumn(
                name: "MulasHidraulicas",
                table: "CustomerContract");

            migrationBuilder.DropColumn(
                name: "Papeleria",
                table: "CustomerContract");

            migrationBuilder.DropColumn(
                name: "Resolution",
                table: "CustomerContract");

            migrationBuilder.DropColumn(
                name: "WarehouseRequirements",
                table: "CustomerContract");
        }
    }
}
