using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class LiquidacionesPrecios : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Liquidacion",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FechaLiquidacion = table.Column<DateTime>(nullable: false),
                    CustomerId = table.Column<long>(nullable: false),
                    ProductId = table.Column<long>(nullable: false),
                    Recibos = table.Column<string>(nullable: true),
                    TasaCambio = table.Column<decimal>(nullable: false),
                    DerechosImportacion = table.Column<decimal>(nullable: false),
                    SelectivoConsumo = table.Column<decimal>(nullable: false),
                    ImpuestoSobreVentas = table.Column<decimal>(nullable: false),
                    Flete = table.Column<decimal>(nullable: false),
                    Seguro = table.Column<decimal>(nullable: false),
                    Otros = table.Column<decimal>(nullable: false),
                    Total = table.Column<decimal>(nullable: false),
                    EstadoId = table.Column<long>(nullable: false),
                    FechaCreacion = table.Column<DateTime>(nullable: true),
                    FechaModificacion = table.Column<DateTime>(nullable: true),
                    UsuarioCreacion = table.Column<string>(nullable: true),
                    UsuarioModificacion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Liquidacion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Liquidacion_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Liquidacion_Estados_EstadoId",
                        column: x => x.EstadoId,
                        principalTable: "Estados",
                        principalColumn: "IdEstado",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Liquidacion_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LiquidacionLine",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    LiquidacionId = table.Column<int>(nullable: false),
                    GoodsReceivedLineId = table.Column<long>(nullable: false),
                    SubProductId = table.Column<long>(nullable: false),
                    SubProductName = table.Column<string>(nullable: true),
                    Cantidad = table.Column<decimal>(nullable: false),
                    TotalFOB = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalCIB = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TasaCambio = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalCIFLPS = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ValorDerechosImportacion = table.Column<decimal>(nullable: false),
                    TotalCIFDerechosImp = table.Column<decimal>(nullable: false),
                    ValorSelectivoConsumo = table.Column<decimal>(nullable: false),
                    OtrosImpuestos = table.Column<decimal>(nullable: false),
                    TotalImpuestoVentas = table.Column<decimal>(nullable: false),
                    TotalDerechosmasImpuestos = table.Column<decimal>(nullable: false),
                    TotalFinal = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LiquidacionLine", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LiquidacionLine_GoodsReceivedLine_GoodsReceivedLineId",
                        column: x => x.GoodsReceivedLineId,
                        principalTable: "GoodsReceivedLine",
                        principalColumn: "GoodsReceiveLinedId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LiquidacionLine_Liquidacion_LiquidacionId",
                        column: x => x.LiquidacionId,
                        principalTable: "Liquidacion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LiquidacionLine_SubProduct_SubProductId",
                        column: x => x.SubProductId,
                        principalTable: "SubProduct",
                        principalColumn: "SubproductId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Liquidacion_CustomerId",
                table: "Liquidacion",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Liquidacion_EstadoId",
                table: "Liquidacion",
                column: "EstadoId");

            migrationBuilder.CreateIndex(
                name: "IX_Liquidacion_ProductId",
                table: "Liquidacion",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_LiquidacionLine_GoodsReceivedLineId",
                table: "LiquidacionLine",
                column: "GoodsReceivedLineId");

            migrationBuilder.CreateIndex(
                name: "IX_LiquidacionLine_LiquidacionId",
                table: "LiquidacionLine",
                column: "LiquidacionId");

            migrationBuilder.CreateIndex(
                name: "IX_LiquidacionLine_SubProductId",
                table: "LiquidacionLine",
                column: "SubProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LiquidacionLine");

            migrationBuilder.DropTable(
                name: "Liquidacion");
        }
    }
}
