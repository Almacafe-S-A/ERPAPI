using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class VigilanteEntregasmercaderias : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "Accounting",
                table: "Tax",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "VigilanteId",
                table: "GoodsDelivered",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "VigilanteName",
                table: "GoodsDelivered",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Accounting",
                table: "Tax");

            migrationBuilder.DropColumn(
                name: "VigilanteId",
                table: "GoodsDelivered");

            migrationBuilder.DropColumn(
                name: "VigilanteName",
                table: "GoodsDelivered");
        }
    }
}
