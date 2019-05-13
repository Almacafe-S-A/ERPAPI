using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class addfieldsGrupoElementoConfiguracion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Fechamodificacion",
                table: "GrupoConfiguracion",
                newName: "FechaModificacion");

            migrationBuilder.RenameColumn(
                name: "Fechacreacion",
                table: "GrupoConfiguracion",
                newName: "FechaCreacion");

            migrationBuilder.RenameColumn(
                name: "Fechacreacion",
                table: "ElementoConfiguracion",
                newName: "FechaCreacion");

            migrationBuilder.AddColumn<string>(
                name: "UsuarioCreacion",
                table: "ElementoConfiguracion",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UsuarioModificacion",
                table: "ElementoConfiguracion",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UsuarioCreacion",
                table: "ElementoConfiguracion");

            migrationBuilder.DropColumn(
                name: "UsuarioModificacion",
                table: "ElementoConfiguracion");

            migrationBuilder.RenameColumn(
                name: "FechaModificacion",
                table: "GrupoConfiguracion",
                newName: "Fechamodificacion");

            migrationBuilder.RenameColumn(
                name: "FechaCreacion",
                table: "GrupoConfiguracion",
                newName: "Fechacreacion");

            migrationBuilder.RenameColumn(
                name: "FechaCreacion",
                table: "ElementoConfiguracion",
                newName: "Fechacreacion");
        }
    }
}
