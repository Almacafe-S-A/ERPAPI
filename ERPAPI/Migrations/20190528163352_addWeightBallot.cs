using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class addWeightBallot : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "WeightBallot",
                table: "GoodsReceived",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_GoodsReceivedLine_GoodsReceivedId",
                table: "GoodsReceivedLine",
                column: "GoodsReceivedId");

            migrationBuilder.CreateIndex(
                name: "IX_ControlPalletsLine_ControlPalletsId",
                table: "ControlPalletsLine",
                column: "ControlPalletsId");

            migrationBuilder.AddForeignKey(
                name: "FK_ControlPalletsLine_ControlPallets_ControlPalletsId",
                table: "ControlPalletsLine",
                column: "ControlPalletsId",
                principalTable: "ControlPallets",
                principalColumn: "ControlPalletsId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_GoodsReceivedLine_GoodsReceived_GoodsReceivedId",
                table: "GoodsReceivedLine",
                column: "GoodsReceivedId",
                principalTable: "GoodsReceived",
                principalColumn: "GoodsReceivedId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ControlPalletsLine_ControlPallets_ControlPalletsId",
                table: "ControlPalletsLine");

            migrationBuilder.DropForeignKey(
                name: "FK_GoodsReceivedLine_GoodsReceived_GoodsReceivedId",
                table: "GoodsReceivedLine");

            migrationBuilder.DropIndex(
                name: "IX_GoodsReceivedLine_GoodsReceivedId",
                table: "GoodsReceivedLine");

            migrationBuilder.DropIndex(
                name: "IX_ControlPalletsLine_ControlPalletsId",
                table: "ControlPalletsLine");

            migrationBuilder.DropColumn(
                name: "WeightBallot",
                table: "GoodsReceived");
        }
    }
}
