using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class Autorizacionretiro3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "GoodsDeliveryAuthorizationLine");

            migrationBuilder.DropColumn(
                name: "valorfinanciado",
                table: "GoodsDeliveryAuthorizationLine");

            migrationBuilder.AddColumn<decimal>(
                name: "DerechosFiscales",
                table: "GoodsDeliveryAuthorizationLine",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "ValorUnitarioDerechos",
                table: "GoodsDeliveryAuthorizationLine",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DerechosFiscales",
                table: "GoodsDeliveryAuthorizationLine");

            migrationBuilder.DropColumn(
                name: "ValorUnitarioDerechos",
                table: "GoodsDeliveryAuthorizationLine");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "GoodsDeliveryAuthorizationLine",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "valorfinanciado",
                table: "GoodsDeliveryAuthorizationLine",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
