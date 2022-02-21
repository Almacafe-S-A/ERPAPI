using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class InventariosFisico : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InventarioFisico",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    InventarioFisicoId = table.Column<int>(nullable: false),
                    Fecha = table.Column<DateTime>(nullable: false),
                    Sucursal = table.Column<string>(nullable: true),
                    WarehouseId = table.Column<int>(nullable: false),
                    Bodega = table.Column<string>(nullable: true),
                    CustomerId = table.Column<long>(nullable: false),
                    Cliente = table.Column<string>(nullable: true),
                    Comentarios = table.Column<string>(nullable: true),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificion = table.Column<DateTime>(nullable: false),
                    UsuarioCreacion = table.Column<string>(nullable: true),
                    UsuarioModificion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventarioFisico", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InventarioFisico_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InventarioFisico_InventarioFisico_InventarioFisicoId",
                        column: x => x.InventarioFisicoId,
                        principalTable: "InventarioFisico",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InventarioFisico_Warehouse_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "Warehouse",
                        principalColumn: "WarehouseId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InventarioFisicoLines",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ProductoId = table.Column<long>(nullable: false),
                    ProductoNombre = table.Column<string>(nullable: true),
                    SaldoLibros = table.Column<decimal>(nullable: false),
                    InventarioFisico = table.Column<decimal>(nullable: false),
                    Diferencia = table.Column<decimal>(nullable: false),
                    Observacion = table.Column<string>(nullable: true),
                    InventarioFisicoId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventarioFisicoLines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InventarioFisicoLines_InventarioFisico_InventarioFisicoId",
                        column: x => x.InventarioFisicoId,
                        principalTable: "InventarioFisico",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InventarioFisicoLines_SubProduct_ProductoId",
                        column: x => x.ProductoId,
                        principalTable: "SubProduct",
                        principalColumn: "SubproductId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InventarioFisico_CustomerId",
                table: "InventarioFisico",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_InventarioFisico_InventarioFisicoId",
                table: "InventarioFisico",
                column: "InventarioFisicoId");

            migrationBuilder.CreateIndex(
                name: "IX_InventarioFisico_WarehouseId",
                table: "InventarioFisico",
                column: "WarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_InventarioFisicoLines_InventarioFisicoId",
                table: "InventarioFisicoLines",
                column: "InventarioFisicoId");

            migrationBuilder.CreateIndex(
                name: "IX_InventarioFisicoLines_ProductoId",
                table: "InventarioFisicoLines",
                column: "ProductoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InventarioFisicoLines");

            migrationBuilder.DropTable(
                name: "InventarioFisico");
        }
    }
}
