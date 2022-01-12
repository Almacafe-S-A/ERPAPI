using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class addedwarehoseBOletaSalidad : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "WarehouseName",
                table: "BoletaDeSalidaLines",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Warehouseid",
                table: "BoletaDeSalidaLines",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BoletaDeSalidaLines_Warehouseid",
                table: "BoletaDeSalidaLines",
                column: "Warehouseid");

            migrationBuilder.AddForeignKey(
                name: "FK_BoletaDeSalidaLines_Warehouse_Warehouseid",
                table: "BoletaDeSalidaLines",
                column: "Warehouseid",
                principalTable: "Warehouse",
                principalColumn: "WarehouseId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BoletaDeSalidaLines_Warehouse_Warehouseid",
                table: "BoletaDeSalidaLines");

            migrationBuilder.DropIndex(
                name: "IX_BoletaDeSalidaLines_Warehouseid",
                table: "BoletaDeSalidaLines");

            migrationBuilder.DropColumn(
                name: "WarehouseName",
                table: "BoletaDeSalidaLines");

            migrationBuilder.DropColumn(
                name: "Warehouseid",
                table: "BoletaDeSalidaLines");
        }
    }
}
