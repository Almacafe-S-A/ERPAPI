using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class TipoPlanillaEstados : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Estado",
                table: "TipoPlanillas");

            migrationBuilder.CreateIndex(
                name: "IX_TipoPlanillas_EstadoId",
                table: "TipoPlanillas",
                column: "EstadoId");

            migrationBuilder.AddForeignKey(
                name: "FK_TipoPlanillas_Estados_EstadoId",
                table: "TipoPlanillas",
                column: "EstadoId",
                principalTable: "Estados",
                principalColumn: "IdEstado",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TipoPlanillas_Estados_EstadoId",
                table: "TipoPlanillas");

            migrationBuilder.DropIndex(
                name: "IX_TipoPlanillas_EstadoId",
                table: "TipoPlanillas");

            migrationBuilder.AddColumn<string>(
                name: "Estado",
                table: "TipoPlanillas",
                nullable: true);
        }
    }
}
