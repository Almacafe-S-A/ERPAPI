using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class GDA_GDss : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GoodsDeliveredLine_GoodsDeliveryAuthorizationLine_NoARLine",
                table: "GoodsDeliveredLine");

            migrationBuilder.DropIndex(
                name: "IX_GoodsDeliveredLine_NoARLine",
                table: "GoodsDeliveredLine");

            migrationBuilder.DropColumn(
                name: "NoARLine",
                table: "GoodsDeliveredLine");

            migrationBuilder.AlterColumn<long>(
                name: "NoARLineId",
                table: "GoodsDeliveredLine",
                nullable: true,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "NoARLineId",
                table: "GoodsDeliveredLine",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AddColumn<long>(
                name: "NoARLine",
                table: "GoodsDeliveredLine",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_GoodsDeliveredLine_NoARLine",
                table: "GoodsDeliveredLine",
                column: "NoARLine");

            migrationBuilder.AddForeignKey(
                name: "FK_GoodsDeliveredLine_GoodsDeliveryAuthorizationLine_NoARLine",
                table: "GoodsDeliveredLine",
                column: "NoARLine",
                principalTable: "GoodsDeliveryAuthorizationLine",
                principalColumn: "GoodsDeliveryAuthorizationLineId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
