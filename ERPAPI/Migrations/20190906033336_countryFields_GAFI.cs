using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class countryFields_GAFI : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AccionId",
                table: "Country",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "AccionName",
                table: "Country",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Actualizacion",
                table: "Country",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "ListaId",
                table: "Country",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ListaName",
                table: "Country",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NivelRiesgo",
                table: "Country",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "NivelRiesgoName",
                table: "Country",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SeguimientoId",
                table: "Country",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "SeguimientoName",
                table: "Country",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TipoAlertaId",
                table: "Country",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "TipoAlertaName",
                table: "Country",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "AccountBalance",
                table: "Account",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccionId",
                table: "Country");

            migrationBuilder.DropColumn(
                name: "AccionName",
                table: "Country");

            migrationBuilder.DropColumn(
                name: "Actualizacion",
                table: "Country");

            migrationBuilder.DropColumn(
                name: "ListaId",
                table: "Country");

            migrationBuilder.DropColumn(
                name: "ListaName",
                table: "Country");

            migrationBuilder.DropColumn(
                name: "NivelRiesgo",
                table: "Country");

            migrationBuilder.DropColumn(
                name: "NivelRiesgoName",
                table: "Country");

            migrationBuilder.DropColumn(
                name: "SeguimientoId",
                table: "Country");

            migrationBuilder.DropColumn(
                name: "SeguimientoName",
                table: "Country");

            migrationBuilder.DropColumn(
                name: "TipoAlertaId",
                table: "Country");

            migrationBuilder.DropColumn(
                name: "TipoAlertaName",
                table: "Country");

            migrationBuilder.DropColumn(
                name: "AccountBalance",
                table: "Account");
        }
    }
}
