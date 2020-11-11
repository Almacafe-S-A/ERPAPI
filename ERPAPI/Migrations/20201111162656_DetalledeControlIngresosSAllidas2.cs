using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class DetalledeControlIngresosSAllidas2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Observacion",
                table: "ControlPalletsLine",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Qty",
                table: "ControlPalletsLine",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "SubPriductId",
                table: "ControlPalletsLine",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "SubProductId",
                table: "ControlPalletsLine",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UnitofMeasureId",
                table: "ControlPalletsLine",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "WarehouseId",
                table: "ControlPalletsLine",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ControlPalletsLine_SubProductId",
                table: "ControlPalletsLine",
                column: "SubProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ControlPalletsLine_UnitofMeasureId",
                table: "ControlPalletsLine",
                column: "UnitofMeasureId");

            migrationBuilder.CreateIndex(
                name: "IX_ControlPalletsLine_WarehouseId",
                table: "ControlPalletsLine",
                column: "WarehouseId");

            migrationBuilder.AddForeignKey(
                name: "FK_ControlPalletsLine_SubProduct_SubProductId",
                table: "ControlPalletsLine",
                column: "SubProductId",
                principalTable: "SubProduct",
                principalColumn: "SubproductId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ControlPalletsLine_UnitOfMeasure_UnitofMeasureId",
                table: "ControlPalletsLine",
                column: "UnitofMeasureId",
                principalTable: "UnitOfMeasure",
                principalColumn: "UnitOfMeasureId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ControlPalletsLine_Warehouse_WarehouseId",
                table: "ControlPalletsLine",
                column: "WarehouseId",
                principalTable: "Warehouse",
                principalColumn: "WarehouseId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ControlPalletsLine_SubProduct_SubProductId",
                table: "ControlPalletsLine");

            migrationBuilder.DropForeignKey(
                name: "FK_ControlPalletsLine_UnitOfMeasure_UnitofMeasureId",
                table: "ControlPalletsLine");

            migrationBuilder.DropForeignKey(
                name: "FK_ControlPalletsLine_Warehouse_WarehouseId",
                table: "ControlPalletsLine");

            migrationBuilder.DropIndex(
                name: "IX_ControlPalletsLine_SubProductId",
                table: "ControlPalletsLine");

            migrationBuilder.DropIndex(
                name: "IX_ControlPalletsLine_UnitofMeasureId",
                table: "ControlPalletsLine");

            migrationBuilder.DropIndex(
                name: "IX_ControlPalletsLine_WarehouseId",
                table: "ControlPalletsLine");

            migrationBuilder.DropColumn(
                name: "Observacion",
                table: "ControlPalletsLine");

            migrationBuilder.DropColumn(
                name: "Qty",
                table: "ControlPalletsLine");

            migrationBuilder.DropColumn(
                name: "SubPriductId",
                table: "ControlPalletsLine");

            migrationBuilder.DropColumn(
                name: "SubProductId",
                table: "ControlPalletsLine");

            migrationBuilder.DropColumn(
                name: "UnitofMeasureId",
                table: "ControlPalletsLine");

            migrationBuilder.DropColumn(
                name: "WarehouseId",
                table: "ControlPalletsLine");
        }
    }
}
