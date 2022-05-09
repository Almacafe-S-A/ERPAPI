using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class BoletaSAlida : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GoodsDeliveredId",
                table: "BoletaDeSalida");

            migrationBuilder.DropColumn(
                name: "GoodsDeliveryAuthorizationId",
                table: "BoletaDeSalida");

            migrationBuilder.DropColumn(
                name: "GoodsReceivedId",
                table: "BoletaDeSalida");

            migrationBuilder.AddColumn<int>(
                name: "DocumentoId",
                table: "BoletaDeSalida",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "DocumentoTipo",
                table: "BoletaDeSalida",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProductName",
                table: "BoletaDeSalida",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DocumentoId",
                table: "BoletaDeSalida");

            migrationBuilder.DropColumn(
                name: "DocumentoTipo",
                table: "BoletaDeSalida");

            migrationBuilder.DropColumn(
                name: "ProductName",
                table: "BoletaDeSalida");

            migrationBuilder.AddColumn<long>(
                name: "GoodsDeliveredId",
                table: "BoletaDeSalida",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "GoodsDeliveryAuthorizationId",
                table: "BoletaDeSalida",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "GoodsReceivedId",
                table: "BoletaDeSalida",
                nullable: false,
                defaultValue: 0L);
        }
    }
}
