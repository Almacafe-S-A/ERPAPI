using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class RemovedFieldsEndosos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BankId",
                table: "CertificadoDeposito");

            migrationBuilder.DropColumn(
                name: "BankName",
                table: "CertificadoDeposito");

            migrationBuilder.DropColumn(
                name: "FechaInicioComputo",
                table: "CertificadoDeposito");

            migrationBuilder.DropColumn(
                name: "FechaPagoBanco",
                table: "CertificadoDeposito");

            migrationBuilder.DropColumn(
                name: "LugarFirma",
                table: "CertificadoDeposito");

            migrationBuilder.DropColumn(
                name: "MontoGarantia",
                table: "CertificadoDeposito");

            migrationBuilder.DropColumn(
                name: "NombrePrestatario",
                table: "CertificadoDeposito");

            migrationBuilder.DropColumn(
                name: "PorcentajeInteresesInsolutos",
                table: "CertificadoDeposito");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "BankId",
                table: "CertificadoDeposito",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BankName",
                table: "CertificadoDeposito",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaInicioComputo",
                table: "CertificadoDeposito",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaPagoBanco",
                table: "CertificadoDeposito",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "LugarFirma",
                table: "CertificadoDeposito",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "MontoGarantia",
                table: "CertificadoDeposito",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NombrePrestatario",
                table: "CertificadoDeposito",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "PorcentajeInteresesInsolutos",
                table: "CertificadoDeposito",
                nullable: true);
        }
    }
}
