using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class placamarca_goodsreceveid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Marca",
                table: "GoodsReceived",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Placa",
                table: "GoodsReceived",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Marca",
                table: "GoodsReceived");

            migrationBuilder.DropColumn(
                name: "Placa",
                table: "GoodsReceived");
        }
    }
}
