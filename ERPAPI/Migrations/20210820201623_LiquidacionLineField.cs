using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class LiquidacionLineField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LiquidacionLine_GoodsReceivedLine_GoodsReceivedLineId",
                table: "LiquidacionLine");

            migrationBuilder.AlterColumn<long>(
                name: "GoodsReceivedLineId",
                table: "LiquidacionLine",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AddForeignKey(
                name: "FK_LiquidacionLine_GoodsReceivedLine_GoodsReceivedLineId",
                table: "LiquidacionLine",
                column: "GoodsReceivedLineId",
                principalTable: "GoodsReceivedLine",
                principalColumn: "GoodsReceiveLinedId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LiquidacionLine_GoodsReceivedLine_GoodsReceivedLineId",
                table: "LiquidacionLine");

            migrationBuilder.AlterColumn<long>(
                name: "GoodsReceivedLineId",
                table: "LiquidacionLine",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_LiquidacionLine_GoodsReceivedLine_GoodsReceivedLineId",
                table: "LiquidacionLine",
                column: "GoodsReceivedLineId",
                principalTable: "GoodsReceivedLine",
                principalColumn: "GoodsReceiveLinedId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
