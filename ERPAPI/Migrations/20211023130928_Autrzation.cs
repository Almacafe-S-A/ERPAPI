using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class Autrzation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrencyId",
                table: "GoodsDeliveryAuthorization");

            migrationBuilder.DropColumn(
                name: "CurrencyName",
                table: "GoodsDeliveryAuthorization");

            migrationBuilder.DropColumn(
                name: "WareHouseId",
                table: "GoodsDeliveryAuthorization");

            migrationBuilder.RenameColumn(
                name: "WareHouseName",
                table: "GoodsDeliveryAuthorization",
                newName: "Certificados");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Certificados",
                table: "GoodsDeliveryAuthorization",
                newName: "WareHouseName");

            migrationBuilder.AddColumn<long>(
                name: "CurrencyId",
                table: "GoodsDeliveryAuthorization",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "CurrencyName",
                table: "GoodsDeliveryAuthorization",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "WareHouseId",
                table: "GoodsDeliveryAuthorization",
                nullable: false,
                defaultValue: 0L);
        }
    }
}
