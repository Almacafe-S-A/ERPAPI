using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class GoodsReceivedAddFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "BranchName",
                table: "GoodsReceived",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "ProductName",
                table: "GoodsReceived",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "SubProductId",
                table: "GoodsReceived",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "SubProductName",
                table: "GoodsReceived",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "WarehouseName",
                table: "GoodsReceived",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BranchName",
                table: "GoodsReceived");

            migrationBuilder.DropColumn(
                name: "ProductName",
                table: "GoodsReceived");

            migrationBuilder.DropColumn(
                name: "SubProductId",
                table: "GoodsReceived");

            migrationBuilder.DropColumn(
                name: "SubProductName",
                table: "GoodsReceived");

            migrationBuilder.DropColumn(
                name: "WarehouseName",
                table: "GoodsReceived");
        }
    }
}
