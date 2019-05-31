using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class RecibosCertificados_CertificadoDeposito : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "PesoBruto",
                table: "GoodsReceived",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "PesoNeto",
                table: "GoodsReceived",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "PesoNeto2",
                table: "GoodsReceived",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "TaraTransporte",
                table: "GoodsReceived",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "TaraUnidadMedida",
                table: "GoodsReceived",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<long>(
                name: "UnitMeasureId",
                table: "CertificadoLine",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "Almacenaje",
                table: "CertificadoDeposito",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "BankId",
                table: "CertificadoDeposito",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "BankName",
                table: "CertificadoDeposito",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "CurrencyId",
                table: "CertificadoDeposito",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "CurrencyName",
                table: "CertificadoDeposito",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaFirma",
                table: "CertificadoDeposito",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaInicioComputo",
                table: "CertificadoDeposito",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaPagoBanco",
                table: "CertificadoDeposito",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaVencimientoDeposito",
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
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<long>(
                name: "NoCD",
                table: "CertificadoDeposito",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "NoTraslado",
                table: "CertificadoDeposito",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "NombrePrestatario",
                table: "CertificadoDeposito",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OtrosCargos",
                table: "CertificadoDeposito",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "PorcentajeInteresesInsolutos",
                table: "CertificadoDeposito",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Quantitysum",
                table: "CertificadoDeposito",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "Seguro",
                table: "CertificadoDeposito",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "SujetasAPago",
                table: "CertificadoDeposito",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Total",
                table: "CertificadoDeposito",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.CreateTable(
                name: "RecibosCertificado",
                columns: table => new
                {
                    IdReciboCertificado = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IdRecibo = table.Column<long>(nullable: false),
                    IdCD = table.Column<long>(nullable: false),
                    UnitMeasureId = table.Column<long>(nullable: false),
                    productorecibolempiras = table.Column<double>(nullable: false),
                    productounidad = table.Column<double>(nullable: false),
                    productocantidadbultos = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecibosCertificado", x => x.IdReciboCertificado);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RecibosCertificado");

            migrationBuilder.DropColumn(
                name: "PesoBruto",
                table: "GoodsReceived");

            migrationBuilder.DropColumn(
                name: "PesoNeto",
                table: "GoodsReceived");

            migrationBuilder.DropColumn(
                name: "PesoNeto2",
                table: "GoodsReceived");

            migrationBuilder.DropColumn(
                name: "TaraTransporte",
                table: "GoodsReceived");

            migrationBuilder.DropColumn(
                name: "TaraUnidadMedida",
                table: "GoodsReceived");

            migrationBuilder.DropColumn(
                name: "UnitMeasureId",
                table: "CertificadoLine");

            migrationBuilder.DropColumn(
                name: "Almacenaje",
                table: "CertificadoDeposito");

            migrationBuilder.DropColumn(
                name: "BankId",
                table: "CertificadoDeposito");

            migrationBuilder.DropColumn(
                name: "BankName",
                table: "CertificadoDeposito");

            migrationBuilder.DropColumn(
                name: "CurrencyId",
                table: "CertificadoDeposito");

            migrationBuilder.DropColumn(
                name: "CurrencyName",
                table: "CertificadoDeposito");

            migrationBuilder.DropColumn(
                name: "FechaFirma",
                table: "CertificadoDeposito");

            migrationBuilder.DropColumn(
                name: "FechaInicioComputo",
                table: "CertificadoDeposito");

            migrationBuilder.DropColumn(
                name: "FechaPagoBanco",
                table: "CertificadoDeposito");

            migrationBuilder.DropColumn(
                name: "FechaVencimientoDeposito",
                table: "CertificadoDeposito");

            migrationBuilder.DropColumn(
                name: "LugarFirma",
                table: "CertificadoDeposito");

            migrationBuilder.DropColumn(
                name: "MontoGarantia",
                table: "CertificadoDeposito");

            migrationBuilder.DropColumn(
                name: "NoCD",
                table: "CertificadoDeposito");

            migrationBuilder.DropColumn(
                name: "NoTraslado",
                table: "CertificadoDeposito");

            migrationBuilder.DropColumn(
                name: "NombrePrestatario",
                table: "CertificadoDeposito");

            migrationBuilder.DropColumn(
                name: "OtrosCargos",
                table: "CertificadoDeposito");

            migrationBuilder.DropColumn(
                name: "PorcentajeInteresesInsolutos",
                table: "CertificadoDeposito");

            migrationBuilder.DropColumn(
                name: "Quantitysum",
                table: "CertificadoDeposito");

            migrationBuilder.DropColumn(
                name: "Seguro",
                table: "CertificadoDeposito");

            migrationBuilder.DropColumn(
                name: "SujetasAPago",
                table: "CertificadoDeposito");

            migrationBuilder.DropColumn(
                name: "Total",
                table: "CertificadoDeposito");
        }
    }
}
