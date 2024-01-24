using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class PdaReciboMercaderia : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Pda",
                table: "GoodsDeliveredLine",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BoletaPesoId",
                table: "GoodsDelivered",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Pda",
                table: "GoodsDeliveredLine");

            migrationBuilder.DropColumn(
                name: "BoletaPesoId",
                table: "GoodsDelivered");
        }
    }
}
