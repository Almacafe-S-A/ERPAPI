using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class GoodsReceivedLines : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProductName",
                table: "GoodsReceivedLine",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UnitOfMeasureName",
                table: "GoodsReceivedLine",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductName",
                table: "GoodsReceivedLine");

            migrationBuilder.DropColumn(
                name: "UnitOfMeasureName",
                table: "GoodsReceivedLine");
        }
    }
}
