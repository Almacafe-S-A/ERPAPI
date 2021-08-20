using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class LiquidacionLineField2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LiquidacionLine_SubProduct_SubProductId",
                table: "LiquidacionLine");

            migrationBuilder.AlterColumn<long>(
                name: "SubProductId",
                table: "LiquidacionLine",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AddForeignKey(
                name: "FK_LiquidacionLine_SubProduct_SubProductId",
                table: "LiquidacionLine",
                column: "SubProductId",
                principalTable: "SubProduct",
                principalColumn: "SubproductId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LiquidacionLine_SubProduct_SubProductId",
                table: "LiquidacionLine");

            migrationBuilder.AlterColumn<long>(
                name: "SubProductId",
                table: "LiquidacionLine",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_LiquidacionLine_SubProduct_SubProductId",
                table: "LiquidacionLine",
                column: "SubProductId",
                principalTable: "SubProduct",
                principalColumn: "SubproductId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
