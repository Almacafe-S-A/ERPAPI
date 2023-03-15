using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class GoodsReceived : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrencyId",
                table: "GoodsDelivered");

            migrationBuilder.DropColumn(
                name: "GoodsDeliveryAuthorizationId",
                table: "GoodsDelivered");

            migrationBuilder.DropColumn(
                name: "WarehouseId",
                table: "GoodsDelivered");

            migrationBuilder.DropColumn(
                name: "WeightBallot",
                table: "GoodsDelivered");

            migrationBuilder.RenameColumn(
                name: "CurrencyName",
                table: "GoodsDelivered",
                newName: "Certificados");

            migrationBuilder.AddColumn<string>(
                name: "Autorizaciones",
                table: "GoodsDelivered",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Autorizaciones",
                table: "GoodsDelivered");

            migrationBuilder.RenameColumn(
                name: "Certificados",
                table: "GoodsDelivered",
                newName: "CurrencyName");

            migrationBuilder.AddColumn<int>(
                name: "CurrencyId",
                table: "GoodsDelivered",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<long>(
                name: "GoodsDeliveryAuthorizationId",
                table: "GoodsDelivered",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<int>(
                name: "WarehouseId",
                table: "GoodsDelivered",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<long>(
                name: "WeightBallot",
                table: "GoodsDelivered",
                nullable: false,
                defaultValue: 0L);
        }
    }
}
