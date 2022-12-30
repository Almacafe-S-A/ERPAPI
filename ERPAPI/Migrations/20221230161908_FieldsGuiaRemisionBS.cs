using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class FieldsGuiaRemisionBS : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DNIMotorista",
                table: "GuiaRemision",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaDocuemto",
                table: "GuiaRemision",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaEntrada",
                table: "GuiaRemision",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaSalida",
                table: "GuiaRemision",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OrdenNo",
                table: "GuiaRemision",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PlacaContenedor",
                table: "GuiaRemision",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RTNTransportista",
                table: "GuiaRemision",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PlacaContenedor",
                table: "BoletaDeSalida",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BoletaDeSalida_CustomerId",
                table: "BoletaDeSalida",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_BoletaDeSalida_Customer_CustomerId",
                table: "BoletaDeSalida",
                column: "CustomerId",
                principalTable: "Customer",
                principalColumn: "CustomerId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BoletaDeSalida_Customer_CustomerId",
                table: "BoletaDeSalida");

            migrationBuilder.DropIndex(
                name: "IX_BoletaDeSalida_CustomerId",
                table: "BoletaDeSalida");

            migrationBuilder.DropColumn(
                name: "DNIMotorista",
                table: "GuiaRemision");

            migrationBuilder.DropColumn(
                name: "FechaDocuemto",
                table: "GuiaRemision");

            migrationBuilder.DropColumn(
                name: "FechaEntrada",
                table: "GuiaRemision");

            migrationBuilder.DropColumn(
                name: "FechaSalida",
                table: "GuiaRemision");

            migrationBuilder.DropColumn(
                name: "OrdenNo",
                table: "GuiaRemision");

            migrationBuilder.DropColumn(
                name: "PlacaContenedor",
                table: "GuiaRemision");

            migrationBuilder.DropColumn(
                name: "RTNTransportista",
                table: "GuiaRemision");

            migrationBuilder.DropColumn(
                name: "PlacaContenedor",
                table: "BoletaDeSalida");
        }
    }
}
