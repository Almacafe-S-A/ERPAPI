using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class GoodsReceivedUOM : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CustomerUnitOfMeasure",
                table: "GoodsReceived",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CustomerUnitOfMeasureId",
                table: "GoodsReceived",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_GoodsReceived_CustomerUnitOfMeasureId",
                table: "GoodsReceived",
                column: "CustomerUnitOfMeasureId");

            migrationBuilder.AddForeignKey(
                name: "FK_GoodsReceived_UnitOfMeasure_CustomerUnitOfMeasureId",
                table: "GoodsReceived",
                column: "CustomerUnitOfMeasureId",
                principalTable: "UnitOfMeasure",
                principalColumn: "UnitOfMeasureId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GoodsReceived_UnitOfMeasure_CustomerUnitOfMeasureId",
                table: "GoodsReceived");

            migrationBuilder.DropIndex(
                name: "IX_GoodsReceived_CustomerUnitOfMeasureId",
                table: "GoodsReceived");

            migrationBuilder.DropColumn(
                name: "CustomerUnitOfMeasure",
                table: "GoodsReceived");

            migrationBuilder.DropColumn(
                name: "CustomerUnitOfMeasureId",
                table: "GoodsReceived");
        }
    }
}
