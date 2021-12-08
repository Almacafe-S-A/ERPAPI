using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class AddedProductIdentification2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EsCafe",
                table: "GoodsReceivedLine");

            migrationBuilder.AddColumn<int>(
                name: "TipoCafe",
                table: "SubProduct",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TipoCafe",
                table: "SubProduct");

            migrationBuilder.AddColumn<bool>(
                name: "EsCafe",
                table: "GoodsReceivedLine",
                nullable: true);
        }
    }
}
