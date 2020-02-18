using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class FkPeriodo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PeriodoId",
                table: "Presupuesto",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Presupuesto_PeriodoId",
                table: "Presupuesto",
                column: "PeriodoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Presupuesto_Periodo_PeriodoId",
                table: "Presupuesto",
                column: "PeriodoId",
                principalTable: "Periodo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Presupuesto_Periodo_PeriodoId",
                table: "Presupuesto");

            migrationBuilder.DropIndex(
                name: "IX_Presupuesto_PeriodoId",
                table: "Presupuesto");

            migrationBuilder.DropColumn(
                name: "PeriodoId",
                table: "Presupuesto");
        }
    }
}
