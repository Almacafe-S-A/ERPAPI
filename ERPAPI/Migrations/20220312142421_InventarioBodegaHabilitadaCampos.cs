using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class InventarioBodegaHabilitadaCampos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NSacos",
                table: "InventarioFisicoLines",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "InventarioBodegaHabilitada",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    No = table.Column<int>(nullable: true),
                    InventarioFisicoId = table.Column<int>(nullable: false),
                    ProductoId = table.Column<long>(nullable: false),
                    Descripcion = table.Column<string>(nullable: true),
                    ProductoNombre = table.Column<string>(nullable: true),
                    SaldoLibros = table.Column<decimal>(nullable: false),
                    Cantidad = table.Column<decimal>(nullable: false),
                    UnitOfMeasureId = table.Column<long>(nullable: true),
                    UnitOfMeasureId1 = table.Column<int>(nullable: true),
                    Factor = table.Column<decimal>(nullable: false),
                    Valor = table.Column<decimal>(nullable: false),
                    Observacion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventarioBodegaHabilitada", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InventarioBodegaHabilitada_InventarioFisico_InventarioFisicoId",
                        column: x => x.InventarioFisicoId,
                        principalTable: "InventarioFisico",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InventarioBodegaHabilitada_SubProduct_ProductoId",
                        column: x => x.ProductoId,
                        principalTable: "SubProduct",
                        principalColumn: "SubproductId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InventarioBodegaHabilitada_UnitOfMeasure_UnitOfMeasureId1",
                        column: x => x.UnitOfMeasureId1,
                        principalTable: "UnitOfMeasure",
                        principalColumn: "UnitOfMeasureId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InventarioBodegaHabilitada_InventarioFisicoId",
                table: "InventarioBodegaHabilitada",
                column: "InventarioFisicoId");

            migrationBuilder.CreateIndex(
                name: "IX_InventarioBodegaHabilitada_ProductoId",
                table: "InventarioBodegaHabilitada",
                column: "ProductoId");

            migrationBuilder.CreateIndex(
                name: "IX_InventarioBodegaHabilitada_UnitOfMeasureId1",
                table: "InventarioBodegaHabilitada",
                column: "UnitOfMeasureId1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InventarioBodegaHabilitada");

            migrationBuilder.DropColumn(
                name: "NSacos",
                table: "InventarioFisicoLines");
        }
    }
}
