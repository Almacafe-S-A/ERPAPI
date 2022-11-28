using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class limpiezaAutorizacionRetiro : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AuthorizationName",
                table: "GoodsDeliveryAuthorization");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "GoodsDeliveryAuthorization");

            migrationBuilder.DropColumn(
                name: "NoCD",
                table: "GoodsDeliveryAuthorization");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AuthorizationName",
                table: "GoodsDeliveryAuthorization",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "GoodsDeliveryAuthorization",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "NoCD",
                table: "GoodsDeliveryAuthorization",
                nullable: false,
                defaultValue: 0L);
        }
    }
}
