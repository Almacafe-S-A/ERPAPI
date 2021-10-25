using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class detalleRecibo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GoodsReceivedLine_Product_ProductId",
                table: "GoodsReceivedLine");

            migrationBuilder.DropIndex(
                name: "IX_GoodsReceivedLine_ProductId",
                table: "GoodsReceivedLine");

            migrationBuilder.DropColumn(
                name: "ProducId",
                table: "GoodsReceivedLine");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "GoodsReceivedLine");

            migrationBuilder.DropColumn(
                name: "ProductName",
                table: "GoodsReceivedLine");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ProducId",
                table: "GoodsReceivedLine",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "ProductId",
                table: "GoodsReceivedLine",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProductName",
                table: "GoodsReceivedLine",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_GoodsReceivedLine_ProductId",
                table: "GoodsReceivedLine",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_GoodsReceivedLine_Product_ProductId",
                table: "GoodsReceivedLine",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
