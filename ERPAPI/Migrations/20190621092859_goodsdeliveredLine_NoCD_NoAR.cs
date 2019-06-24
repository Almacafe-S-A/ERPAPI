using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class goodsdeliveredLine_NoCD_NoAR : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "NoAR",
                table: "GoodsDeliveredLine",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "NoCD",
                table: "GoodsDeliveredLine",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NoAR",
                table: "GoodsDeliveredLine");

            migrationBuilder.DropColumn(
                name: "NoCD",
                table: "GoodsDeliveredLine");
        }
    }
}
