using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class TransferenciaInventario : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UsuarioModifiacion",
                table: "InventarioFisico",
                newName: "UsuarioModificacion");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UsuarioModificacion",
                table: "InventarioFisico",
                newName: "UsuarioModifiacion");
        }
    }
}
