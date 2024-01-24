using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class FeriadoPeriodoFK : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Anio",
                table: "Feriados",
                newName: "PeriodoId");

            migrationBuilder.CreateIndex(
                name: "IX_Feriados_PeriodoId",
                table: "Feriados",
                column: "PeriodoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Feriados_Periodo_PeriodoId",
                table: "Feriados",
                column: "PeriodoId",
                principalTable: "Periodo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Feriados_Periodo_PeriodoId",
                table: "Feriados");

            migrationBuilder.DropIndex(
                name: "IX_Feriados_PeriodoId",
                table: "Feriados");

            migrationBuilder.RenameColumn(
                name: "PeriodoId",
                table: "Feriados",
                newName: "Anio");
        }
    }
}
