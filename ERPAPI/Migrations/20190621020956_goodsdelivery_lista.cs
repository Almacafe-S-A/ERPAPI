using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class goodsdelivery_lista : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_GoodsDeliveryAuthorizationLine_GoodsDeliveryAuthorizationId",
                table: "GoodsDeliveryAuthorizationLine",
                column: "GoodsDeliveryAuthorizationId");

            migrationBuilder.AddForeignKey(
                name: "FK_GoodsDeliveryAuthorizationLine_GoodsDeliveryAuthorization_GoodsDeliveryAuthorizationId",
                table: "GoodsDeliveryAuthorizationLine",
                column: "GoodsDeliveryAuthorizationId",
                principalTable: "GoodsDeliveryAuthorization",
                principalColumn: "GoodsDeliveryAuthorizationId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GoodsDeliveryAuthorizationLine_GoodsDeliveryAuthorization_GoodsDeliveryAuthorizationId",
                table: "GoodsDeliveryAuthorizationLine");

            migrationBuilder.DropIndex(
                name: "IX_GoodsDeliveryAuthorizationLine_GoodsDeliveryAuthorizationId",
                table: "GoodsDeliveryAuthorizationLine");
        }
    }
}
