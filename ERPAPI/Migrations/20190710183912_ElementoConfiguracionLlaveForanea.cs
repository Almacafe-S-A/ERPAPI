using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class ElementoConfiguracionLlaveForanea : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_ElementoConfiguracion_Idconfiguracion",
                table: "ElementoConfiguracion",
                column: "Idconfiguracion");

            migrationBuilder.AddForeignKey(
                name: "FK_ElementoConfiguracion_GrupoConfiguracion_Idconfiguracion",
                table: "ElementoConfiguracion",
                column: "Idconfiguracion",
                principalTable: "GrupoConfiguracion",
                principalColumn: "IdConfiguracion",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ElementoConfiguracion_GrupoConfiguracion_Idconfiguracion",
                table: "ElementoConfiguracion");

            migrationBuilder.DropIndex(
                name: "IX_ElementoConfiguracion_Idconfiguracion",
                table: "ElementoConfiguracion");
        }
    }
}
