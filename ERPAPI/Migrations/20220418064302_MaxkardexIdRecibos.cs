using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class MaxkardexIdRecibos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "MaxKardexId",
                table: "GoodsReceivedLine",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_GoodsReceivedLine_MaxKardexId",
                table: "GoodsReceivedLine",
                column: "MaxKardexId");

            migrationBuilder.AddForeignKey(
                name: "FK_GoodsReceivedLine_Kardex_MaxKardexId",
                table: "GoodsReceivedLine",
                column: "MaxKardexId",
                principalTable: "Kardex",
                principalColumn: "KardexId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GoodsReceivedLine_Kardex_MaxKardexId",
                table: "GoodsReceivedLine");

            migrationBuilder.DropIndex(
                name: "IX_GoodsReceivedLine_MaxKardexId",
                table: "GoodsReceivedLine");

            migrationBuilder.DropColumn(
                name: "MaxKardexId",
                table: "GoodsReceivedLine");
        }
    }
}
