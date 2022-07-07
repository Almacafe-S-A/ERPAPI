using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class BoletadeSalidaFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DNIMotorista",
                table: "BoletaDeSalida",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaIngreso",
                table: "BoletaDeSalida",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaSalida",
                table: "BoletaDeSalida",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RTNTransportista",
                table: "BoletaDeSalida",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DNIMotorista",
                table: "BoletaDeSalida");

            migrationBuilder.DropColumn(
                name: "FechaIngreso",
                table: "BoletaDeSalida");

            migrationBuilder.DropColumn(
                name: "FechaSalida",
                table: "BoletaDeSalida");

            migrationBuilder.DropColumn(
                name: "RTNTransportista",
                table: "BoletaDeSalida");
        }
    }
}
