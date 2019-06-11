using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class SolicitudCertificadoDeposito : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SolicitudCertificadoDeposito",
                columns: table => new
                {
                    IdCD = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    NoCD = table.Column<long>(nullable: false),
                    CustomerId = table.Column<long>(nullable: false),
                    CustomerName = table.Column<string>(nullable: true),
                    WarehouseId = table.Column<long>(nullable: false),
                    WarehouseName = table.Column<string>(nullable: true),
                    ServicioId = table.Column<long>(nullable: false),
                    ServicioName = table.Column<string>(nullable: true),
                    Direccion = table.Column<string>(nullable: true),
                    FechaCertificado = table.Column<DateTime>(nullable: false),
                    NombreEmpresa = table.Column<string>(nullable: true),
                    IdEstado = table.Column<long>(nullable: false),
                    Estado = table.Column<string>(nullable: true),
                    EmpresaSeguro = table.Column<string>(nullable: true),
                    NoPoliza = table.Column<string>(nullable: true),
                    SujetasAPago = table.Column<double>(nullable: false),
                    FechaVencimientoDeposito = table.Column<DateTime>(nullable: false),
                    NoTraslado = table.Column<long>(nullable: false),
                    FechaVencimiento = table.Column<DateTime>(nullable: false),
                    Almacenaje = table.Column<string>(nullable: true),
                    Seguro = table.Column<string>(nullable: true),
                    OtrosCargos = table.Column<string>(nullable: true),
                    BankId = table.Column<long>(nullable: false),
                    BankName = table.Column<string>(nullable: true),
                    CurrencyId = table.Column<long>(nullable: false),
                    CurrencyName = table.Column<string>(nullable: true),
                    MontoGarantia = table.Column<double>(nullable: false),
                    FechaPagoBanco = table.Column<DateTime>(nullable: false),
                    PorcentajeInteresesInsolutos = table.Column<double>(nullable: false),
                    FechaInicioComputo = table.Column<DateTime>(nullable: false),
                    LugarFirma = table.Column<string>(nullable: true),
                    FechaFirma = table.Column<DateTime>(nullable: false),
                    NombrePrestatario = table.Column<string>(nullable: true),
                    Quantitysum = table.Column<double>(nullable: false),
                    Total = table.Column<double>(nullable: false),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false),
                    UsuarioCreacion = table.Column<string>(nullable: true),
                    UsuarioModificacion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SolicitudCertificadoDeposito", x => x.IdCD);
                });

            migrationBuilder.CreateTable(
                name: "SolicitudCertificadoLine",
                columns: table => new
                {
                    CertificadoLineId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IdCD = table.Column<long>(nullable: false),
                    SubProductId = table.Column<long>(nullable: false),
                    SubProductName = table.Column<string>(nullable: true),
                    UnitMeasureId = table.Column<long>(nullable: false),
                    UnitMeasurName = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Quantity = table.Column<double>(nullable: false),
                    Price = table.Column<double>(nullable: false),
                    Amount = table.Column<double>(nullable: false),
                    TotalCantidad = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SolicitudCertificadoLine", x => x.CertificadoLineId);
                    table.ForeignKey(
                        name: "FK_SolicitudCertificadoLine_SolicitudCertificadoDeposito_IdCD",
                        column: x => x.IdCD,
                        principalTable: "SolicitudCertificadoDeposito",
                        principalColumn: "IdCD",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SolicitudCertificadoLine_IdCD",
                table: "SolicitudCertificadoLine",
                column: "IdCD");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SolicitudCertificadoLine");

            migrationBuilder.DropTable(
                name: "SolicitudCertificadoDeposito");
        }
    }
}
