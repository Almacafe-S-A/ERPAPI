using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class Cie : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "CierresJournalEntryId",
                table: "CierresJournalEntryLine",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CierresJournalEntryLine_CierresJournalEntryId",
                table: "CierresJournalEntryLine",
                column: "CierresJournalEntryId");

            migrationBuilder.AddForeignKey(
                name: "FK_CierresJournalEntryLine_CierresJournal_CierresJournalEntryId",
                table: "CierresJournalEntryLine",
                column: "CierresJournalEntryId",
                principalTable: "CierresJournal",
                principalColumn: "CierresJournalEntryId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CierresJournalEntryLine_CierresJournal_CierresJournalEntryId",
                table: "CierresJournalEntryLine");

            migrationBuilder.DropIndex(
                name: "IX_CierresJournalEntryLine_CierresJournalEntryId",
                table: "CierresJournalEntryLine");

            migrationBuilder.DropColumn(
                name: "CierresJournalEntryId",
                table: "CierresJournalEntryLine");
        }
    }
}
