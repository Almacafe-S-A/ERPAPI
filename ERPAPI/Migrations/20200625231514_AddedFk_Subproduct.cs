using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class AddedFk_Subproduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ProductId",
                table: "GoodsReceivedLine",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SolicitudCertificadoLine_SubProductId",
                table: "SolicitudCertificadoLine",
                column: "SubProductId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesOrderLine_ProductId",
                table: "SalesOrderLine",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesOrderLine_SubProductId",
                table: "SalesOrderLine",
                column: "SubProductId");

            migrationBuilder.CreateIndex(
                name: "IX_GoodsReceivedLine_ProductId",
                table: "GoodsReceivedLine",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_GoodsReceivedLine_SubProductId",
                table: "GoodsReceivedLine",
                column: "SubProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_GoodsReceivedLine_Product_ProductId",
                table: "GoodsReceivedLine",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_GoodsReceivedLine_SubProduct_SubProductId",
                table: "GoodsReceivedLine",
                column: "SubProductId",
                principalTable: "SubProduct",
                principalColumn: "SubproductId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SalesOrderLine_Product_ProductId",
                table: "SalesOrderLine",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SalesOrderLine_SubProduct_SubProductId",
                table: "SalesOrderLine",
                column: "SubProductId",
                principalTable: "SubProduct",
                principalColumn: "SubproductId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SolicitudCertificadoLine_SubProduct_SubProductId",
                table: "SolicitudCertificadoLine",
                column: "SubProductId",
                principalTable: "SubProduct",
                principalColumn: "SubproductId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GoodsReceivedLine_Product_ProductId",
                table: "GoodsReceivedLine");

            migrationBuilder.DropForeignKey(
                name: "FK_GoodsReceivedLine_SubProduct_SubProductId",
                table: "GoodsReceivedLine");

            migrationBuilder.DropForeignKey(
                name: "FK_SalesOrderLine_Product_ProductId",
                table: "SalesOrderLine");

            migrationBuilder.DropForeignKey(
                name: "FK_SalesOrderLine_SubProduct_SubProductId",
                table: "SalesOrderLine");

            migrationBuilder.DropForeignKey(
                name: "FK_SolicitudCertificadoLine_SubProduct_SubProductId",
                table: "SolicitudCertificadoLine");

            migrationBuilder.DropIndex(
                name: "IX_SolicitudCertificadoLine_SubProductId",
                table: "SolicitudCertificadoLine");

            migrationBuilder.DropIndex(
                name: "IX_SalesOrderLine_ProductId",
                table: "SalesOrderLine");

            migrationBuilder.DropIndex(
                name: "IX_SalesOrderLine_SubProductId",
                table: "SalesOrderLine");

            migrationBuilder.DropIndex(
                name: "IX_GoodsReceivedLine_ProductId",
                table: "GoodsReceivedLine");

            migrationBuilder.DropIndex(
                name: "IX_GoodsReceivedLine_SubProductId",
                table: "GoodsReceivedLine");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "GoodsReceivedLine");
        }
    }
}
