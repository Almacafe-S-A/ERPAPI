using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class PeriodosAsientos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Periodo",
                table: "JournalEntry",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PeriodoId",
                table: "JournalEntry",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_JournalEntry_PeriodoId",
                table: "JournalEntry",
                column: "PeriodoId");

            migrationBuilder.AddForeignKey(
                name: "FK_JournalEntry_Periodo_PeriodoId",
                table: "JournalEntry",
                column: "PeriodoId",
                principalTable: "Periodo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JournalEntry_Periodo_PeriodoId",
                table: "JournalEntry");

            migrationBuilder.DropIndex(
                name: "IX_JournalEntry_PeriodoId",
                table: "JournalEntry");

            migrationBuilder.DropColumn(
                name: "Periodo",
                table: "JournalEntry");

            migrationBuilder.DropColumn(
                name: "PeriodoId",
                table: "JournalEntry");
        }
    }
}
