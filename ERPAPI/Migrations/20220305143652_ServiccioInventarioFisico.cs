using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class ServiccioInventarioFisico : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ProductId",
                table: "InventarioFisico",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Servicio",
                table: "InventarioFisico",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_InventarioFisico_ProductId",
                table: "InventarioFisico",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_InventarioFisico_Product_ProductId",
                table: "InventarioFisico",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InventarioFisico_Product_ProductId",
                table: "InventarioFisico");

            migrationBuilder.DropIndex(
                name: "IX_InventarioFisico_ProductId",
                table: "InventarioFisico");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "InventarioFisico");

            migrationBuilder.DropColumn(
                name: "Servicio",
                table: "InventarioFisico");
        }
    }
}
