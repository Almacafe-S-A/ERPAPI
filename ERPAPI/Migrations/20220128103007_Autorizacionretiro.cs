using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class Autorizacionretiro : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Autorizados",
                table: "GoodsDeliveryAuthorizationLine",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Pda",
                table: "GoodsDeliveryAuthorizationLine",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Autorizados",
                table: "GoodsDeliveryAuthorizationLine");

            migrationBuilder.DropColumn(
                name: "Pda",
                table: "GoodsDeliveryAuthorizationLine");
        }
    }
}
