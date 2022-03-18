using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class UOMInventario : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UnitOfMeasureName",
                table: "InventarioFisicoLines",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaCompletado",
                table: "InventarioFisico",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UnitOfMeasureName",
                table: "InventarioBodegaHabilitada",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UnitOfMeasureName",
                table: "InventarioFisicoLines");

            migrationBuilder.DropColumn(
                name: "FechaCompletado",
                table: "InventarioFisico");

            migrationBuilder.DropColumn(
                name: "UnitOfMeasureName",
                table: "InventarioBodegaHabilitada");
        }
    }
}
