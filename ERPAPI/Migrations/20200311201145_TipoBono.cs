using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class TipoBono : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Bonificaciones_TipoId",
                table: "Bonificaciones",
                column: "TipoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bonificaciones_TiposBonificaciones_TipoId",
                table: "Bonificaciones",
                column: "TipoId",
                principalTable: "TiposBonificaciones",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bonificaciones_TiposBonificaciones_TipoId",
                table: "Bonificaciones");

            migrationBuilder.DropIndex(
                name: "IX_Bonificaciones_TipoId",
                table: "Bonificaciones");
        }
    }
}
