using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class Autorizacionretiro2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Autorizados",
                table: "GoodsDeliveryAuthorizationLine");

            migrationBuilder.AddColumn<string>(
                name: "Autorizados",
                table: "GoodsDeliveryAuthorization",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Autorizados",
                table: "GoodsDeliveryAuthorization");

            migrationBuilder.AddColumn<string>(
                name: "Autorizados",
                table: "GoodsDeliveryAuthorizationLine",
                nullable: true);
        }
    }
}
