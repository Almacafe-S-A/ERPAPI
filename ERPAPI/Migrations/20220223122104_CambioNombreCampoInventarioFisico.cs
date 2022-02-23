using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class CambioNombreCampoInventarioFisico : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "InventarioFisico",
                table: "InventarioFisicoLines",
                newName: "InventarioFisicoCantidad");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "InventarioFisicoCantidad",
                table: "InventarioFisicoLines",
                newName: "InventarioFisico");
        }
    }
}
