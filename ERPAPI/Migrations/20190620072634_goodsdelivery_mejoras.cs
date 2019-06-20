using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class goodsdelivery_mejoras : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "SubProductId",
                table: "GoodsDeliveryAuthorizationLine",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "SubProductName",
                table: "GoodsDeliveryAuthorizationLine",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UnitOfMeasureId",
                table: "GoodsDeliveryAuthorizationLine",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "UnitOfMeasureName",
                table: "GoodsDeliveryAuthorizationLine",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "WarehouseId",
                table: "GoodsDeliveryAuthorizationLine",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "WarehouseName",
                table: "GoodsDeliveryAuthorizationLine",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "CurrencyId",
                table: "GoodsDeliveryAuthorization",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "CurrencyName",
                table: "GoodsDeliveryAuthorization",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SubProductId",
                table: "GoodsDeliveryAuthorizationLine");

            migrationBuilder.DropColumn(
                name: "SubProductName",
                table: "GoodsDeliveryAuthorizationLine");

            migrationBuilder.DropColumn(
                name: "UnitOfMeasureId",
                table: "GoodsDeliveryAuthorizationLine");

            migrationBuilder.DropColumn(
                name: "UnitOfMeasureName",
                table: "GoodsDeliveryAuthorizationLine");

            migrationBuilder.DropColumn(
                name: "WarehouseId",
                table: "GoodsDeliveryAuthorizationLine");

            migrationBuilder.DropColumn(
                name: "WarehouseName",
                table: "GoodsDeliveryAuthorizationLine");

            migrationBuilder.DropColumn(
                name: "CurrencyId",
                table: "GoodsDeliveryAuthorization");

            migrationBuilder.DropColumn(
                name: "CurrencyName",
                table: "GoodsDeliveryAuthorization");
        }
    }
}
