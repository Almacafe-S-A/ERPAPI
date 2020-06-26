using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class UniqueProductRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ProductRelation_ProductId",
                table: "ProductRelation");

            migrationBuilder.CreateIndex(
                name: "IX_ProductRelation_ProductId_SubProductId",
                table: "ProductRelation",
                columns: new[] { "ProductId", "SubProductId" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ProductRelation_ProductId_SubProductId",
                table: "ProductRelation");

            migrationBuilder.CreateIndex(
                name: "IX_ProductRelation_ProductId",
                table: "ProductRelation",
                column: "ProductId");
        }
    }
}
