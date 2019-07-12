using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class ProductRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_ProductRelation_ProductId",
                table: "ProductRelation",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductRelation_SubProductId",
                table: "ProductRelation",
                column: "SubProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductRelation_Product_ProductId",
                table: "ProductRelation",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductRelation_SubProduct_SubProductId",
                table: "ProductRelation",
                column: "SubProductId",
                principalTable: "SubProduct",
                principalColumn: "SubproductId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductRelation_Product_ProductId",
                table: "ProductRelation");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductRelation_SubProduct_SubProductId",
                table: "ProductRelation");

            migrationBuilder.DropIndex(
                name: "IX_ProductRelation_ProductId",
                table: "ProductRelation");

            migrationBuilder.DropIndex(
                name: "IX_ProductRelation_SubProductId",
                table: "ProductRelation");
        }
    }
}
