using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class customerEnGoodsDeliveryAuthorization : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "CustomerId",
                table: "GoodsDeliveryAuthorization",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "CustomerName",
                table: "GoodsDeliveryAuthorization",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "GoodsDeliveryAuthorization");

            migrationBuilder.DropColumn(
                name: "CustomerName",
                table: "GoodsDeliveryAuthorization");
        }
    }
}
