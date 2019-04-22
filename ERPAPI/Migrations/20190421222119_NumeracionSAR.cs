using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class NumeracionSAR : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SalesOrderName",
                table: "ProformaInvoice",
                newName: "ProformaName");

            migrationBuilder.RenameColumn(
                name: "SalesOrderId",
                table: "ProformaInvoice",
                newName: "ProformaId");

            migrationBuilder.RenameColumn(
                name: "SalesOrderLineId",
                table: "InvoiceLine",
                newName: "InvoiceLineId");

            migrationBuilder.CreateTable(
                name: "CAI",
                columns: table => new
                {
                    IdCAI = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    cai = table.Column<string>(nullable: true),
                    FechaRecepcion = table.Column<DateTime>(nullable: false),
                    FechaLimiteEmision = table.Column<DateTime>(nullable: false),
                    Estado = table.Column<string>(nullable: true),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false),
                    UsuarioCreacion = table.Column<string>(nullable: true),
                    UsuarioModificacion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CAI", x => x.IdCAI);
                });

            migrationBuilder.CreateTable(
                name: "CertificadoDeposito",
                columns: table => new
                {
                    IdCD = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CustomerId = table.Column<long>(nullable: false),
                    WarehouseId = table.Column<long>(nullable: false),
                    ServicioId = table.Column<long>(nullable: false),
                    Direccion = table.Column<string>(nullable: true),
                    FechaCertificado = table.Column<DateTime>(nullable: false),
                    NombreEmpresa = table.Column<string>(nullable: true),
                    EmpresaSeguro = table.Column<string>(nullable: true),
                    NoPoliza = table.Column<string>(nullable: true),
                    FechaVencimiento = table.Column<DateTime>(nullable: false),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false),
                    UsuarioCreacion = table.Column<string>(nullable: true),
                    UsuarioModificacion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CertificadoDeposito", x => x.IdCD);
                });

            migrationBuilder.CreateTable(
                name: "CertificadoLine",
                columns: table => new
                {
                    CertificadoLineId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CertificadoId = table.Column<int>(nullable: false),
                    ProductId = table.Column<long>(nullable: false),
                    ProductName = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Quantity = table.Column<double>(nullable: false),
                    Price = table.Column<double>(nullable: false),
                    Amount = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CertificadoLine", x => x.CertificadoLineId);
                });

            migrationBuilder.CreateTable(
                name: "NumeracionSAR",
                columns: table => new
                {
                    IdCAI = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    NoInicio = table.Column<string>(nullable: true),
                    NoFin = table.Column<string>(nullable: true),
                    FechaLimite = table.Column<DateTime>(nullable: false),
                    CantidadOtorgada = table.Column<int>(nullable: false),
                    SiguienteNumero = table.Column<string>(nullable: true),
                    PuntoEmision = table.Column<string>(nullable: true),
                    DocType = table.Column<string>(nullable: true),
                    DocSubType = table.Column<string>(nullable: true),
                    Estado = table.Column<string>(nullable: true),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false),
                    UsuarioCreacion = table.Column<string>(nullable: true),
                    UsuarioModificacion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NumeracionSAR", x => x.IdCAI);
                });

            migrationBuilder.CreateTable(
                name: "PuntoEmision",
                columns: table => new
                {
                    IdPuntoEmision = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PuntoEmisionCod = table.Column<string>(nullable: true),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false),
                    UsuarioCreacion = table.Column<string>(nullable: true),
                    UsuarioModificacion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PuntoEmision", x => x.IdPuntoEmision);
                });

            migrationBuilder.CreateTable(
                name: "TiposDocumento",
                columns: table => new
                {
                    IdTipoDocumento = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Codigo = table.Column<string>(nullable: true),
                    Descripcion = table.Column<string>(nullable: true),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false),
                    UsuarioCreacion = table.Column<string>(nullable: true),
                    UsuarioModificacion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TiposDocumento", x => x.IdTipoDocumento);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CAI");

            migrationBuilder.DropTable(
                name: "CertificadoDeposito");

            migrationBuilder.DropTable(
                name: "CertificadoLine");

            migrationBuilder.DropTable(
                name: "NumeracionSAR");

            migrationBuilder.DropTable(
                name: "PuntoEmision");

            migrationBuilder.DropTable(
                name: "TiposDocumento");

            migrationBuilder.RenameColumn(
                name: "ProformaName",
                table: "ProformaInvoice",
                newName: "SalesOrderName");

            migrationBuilder.RenameColumn(
                name: "ProformaId",
                table: "ProformaInvoice",
                newName: "SalesOrderId");

            migrationBuilder.RenameColumn(
                name: "InvoiceLineId",
                table: "InvoiceLine",
                newName: "SalesOrderLineId");
        }
    }
}
