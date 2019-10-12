using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class Creditnote_DebitNote : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CreditNote",
                columns: table => new
                {
                    CreditNoteId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreditNoteName = table.Column<string>(nullable: true),
                    ShipmentId = table.Column<int>(nullable: false),
                    IdPuntoEmision = table.Column<long>(nullable: false),
                    CreditNoteDate = table.Column<DateTime>(nullable: false),
                    CreditNoteDueDate = table.Column<DateTime>(nullable: false),
                    ExpirationDate = table.Column<DateTime>(nullable: false),
                    CreditNoteTypeId = table.Column<int>(nullable: false),
                    SalesOrderId = table.Column<long>(nullable: false),
                    CertificadoDepositoId = table.Column<long>(nullable: false),
                    Sucursal = table.Column<string>(nullable: true),
                    Caja = table.Column<string>(nullable: true),
                    TipoDocumento = table.Column<string>(nullable: true),
                    NúmeroDEI = table.Column<int>(nullable: false),
                    NoInicio = table.Column<string>(nullable: true),
                    NoFin = table.Column<string>(nullable: true),
                    FechaLimiteEmision = table.Column<DateTime>(nullable: false),
                    CAI = table.Column<string>(nullable: true),
                    NoOCExenta = table.Column<string>(nullable: true),
                    NoConstanciadeRegistro = table.Column<string>(nullable: true),
                    NoSAG = table.Column<string>(nullable: true),
                    RTN = table.Column<string>(nullable: true),
                    Tefono = table.Column<string>(nullable: true),
                    Correo = table.Column<string>(nullable: true),
                    Direccion = table.Column<string>(nullable: true),
                    BranchId = table.Column<int>(nullable: false),
                    BranchName = table.Column<string>(nullable: true),
                    CustomerId = table.Column<int>(nullable: false),
                    CustomerName = table.Column<string>(nullable: true),
                    ProductId = table.Column<long>(nullable: false),
                    ProductName = table.Column<string>(nullable: true),
                    OrderDate = table.Column<DateTime>(nullable: false),
                    DeliveryDate = table.Column<DateTime>(nullable: false),
                    CurrencyId = table.Column<int>(nullable: false),
                    CurrencyName = table.Column<string>(nullable: true),
                    Currency = table.Column<double>(nullable: false),
                    CustomerRefNumber = table.Column<string>(nullable: true),
                    SalesTypeId = table.Column<int>(nullable: false),
                    Remarks = table.Column<string>(nullable: true),
                    Amount = table.Column<double>(nullable: false),
                    SubTotal = table.Column<double>(nullable: false),
                    Discount = table.Column<double>(nullable: false),
                    Tax = table.Column<double>(nullable: false),
                    Tax18 = table.Column<double>(nullable: false),
                    Freight = table.Column<double>(nullable: false),
                    TotalExento = table.Column<double>(nullable: false),
                    TotalExonerado = table.Column<double>(nullable: false),
                    TotalGravado = table.Column<double>(nullable: false),
                    TotalGravado18 = table.Column<double>(nullable: false),
                    Total = table.Column<double>(nullable: false),
                    TotalLetras = table.Column<string>(nullable: true),
                    IdEstado = table.Column<long>(nullable: false),
                    Estado = table.Column<string>(nullable: true),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false),
                    UsuarioCreacion = table.Column<string>(nullable: true),
                    UsuarioModificacion = table.Column<string>(nullable: true),
                    Impreso = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CreditNote", x => x.CreditNoteId);
                });

            migrationBuilder.CreateTable(
                name: "DebitNote",
                columns: table => new
                {
                    DebitNoteId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DebitNoteName = table.Column<string>(nullable: true),
                    ShipmentId = table.Column<int>(nullable: false),
                    IdPuntoEmision = table.Column<long>(nullable: false),
                    DebitNoteDate = table.Column<DateTime>(nullable: false),
                    DebitNoteDueDate = table.Column<DateTime>(nullable: false),
                    ExpirationDate = table.Column<DateTime>(nullable: false),
                    DebitNoteTypeId = table.Column<int>(nullable: false),
                    SalesOrderId = table.Column<long>(nullable: false),
                    CertificadoDepositoId = table.Column<long>(nullable: false),
                    Sucursal = table.Column<string>(nullable: true),
                    Caja = table.Column<string>(nullable: true),
                    TipoDocumento = table.Column<string>(nullable: true),
                    NúmeroDEI = table.Column<int>(nullable: false),
                    NoInicio = table.Column<string>(nullable: true),
                    NoFin = table.Column<string>(nullable: true),
                    FechaLimiteEmision = table.Column<DateTime>(nullable: false),
                    CAI = table.Column<string>(nullable: true),
                    NoOCExenta = table.Column<string>(nullable: true),
                    NoConstanciadeRegistro = table.Column<string>(nullable: true),
                    NoSAG = table.Column<string>(nullable: true),
                    RTN = table.Column<string>(nullable: true),
                    Tefono = table.Column<string>(nullable: true),
                    Correo = table.Column<string>(nullable: true),
                    Direccion = table.Column<string>(nullable: true),
                    BranchId = table.Column<int>(nullable: false),
                    BranchName = table.Column<string>(nullable: true),
                    CustomerId = table.Column<int>(nullable: false),
                    CustomerName = table.Column<string>(nullable: true),
                    ProductId = table.Column<long>(nullable: false),
                    ProductName = table.Column<string>(nullable: true),
                    OrderDate = table.Column<DateTime>(nullable: false),
                    DeliveryDate = table.Column<DateTime>(nullable: false),
                    CurrencyId = table.Column<int>(nullable: false),
                    CurrencyName = table.Column<string>(nullable: true),
                    Currency = table.Column<double>(nullable: false),
                    CustomerRefNumber = table.Column<string>(nullable: true),
                    SalesTypeId = table.Column<int>(nullable: false),
                    Remarks = table.Column<string>(nullable: true),
                    Amount = table.Column<double>(nullable: false),
                    SubTotal = table.Column<double>(nullable: false),
                    Discount = table.Column<double>(nullable: false),
                    Tax = table.Column<double>(nullable: false),
                    Tax18 = table.Column<double>(nullable: false),
                    Freight = table.Column<double>(nullable: false),
                    TotalExento = table.Column<double>(nullable: false),
                    TotalExonerado = table.Column<double>(nullable: false),
                    TotalGravado = table.Column<double>(nullable: false),
                    TotalGravado18 = table.Column<double>(nullable: false),
                    Total = table.Column<double>(nullable: false),
                    TotalLetras = table.Column<string>(nullable: true),
                    IdEstado = table.Column<long>(nullable: false),
                    Estado = table.Column<string>(nullable: true),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false),
                    UsuarioCreacion = table.Column<string>(nullable: true),
                    UsuarioModificacion = table.Column<string>(nullable: true),
                    Impreso = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DebitNote", x => x.DebitNoteId);
                });

            migrationBuilder.CreateTable(
                name: "CreditNoteLine",
                columns: table => new
                {
                    CreditNoteLineId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreditNoteId = table.Column<int>(nullable: false),
                    ProductId = table.Column<long>(nullable: false),
                    ProductName = table.Column<string>(nullable: true),
                    SubProductId = table.Column<long>(nullable: false),
                    SubProductName = table.Column<string>(nullable: true),
                    UnitOfMeasureId = table.Column<long>(nullable: false),
                    UnitOfMeasureName = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Quantity = table.Column<double>(nullable: false),
                    Price = table.Column<double>(nullable: false),
                    Amount = table.Column<double>(nullable: false),
                    WareHouseId = table.Column<long>(nullable: false),
                    CostCenterId = table.Column<long>(nullable: false),
                    CostCenterName = table.Column<string>(nullable: true),
                    DiscountPercentage = table.Column<double>(nullable: false),
                    DiscountAmount = table.Column<double>(nullable: false),
                    SubTotal = table.Column<double>(nullable: false),
                    TaxPercentage = table.Column<double>(nullable: false),
                    TaxCode = table.Column<string>(nullable: true),
                    TaxAmount = table.Column<double>(nullable: false),
                    Total = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CreditNoteLine", x => x.CreditNoteLineId);
                    table.ForeignKey(
                        name: "FK_CreditNoteLine_CreditNote_CreditNoteId",
                        column: x => x.CreditNoteId,
                        principalTable: "CreditNote",
                        principalColumn: "CreditNoteId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DebitNoteLine",
                columns: table => new
                {
                    DebitNoteLineId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DebitNoteId = table.Column<int>(nullable: false),
                    DebitNoteId1 = table.Column<long>(nullable: true),
                    ProductId = table.Column<long>(nullable: false),
                    ProductName = table.Column<string>(nullable: true),
                    SubProductId = table.Column<long>(nullable: false),
                    SubProductName = table.Column<string>(nullable: true),
                    UnitOfMeasureId = table.Column<long>(nullable: false),
                    UnitOfMeasureName = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Quantity = table.Column<double>(nullable: false),
                    Price = table.Column<double>(nullable: false),
                    Amount = table.Column<double>(nullable: false),
                    WareHouseId = table.Column<long>(nullable: false),
                    CostCenterId = table.Column<long>(nullable: false),
                    CostCenterName = table.Column<string>(nullable: true),
                    DiscountPercentage = table.Column<double>(nullable: false),
                    DiscountAmount = table.Column<double>(nullable: false),
                    SubTotal = table.Column<double>(nullable: false),
                    TaxPercentage = table.Column<double>(nullable: false),
                    TaxCode = table.Column<string>(nullable: true),
                    TaxAmount = table.Column<double>(nullable: false),
                    Total = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DebitNoteLine", x => x.DebitNoteLineId);
                    table.ForeignKey(
                        name: "FK_DebitNoteLine_DebitNote_DebitNoteId1",
                        column: x => x.DebitNoteId1,
                        principalTable: "DebitNote",
                        principalColumn: "DebitNoteId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CreditNoteLine_CreditNoteId",
                table: "CreditNoteLine",
                column: "CreditNoteId");

            migrationBuilder.CreateIndex(
                name: "IX_DebitNoteLine_DebitNoteId1",
                table: "DebitNoteLine",
                column: "DebitNoteId1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CreditNoteLine");

            migrationBuilder.DropTable(
                name: "DebitNoteLine");

            migrationBuilder.DropTable(
                name: "CreditNote");

            migrationBuilder.DropTable(
                name: "DebitNote");
        }
    }
}
