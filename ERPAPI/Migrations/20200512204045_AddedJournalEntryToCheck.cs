using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class AddedJournalEntryToCheck : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "JournalEntrId",
                table: "CheckAccountLines",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CheckAccountLines_JournalEntrId",
                table: "CheckAccountLines",
                column: "JournalEntrId");

            migrationBuilder.AddForeignKey(
                name: "FK_CheckAccountLines_JournalEntry_JournalEntrId",
                table: "CheckAccountLines",
                column: "JournalEntrId",
                principalTable: "JournalEntry",
                principalColumn: "JournalEntryId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CheckAccountLines_JournalEntry_JournalEntrId",
                table: "CheckAccountLines");

            migrationBuilder.DropIndex(
                name: "IX_CheckAccountLines_JournalEntrId",
                table: "CheckAccountLines");

            migrationBuilder.DropColumn(
                name: "JournalEntrId",
                table: "CheckAccountLines");
        }
    }
}
