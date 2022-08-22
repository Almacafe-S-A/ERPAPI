using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class del_fieldsSolicitudCertificado : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BankId",
                table: "SolicitudCertificadoDeposito");

            migrationBuilder.DropColumn(
                name: "BankName",
                table: "SolicitudCertificadoDeposito");

            migrationBuilder.DropColumn(
                name: "CurrencyId",
                table: "SolicitudCertificadoDeposito");

            migrationBuilder.DropColumn(
                name: "CurrencyName",
                table: "SolicitudCertificadoDeposito");

            migrationBuilder.DropColumn(
                name: "FechaFirma",
                table: "SolicitudCertificadoDeposito");

            migrationBuilder.DropColumn(
                name: "FechaInicioComputo",
                table: "SolicitudCertificadoDeposito");

            migrationBuilder.DropColumn(
                name: "FechaPagoBanco",
                table: "SolicitudCertificadoDeposito");

            migrationBuilder.DropColumn(
                name: "Impreso",
                table: "SolicitudCertificadoDeposito");

            migrationBuilder.DropColumn(
                name: "LugarFirma",
                table: "SolicitudCertificadoDeposito");

            migrationBuilder.DropColumn(
                name: "MontoGarantia",
                table: "SolicitudCertificadoDeposito");

            migrationBuilder.DropColumn(
                name: "PorcentajeInteresesInsolutos",
                table: "SolicitudCertificadoDeposito");

            migrationBuilder.DropColumn(
                name: "WarehouseId",
                table: "SolicitudCertificadoDeposito");

            migrationBuilder.DropColumn(
                name: "WarehouseName",
                table: "SolicitudCertificadoDeposito");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "BankId",
                table: "SolicitudCertificadoDeposito",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BankName",
                table: "SolicitudCertificadoDeposito",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "CurrencyId",
                table: "SolicitudCertificadoDeposito",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "CurrencyName",
                table: "SolicitudCertificadoDeposito",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaFirma",
                table: "SolicitudCertificadoDeposito",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaInicioComputo",
                table: "SolicitudCertificadoDeposito",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaPagoBanco",
                table: "SolicitudCertificadoDeposito",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Impreso",
                table: "SolicitudCertificadoDeposito",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LugarFirma",
                table: "SolicitudCertificadoDeposito",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "MontoGarantia",
                table: "SolicitudCertificadoDeposito",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "PorcentajeInteresesInsolutos",
                table: "SolicitudCertificadoDeposito",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "WarehouseId",
                table: "SolicitudCertificadoDeposito",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "WarehouseName",
                table: "SolicitudCertificadoDeposito",
                nullable: true);
        }
    }
}
