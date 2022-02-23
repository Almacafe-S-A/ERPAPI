using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class TransferenciaInventarrioMod : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UsuarioModificion",
                table: "InventarioFisico",
                newName: "UsuarioModifiacion");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UsuarioModifiacion",
                table: "InventarioFisico",
                newName: "UsuarioModificion");
        }
    }
}
