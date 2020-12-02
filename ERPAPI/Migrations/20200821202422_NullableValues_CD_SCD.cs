using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class NullableValues_CD_SCD : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FechaFirma",
                table: "CertificadoDeposito");

            migrationBuilder.DropColumn(
                name: "FechaVencimiento",
                table: "CertificadoDeposito");

            migrationBuilder.AlterColumn<double>(
                name: "PorcentajeInteresesInsolutos",
                table: "SolicitudCertificadoDeposito",
                nullable: true,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<long>(
                name: "NoTraslado",
                table: "SolicitudCertificadoDeposito",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AlterColumn<double>(
                name: "MontoGarantia",
                table: "SolicitudCertificadoDeposito",
                nullable: true,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaVencimientoDeposito",
                table: "SolicitudCertificadoDeposito",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaPagoBanco",
                table: "SolicitudCertificadoDeposito",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaInicioComputo",
                table: "SolicitudCertificadoDeposito",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaFirma",
                table: "SolicitudCertificadoDeposito",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<long>(
                name: "BankId",
                table: "SolicitudCertificadoDeposito",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AlterColumn<double>(
                name: "PorcentajeInteresesInsolutos",
                table: "CertificadoDeposito",
                nullable: true,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<long>(
                name: "NoTraslado",
                table: "CertificadoDeposito",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AlterColumn<double>(
                name: "MontoGarantia",
                table: "CertificadoDeposito",
                nullable: true,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaInicioComputo",
                table: "CertificadoDeposito",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<long>(
                name: "BankId",
                table: "CertificadoDeposito",
                nullable: true,
                oldClrType: typeof(long));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "PorcentajeInteresesInsolutos",
                table: "SolicitudCertificadoDeposito",
                nullable: false,
                oldClrType: typeof(double),
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "NoTraslado",
                table: "SolicitudCertificadoDeposito",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "MontoGarantia",
                table: "SolicitudCertificadoDeposito",
                nullable: false,
                oldClrType: typeof(double),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaVencimientoDeposito",
                table: "SolicitudCertificadoDeposito",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaPagoBanco",
                table: "SolicitudCertificadoDeposito",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaInicioComputo",
                table: "SolicitudCertificadoDeposito",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaFirma",
                table: "SolicitudCertificadoDeposito",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "BankId",
                table: "SolicitudCertificadoDeposito",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "PorcentajeInteresesInsolutos",
                table: "CertificadoDeposito",
                nullable: false,
                oldClrType: typeof(double),
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "NoTraslado",
                table: "CertificadoDeposito",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "MontoGarantia",
                table: "CertificadoDeposito",
                nullable: false,
                oldClrType: typeof(double),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaInicioComputo",
                table: "CertificadoDeposito",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "BankId",
                table: "CertificadoDeposito",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaFirma",
                table: "CertificadoDeposito",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaVencimiento",
                table: "CertificadoDeposito",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
