using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class GoodsDelivered : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Currency",
                table: "GoodsDelivered");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "GoodsDelivered",
                newName: "Motorista");

            migrationBuilder.AddColumn<string>(
                name: "EntregadoA",
                table: "GoodsDelivered",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EntregadoA",
                table: "GoodsDelivered");

            migrationBuilder.RenameColumn(
                name: "Motorista",
                table: "GoodsDelivered",
                newName: "Name");

            migrationBuilder.AddColumn<decimal>(
                name: "Currency",
                table: "GoodsDelivered",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
