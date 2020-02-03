using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class FksCierre : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CierresJournalEntryLine_Accounting_AccountId1",
                table: "CierresJournalEntryLine");

            migrationBuilder.DropForeignKey(
                name: "FK_CierresJournalEntryLine_JournalEntry_JournalEntryId",
                table: "CierresJournalEntryLine");

            //migrationBuilder.DropIndex(
            //    name: "IX_CierresJournalEntryLine_AccountId1",
            //    table: "CierresJournalEntryLine");

            //migrationBuilder.DropIndex(
            //    name: "IX_CierresJournalEntryLine_JournalEntryId",
            //    table: "CierresJournalEntryLine");

            //migrationBuilder.DropColumn(
            //    name: "AccountId1",
            //    table: "CierresJournalEntryLine");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "AccountId1",
                table: "CierresJournalEntryLine",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CierresJournalEntryLine_AccountId1",
                table: "CierresJournalEntryLine",
                column: "AccountId1");

            migrationBuilder.CreateIndex(
                name: "IX_CierresJournalEntryLine_JournalEntryId",
                table: "CierresJournalEntryLine",
                column: "JournalEntryId");

            migrationBuilder.AddForeignKey(
                name: "FK_CierresJournalEntryLine_Accounting_AccountId1",
                table: "CierresJournalEntryLine",
                column: "AccountId1",
                principalTable: "Accounting",
                principalColumn: "AccountId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CierresJournalEntryLine_JournalEntry_JournalEntryId",
                table: "CierresJournalEntryLine",
                column: "JournalEntryId",
                principalTable: "JournalEntry",
                principalColumn: "JournalEntryId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
