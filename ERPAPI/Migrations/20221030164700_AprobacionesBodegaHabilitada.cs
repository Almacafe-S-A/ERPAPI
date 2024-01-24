using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class AprobacionesBodegaHabilitada : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AutorizadoPor",
                table: "InventarioFisico",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RevisadoPor",
                table: "InventarioFisico",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AutorizadoPor",
                table: "InventarioFisico");

            migrationBuilder.DropColumn(
                name: "RevisadoPor",
                table: "InventarioFisico");
        }
    }
}
