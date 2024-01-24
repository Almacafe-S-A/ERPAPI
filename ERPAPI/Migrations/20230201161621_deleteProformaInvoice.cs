using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class deleteProformaInvoice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProformaInvoiceLine");

            migrationBuilder.DropTable(
                name: "ProformaInvoice");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProformaInvoice",
                columns: table => new
                {
                    ProformaId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Amount = table.Column<decimal>(nullable: false),
                    BranchId = table.Column<long>(nullable: false),
                    BranchName = table.Column<string>(nullable: true),
                    CertificadoDepositoId = table.Column<long>(nullable: false),
                    Correo = table.Column<string>(nullable: true),
                    Currency = table.Column<decimal>(nullable: false),
                    CurrencyId = table.Column<int>(nullable: false),
                    CurrencyName = table.Column<string>(nullable: true),
                    CustomerAreaId = table.Column<long>(nullable: false),
                    CustomerId = table.Column<long>(nullable: false),
                    CustomerName = table.Column<string>(nullable: true),
                    CustomerRefNumber = table.Column<string>(nullable: true),
                    DeliveryDate = table.Column<DateTime>(nullable: false),
                    Direccion = table.Column<string>(nullable: true),
                    Discount = table.Column<decimal>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: true),
                    Estado = table.Column<string>(nullable: true),
                    ExpirationDate = table.Column<DateTime>(nullable: false),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false),
                    Freight = table.Column<decimal>(nullable: false),
                    IdEstado = table.Column<long>(nullable: false),
                    Impreso = table.Column<string>(nullable: true),
                    OrderDate = table.Column<DateTime>(nullable: false),
                    ProductId = table.Column<long>(nullable: false),
                    ProductName = table.Column<string>(nullable: true),
                    ProformaName = table.Column<string>(nullable: true),
                    RTN = table.Column<string>(nullable: true),
                    Remarks = table.Column<string>(nullable: true),
                    SalesOrderId = table.Column<long>(nullable: false),
                    SalesTypeId = table.Column<int>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: true),
                    SubTotal = table.Column<decimal>(nullable: false),
                    Tax = table.Column<decimal>(nullable: false),
                    Tax18 = table.Column<decimal>(nullable: false),
                    Tefono = table.Column<string>(nullable: true),
                    Total = table.Column<decimal>(nullable: false),
                    TotalExento = table.Column<decimal>(nullable: false),
                    TotalExonerado = table.Column<decimal>(nullable: false),
                    TotalGravado = table.Column<decimal>(nullable: false),
                    TotalGravado18 = table.Column<decimal>(nullable: false),
                    UsuarioCreacion = table.Column<string>(nullable: true),
                    UsuarioModificacion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProformaInvoice", x => x.ProformaId);
                });

            migrationBuilder.CreateTable(
                name: "ProformaInvoiceLine",
                columns: table => new
                {
                    ProformaLineId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Amount = table.Column<decimal>(nullable: false),
                    CostCenterId = table.Column<long>(nullable: false),
                    CostCenterName = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    DiscountAmount = table.Column<decimal>(nullable: false),
                    DiscountPercentage = table.Column<decimal>(nullable: false),
                    Price = table.Column<decimal>(nullable: false),
                    ProductId = table.Column<long>(nullable: false),
                    ProductName = table.Column<string>(nullable: true),
                    ProformaInvoiceId = table.Column<int>(nullable: false),
                    Quantity = table.Column<decimal>(nullable: false),
                    SubProductId = table.Column<long>(nullable: false),
                    SubProductName = table.Column<string>(nullable: true),
                    SubTotal = table.Column<decimal>(nullable: false),
                    TaxAmount = table.Column<decimal>(nullable: false),
                    TaxCode = table.Column<string>(nullable: true),
                    TaxId = table.Column<long>(nullable: false),
                    TaxPercentage = table.Column<decimal>(nullable: false),
                    Total = table.Column<decimal>(nullable: false),
                    UnitOfMeasureId = table.Column<long>(nullable: false),
                    UnitOfMeasureName = table.Column<string>(nullable: true),
                    WareHouseId = table.Column<long>(nullable: false),
                    WareHouseName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProformaInvoiceLine", x => x.ProformaLineId);
                    table.ForeignKey(
                        name: "FK_ProformaInvoiceLine_ProformaInvoice_ProformaInvoiceId",
                        column: x => x.ProformaInvoiceId,
                        principalTable: "ProformaInvoice",
                        principalColumn: "ProformaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProformaInvoiceLine_ProformaInvoiceId",
                table: "ProformaInvoiceLine",
                column: "ProformaInvoiceId");
        }
    }
}
