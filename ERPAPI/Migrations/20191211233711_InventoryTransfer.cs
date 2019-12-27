using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class InventoryTransfer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            

            migrationBuilder.CreateTable(
                name: "InventoryTransfer",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: true),
                    SourceBranchId = table.Column<int>(nullable: false),
                    TargetBranchId = table.Column<int>(nullable: false),
                    DateGenerated = table.Column<DateTime>(nullable: false),
                    DepartureDate = table.Column<DateTime>(nullable: false),
                    DateReceived = table.Column<DateTime>(nullable: false),
                    GeneratedbyEmployeeId = table.Column<long>(nullable: false),
                    ReceivedByEmployeeId = table.Column<long>(nullable: false),
                    CarriedByEmployeeId = table.Column<long>(nullable: false),
                    EstadoId = table.Column<long>(nullable: false),
                    Estado = table.Column<string>(nullable: true),
                    ReasonId = table.Column<int>(nullable: false),
                    Reason = table.Column<string>(nullable: true),
                    CAI = table.Column<string>(nullable: true),
                    NumeroSAR = table.Column<string>(nullable: true),
                    Rango = table.Column<string>(nullable: true),
                    TipoDocumentoId = table.Column<int>(nullable: false),
                    NumeracionSARId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventoryTransfer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InventoryTransfer_Employees_CarriedByEmployeeId",
                        column: x => x.CarriedByEmployeeId,
                        principalTable: "Employees",
                        principalColumn: "IdEmpleado",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InventoryTransfer_Estados_EstadoId",
                        column: x => x.EstadoId,
                        principalTable: "Estados",
                        principalColumn: "IdEstado",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InventoryTransfer_TiposDocumento_EstadoId",
                        column: x => x.EstadoId,
                        principalTable: "TiposDocumento",
                        principalColumn: "IdTipoDocumento",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InventoryTransfer_Employees_GeneratedbyEmployeeId",
                        column: x => x.GeneratedbyEmployeeId,
                        principalTable: "Employees",
                        principalColumn: "IdEmpleado",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InventoryTransfer_NumeracionSAR_NumeracionSARId",
                        column: x => x.NumeracionSARId,
                        principalTable: "NumeracionSAR",
                        principalColumn: "IdNumeracion",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InventoryTransfer_Employees_ReceivedByEmployeeId",
                        column: x => x.ReceivedByEmployeeId,
                        principalTable: "Employees",
                        principalColumn: "IdEmpleado",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InventoryTransfer_Branch_SourceBranchId",
                        column: x => x.SourceBranchId,
                        principalTable: "Branch",
                        principalColumn: "BranchId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InventoryTransfer_Branch_TargetBranchId",
                        column: x => x.TargetBranchId,
                        principalTable: "Branch",
                        principalColumn: "BranchId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InventoryTransferLine",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    InventoryTransferId = table.Column<int>(nullable: false),
                    ProductId = table.Column<long>(nullable: false),
                    ProductName = table.Column<string>(nullable: true),
                    QtyStock = table.Column<decimal>(nullable: false),
                    QtyOut = table.Column<decimal>(nullable: false),
                    QtyIn = table.Column<decimal>(nullable: false),
                    Cost = table.Column<decimal>(nullable: false),
                    Price = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventoryTransferLine", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InventoryTransferLine_InventoryTransfer_InventoryTransferId",
                        column: x => x.InventoryTransferId,
                        principalTable: "InventoryTransfer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InventoryTransferLine_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InventoryTransfer_CarriedByEmployeeId",
                table: "InventoryTransfer",
                column: "CarriedByEmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryTransfer_EstadoId",
                table: "InventoryTransfer",
                column: "EstadoId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryTransfer_GeneratedbyEmployeeId",
                table: "InventoryTransfer",
                column: "GeneratedbyEmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryTransfer_NumeracionSARId",
                table: "InventoryTransfer",
                column: "NumeracionSARId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryTransfer_ReceivedByEmployeeId",
                table: "InventoryTransfer",
                column: "ReceivedByEmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryTransfer_SourceBranchId",
                table: "InventoryTransfer",
                column: "SourceBranchId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryTransfer_TargetBranchId",
                table: "InventoryTransfer",
                column: "TargetBranchId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryTransferLine_InventoryTransferId",
                table: "InventoryTransferLine",
                column: "InventoryTransferId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryTransferLine_ProductId",
                table: "InventoryTransferLine",
                column: "ProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InventoryTransferLine");

            migrationBuilder.DropTable(
                name: "InventoryTransfer");

            
        }
    }
}
