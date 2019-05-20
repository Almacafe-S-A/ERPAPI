using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class AddFieldsGoodsReceived : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "BranchId",
                table: "GoodsReceived",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "ProductId",
                table: "GoodsReceived",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<int>(
                name: "WarehouseId",
                table: "GoodsReceived",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<long>(
                name: "BranchId",
                table: "ControlPallets",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BranchId",
                table: "GoodsReceived");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "GoodsReceived");

            migrationBuilder.DropColumn(
                name: "WarehouseId",
                table: "GoodsReceived");

            migrationBuilder.DropColumn(
                name: "BranchId",
                table: "ControlPallets");
        }
    }
}
