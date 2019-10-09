using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class InvoiceCalculation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InvoiceCalculation",
                columns: table => new
                {
                    InvoiceCalculationId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DocumentDate = table.Column<DateTime>(nullable: false),
                    CustomerId = table.Column<long>(nullable: false),
                    ProformaInvoiceId = table.Column<long>(nullable: false),
                    InvoiceId = table.Column<long>(nullable: false),
                    IdCD = table.Column<long>(nullable: false),
                    NoCD = table.Column<long>(nullable: false),
                    ProductId = table.Column<long>(nullable: false),
                    ProductName = table.Column<string>(nullable: true),
                    Dias = table.Column<int>(nullable: false),
                    UnitPrice = table.Column<double>(nullable: false),
                    ValorLps = table.Column<double>(nullable: false),
                    ValorFacturar = table.Column<double>(nullable: false),
                    IngresoMercadería = table.Column<double>(nullable: false),
                    MercaderiaCertificada = table.Column<double>(nullable: false),
                    Dias2 = table.Column<int>(nullable: false),
                    PorcentajeMerma = table.Column<double>(nullable: false),
                    ValorLpsMerma = table.Column<double>(nullable: false),
                    ValorAFacturarMerma = table.Column<double>(nullable: false),
                    UsuarioCreacion = table.Column<string>(nullable: true),
                    UsuarioModificacion = table.Column<string>(nullable: true),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceCalculation", x => x.InvoiceCalculationId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InvoiceCalculation");
        }
    }
}
