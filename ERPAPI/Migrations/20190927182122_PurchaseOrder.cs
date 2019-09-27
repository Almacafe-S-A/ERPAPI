using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class PurchaseOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            
            migrationBuilder.CreateTable(
                name: "PurchaseOrder",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PONumber = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    EstadoId = table.Column<long>(nullable: true),
                    POTypeId = table.Column<long>(nullable: true),
                    BranchId = table.Column<int>(nullable: true),
                    VendorId = table.Column<long>(nullable: true),
                    VendorName = table.Column<string>(nullable: true),
                    DatePlaced = table.Column<DateTime>(nullable: false),
                    CurrencyName = table.Column<string>(nullable: true),
                    CurrencyId = table.Column<int>(nullable: true),
                    Terms = table.Column<string>(nullable: true),
                    Freight = table.Column<double>(nullable: true),
                    TaxId = table.Column<long>(nullable: true),
                    TaxName = table.Column<string>(nullable: true),
                    TaxRate = table.Column<decimal>(nullable: false),
                    ShippingTypeId = table.Column<long>(nullable: true),
                    ShippingTypeName = table.Column<string>(nullable: true),
                    Requisitioner = table.Column<string>(nullable: true),
                    Remarks = table.Column<string>(nullable: true),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false),
                    UsuarioCreacion = table.Column<string>(nullable: true),
                    UsuarioModificacion = table.Column<string>(nullable: true),
                    Amount = table.Column<double>(nullable: false),
                    SubTotal = table.Column<double>(nullable: false),
                    Discount = table.Column<double>(nullable: false),
                    TaxAmount = table.Column<double>(nullable: false),
                    Tax18 = table.Column<double>(nullable: false),
                    TotalExento = table.Column<double>(nullable: false),
                    TotalExonerado = table.Column<double>(nullable: false),
                    TotalGravado = table.Column<double>(nullable: false),
                    TotalGravado18 = table.Column<double>(nullable: false),
                    Total = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseOrder", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PurchaseOrder_Branch_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branch",
                        principalColumn: "BranchId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PurchaseOrder_Currency_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "Currency",
                        principalColumn: "CurrencyId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PurchaseOrder_ElementoConfiguracion_EstadoId",
                        column: x => x.EstadoId,
                        principalTable: "ElementoConfiguracion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PurchaseOrder_Estados_EstadoId",
                        column: x => x.EstadoId,
                        principalTable: "Estados",
                        principalColumn: "IdEstado",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PurchaseOrder_ElementoConfiguracion_ShippingTypeId",
                        column: x => x.ShippingTypeId,
                        principalTable: "ElementoConfiguracion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PurchaseOrder_Tax_TaxId",
                        column: x => x.TaxId,
                        principalTable: "Tax",
                        principalColumn: "TaxId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PurchaseOrder_Vendor_VendorId",
                        column: x => x.VendorId,
                        principalTable: "Vendor",
                        principalColumn: "VendorId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PurchaseOrderLine",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    LineNumber = table.Column<int>(nullable: true),
                    PurchaseOrderId = table.Column<int>(nullable: false),
                    ProductId = table.Column<long>(nullable: true),
                    ProductDescription = table.Column<string>(nullable: true),
                    QtyOrdered = table.Column<double>(nullable: true),
                    QtyReceived = table.Column<double>(nullable: true),
                    QtyReceivedToDate = table.Column<double>(nullable: true),
                    Price = table.Column<double>(nullable: true),
                    TaxName = table.Column<string>(nullable: true),
                    TaxRate = table.Column<decimal>(nullable: false),
                    TaxId = table.Column<long>(nullable: true),
                    UnitOfMeasureId = table.Column<int>(nullable: false),
                    UnitOfMeasureName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseOrderLine", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PurchaseOrderLine_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PurchaseOrderLine_PurchaseOrder_PurchaseOrderId",
                        column: x => x.PurchaseOrderId,
                        principalTable: "PurchaseOrder",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PurchaseOrderLine_Tax_TaxId",
                        column: x => x.TaxId,
                        principalTable: "Tax",
                        principalColumn: "TaxId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PurchaseOrderLine_UnitOfMeasure_UnitOfMeasureId",
                        column: x => x.UnitOfMeasureId,
                        principalTable: "UnitOfMeasure",
                        principalColumn: "UnitOfMeasureId",
                        onDelete: ReferentialAction.Cascade);
                });

            
            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrder_BranchId",
                table: "PurchaseOrder",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrder_CurrencyId",
                table: "PurchaseOrder",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrder_EstadoId",
                table: "PurchaseOrder",
                column: "EstadoId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrder_ShippingTypeId",
                table: "PurchaseOrder",
                column: "ShippingTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrder_TaxId",
                table: "PurchaseOrder",
                column: "TaxId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrder_VendorId",
                table: "PurchaseOrder",
                column: "VendorId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrderLine_ProductId",
                table: "PurchaseOrderLine",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrderLine_PurchaseOrderId",
                table: "PurchaseOrderLine",
                column: "PurchaseOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrderLine_TaxId",
                table: "PurchaseOrderLine",
                column: "TaxId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrderLine_UnitOfMeasureId",
                table: "PurchaseOrderLine",
                column: "UnitOfMeasureId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            
            migrationBuilder.DropTable(
                name: "PurchaseOrderLine");

            migrationBuilder.DropTable(
                name: "PurchaseOrder");

        }
    }
}
