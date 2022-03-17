using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class CambiosdetInventarioFisico2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InventarioFisico_Warehouse_WarehouseId",
                table: "InventarioFisico");

            migrationBuilder.DropIndex(
                name: "IX_InventarioFisico_WarehouseId",
                table: "InventarioFisico");

            migrationBuilder.DropColumn(
                name: "Bodega",
                table: "InventarioFisico");

            migrationBuilder.DropColumn(
                name: "WarehouseId",
                table: "InventarioFisico");

            migrationBuilder.DropColumn(
                name: "SaldoLibros",
                table: "InventarioBodegaHabilitada");

            migrationBuilder.AddColumn<decimal>(
                name: "FactorSacos",
                table: "InventarioFisicoLines",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "WarehouseId",
                table: "InventarioFisicoLines",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WarehouseName",
                table: "InventarioFisicoLines",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "FactorPergamino",
                table: "InventarioBodegaHabilitada",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "ValorPergamino",
                table: "InventarioBodegaHabilitada",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "WarehouseId",
                table: "InventarioBodegaHabilitada",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WarehouseName",
                table: "InventarioBodegaHabilitada",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_InventarioFisicoLines_WarehouseId",
                table: "InventarioFisicoLines",
                column: "WarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_InventarioFisico_BranchId",
                table: "InventarioFisico",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_InventarioBodegaHabilitada_WarehouseId",
                table: "InventarioBodegaHabilitada",
                column: "WarehouseId");

            migrationBuilder.AddForeignKey(
                name: "FK_InventarioBodegaHabilitada_Warehouse_WarehouseId",
                table: "InventarioBodegaHabilitada",
                column: "WarehouseId",
                principalTable: "Warehouse",
                principalColumn: "WarehouseId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_InventarioFisico_Branch_BranchId",
                table: "InventarioFisico",
                column: "BranchId",
                principalTable: "Branch",
                principalColumn: "BranchId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_InventarioFisicoLines_Warehouse_WarehouseId",
                table: "InventarioFisicoLines",
                column: "WarehouseId",
                principalTable: "Warehouse",
                principalColumn: "WarehouseId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InventarioBodegaHabilitada_Warehouse_WarehouseId",
                table: "InventarioBodegaHabilitada");

            migrationBuilder.DropForeignKey(
                name: "FK_InventarioFisico_Branch_BranchId",
                table: "InventarioFisico");

            migrationBuilder.DropForeignKey(
                name: "FK_InventarioFisicoLines_Warehouse_WarehouseId",
                table: "InventarioFisicoLines");

            migrationBuilder.DropIndex(
                name: "IX_InventarioFisicoLines_WarehouseId",
                table: "InventarioFisicoLines");

            migrationBuilder.DropIndex(
                name: "IX_InventarioFisico_BranchId",
                table: "InventarioFisico");

            migrationBuilder.DropIndex(
                name: "IX_InventarioBodegaHabilitada_WarehouseId",
                table: "InventarioBodegaHabilitada");

            migrationBuilder.DropColumn(
                name: "FactorSacos",
                table: "InventarioFisicoLines");

            migrationBuilder.DropColumn(
                name: "WarehouseId",
                table: "InventarioFisicoLines");

            migrationBuilder.DropColumn(
                name: "WarehouseName",
                table: "InventarioFisicoLines");

            migrationBuilder.DropColumn(
                name: "FactorPergamino",
                table: "InventarioBodegaHabilitada");

            migrationBuilder.DropColumn(
                name: "ValorPergamino",
                table: "InventarioBodegaHabilitada");

            migrationBuilder.DropColumn(
                name: "WarehouseId",
                table: "InventarioBodegaHabilitada");

            migrationBuilder.DropColumn(
                name: "WarehouseName",
                table: "InventarioBodegaHabilitada");

            migrationBuilder.AddColumn<string>(
                name: "Bodega",
                table: "InventarioFisico",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "WarehouseId",
                table: "InventarioFisico",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "SaldoLibros",
                table: "InventarioBodegaHabilitada",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateIndex(
                name: "IX_InventarioFisico_WarehouseId",
                table: "InventarioFisico",
                column: "WarehouseId");

            migrationBuilder.AddForeignKey(
                name: "FK_InventarioFisico_Warehouse_WarehouseId",
                table: "InventarioFisico",
                column: "WarehouseId",
                principalTable: "Warehouse",
                principalColumn: "WarehouseId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
