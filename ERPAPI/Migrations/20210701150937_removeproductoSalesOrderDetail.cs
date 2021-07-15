using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class removeproductoSalesOrderDetail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SalesOrderLine_Product_ProductId",
                table: "SalesOrderLine");

            migrationBuilder.DropIndex(
                name: "IX_SalesOrderLine_ProductId",
                table: "SalesOrderLine");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "SalesOrderLine");

            migrationBuilder.DropColumn(
                name: "ProductName",
                table: "SalesOrderLine");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ProductId",
                table: "SalesOrderLine",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "ProductName",
                table: "SalesOrderLine",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SalesOrderLine_ProductId",
                table: "SalesOrderLine",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_SalesOrderLine_Product_ProductId",
                table: "SalesOrderLine",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
