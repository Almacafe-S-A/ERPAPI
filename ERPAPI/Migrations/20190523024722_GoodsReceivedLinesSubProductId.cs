using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class GoodsReceivedLinesSubProductId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "SubProductId",
                table: "GoodsReceivedLine",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "SubProductName",
                table: "GoodsReceivedLine",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SubProductId",
                table: "GoodsReceivedLine");

            migrationBuilder.DropColumn(
                name: "SubProductName",
                table: "GoodsReceivedLine");
        }
    }
}
