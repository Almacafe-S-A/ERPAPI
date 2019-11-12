using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class ChangeNameKeyCierresJournal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "JournalEntryId",
                table: "CierresJournal",
                newName: "CierresJournalEntryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CierresJournalEntryId",
                table: "CierresJournal",
                newName: "JournalEntryId");
        }
    }
}
