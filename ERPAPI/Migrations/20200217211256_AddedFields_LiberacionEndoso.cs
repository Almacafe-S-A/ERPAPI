using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class AddedFields_LiberacionEndoso : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "SubProductId",
                table: "EndososLiberacion",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "SubProductName",
                table: "EndososLiberacion",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UnitOfMeasureId",
                table: "EndososLiberacion",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "UnitOfMeasureName",
                table: "EndososLiberacion",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Saldo",
                table: "EndososCertificadosLine",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "CantidadEndosada",
                table: "EndososCertificados",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaCancelacion",
                table: "EndososCertificados",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaLiberacion",
                table: "EndososCertificados",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<double>(
                name: "Saldo",
                table: "EndososCertificados",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SubProductId",
                table: "EndososLiberacion");

            migrationBuilder.DropColumn(
                name: "SubProductName",
                table: "EndososLiberacion");

            migrationBuilder.DropColumn(
                name: "UnitOfMeasureId",
                table: "EndososLiberacion");

            migrationBuilder.DropColumn(
                name: "UnitOfMeasureName",
                table: "EndososLiberacion");

            migrationBuilder.DropColumn(
                name: "Saldo",
                table: "EndososCertificadosLine");

            migrationBuilder.DropColumn(
                name: "CantidadEndosada",
                table: "EndososCertificados");

            migrationBuilder.DropColumn(
                name: "FechaCancelacion",
                table: "EndososCertificados");

            migrationBuilder.DropColumn(
                name: "FechaLiberacion",
                table: "EndososCertificados");

            migrationBuilder.DropColumn(
                name: "Saldo",
                table: "EndososCertificados");
        }
    }
}
