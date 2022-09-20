using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class CierreAnuales : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Anio",
                table: "BitacoraCierreContable",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Mes",
                table: "BitacoraCierreContable",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PeriodoId",
                table: "BitacoraCierreContable",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BitacoraCierreContable_PeriodoId",
                table: "BitacoraCierreContable",
                column: "PeriodoId");

            migrationBuilder.AddForeignKey(
                name: "FK_BitacoraCierreContable_Periodo_PeriodoId",
                table: "BitacoraCierreContable",
                column: "PeriodoId",
                principalTable: "Periodo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BitacoraCierreContable_Periodo_PeriodoId",
                table: "BitacoraCierreContable");

            migrationBuilder.DropIndex(
                name: "IX_BitacoraCierreContable_PeriodoId",
                table: "BitacoraCierreContable");

            migrationBuilder.DropColumn(
                name: "Anio",
                table: "BitacoraCierreContable");

            migrationBuilder.DropColumn(
                name: "Mes",
                table: "BitacoraCierreContable");

            migrationBuilder.DropColumn(
                name: "PeriodoId",
                table: "BitacoraCierreContable");
        }
    }
}
