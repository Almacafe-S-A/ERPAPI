using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class camposauditoriaLiquidaciones : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CustomerName",
                table: "Liquidacion",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProductName",
                table: "Liquidacion",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_GoodsReceived_CustomerId",
                table: "GoodsReceived",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_GoodsReceived_Customer_CustomerId",
                table: "GoodsReceived",
                column: "CustomerId",
                principalTable: "Customer",
                principalColumn: "CustomerId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GoodsReceived_Customer_CustomerId",
                table: "GoodsReceived");

            migrationBuilder.DropIndex(
                name: "IX_GoodsReceived_CustomerId",
                table: "GoodsReceived");

            migrationBuilder.DropColumn(
                name: "CustomerName",
                table: "Liquidacion");

            migrationBuilder.DropColumn(
                name: "ProductName",
                table: "Liquidacion");
        }
    }
}
