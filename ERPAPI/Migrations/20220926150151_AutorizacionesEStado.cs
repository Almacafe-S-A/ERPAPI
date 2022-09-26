using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class AutorizacionesEStado : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Estado",
                table: "GoodsDeliveryAuthorization",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EstadoId",
                table: "GoodsDeliveryAuthorization",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Estado",
                table: "GoodsDeliveryAuthorization");

            migrationBuilder.DropColumn(
                name: "EstadoId",
                table: "GoodsDeliveryAuthorization");
        }
    }
}
