using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class BoletaSalida_GoodsDelivered : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NoEntrega",
                table: "BoletaDeSalida",
                newName: "GoodsDeliveredId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "GoodsDeliveredId",
                table: "BoletaDeSalida",
                newName: "NoEntrega");
        }
    }
}
