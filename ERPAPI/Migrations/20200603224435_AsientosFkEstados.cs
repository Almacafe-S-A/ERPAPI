using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class AsientosFkEstados : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_JournalEntry_EstadoId",
                table: "JournalEntry",
                column: "EstadoId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountManagement_AccountId",
                table: "AccountManagement",
                column: "AccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_AccountManagement_Accounting_AccountId",
                table: "AccountManagement",
                column: "AccountId",
                principalTable: "Accounting",
                principalColumn: "AccountId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_JournalEntry_Estados_EstadoId",
                table: "JournalEntry",
                column: "EstadoId",
                principalTable: "Estados",
                principalColumn: "IdEstado",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccountManagement_Accounting_AccountId",
                table: "AccountManagement");

            migrationBuilder.DropForeignKey(
                name: "FK_JournalEntry_Estados_EstadoId",
                table: "JournalEntry");

            migrationBuilder.DropIndex(
                name: "IX_JournalEntry_EstadoId",
                table: "JournalEntry");

            migrationBuilder.DropIndex(
                name: "IX_AccountManagement_AccountId",
                table: "AccountManagement");
        }
    }
}
