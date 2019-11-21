using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class RemoveNavPropGrupoEstados : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Estados_GrupoConfiguracion_IdGrupoEstado",
                table: "Estados");

            //migrationBuilder.DropIndex(
            //    name: "IX_Estados_IdGrupoEstado",
            //    table: "Estados");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Estados_IdGrupoEstado",
                table: "Estados",
                column: "IdGrupoEstado");

            migrationBuilder.AddForeignKey(
                name: "FK_Estados_GrupoConfiguracion_IdGrupoEstado",
                table: "Estados",
                column: "IdGrupoEstado",
                principalTable: "GrupoConfiguracion",
                principalColumn: "IdConfiguracion",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
