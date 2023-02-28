using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class removedfieldsformjournalentry : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JournalEntry_GeneralLedgerHeader_GeneralLedgerHeaderId1",
                table: "JournalEntry");

            migrationBuilder.DropForeignKey(
                name: "FK_JournalEntry_Party_PartyId1",
                table: "JournalEntry");

            migrationBuilder.DropIndex(
                name: "IX_JournalEntry_GeneralLedgerHeaderId1",
                table: "JournalEntry");

            migrationBuilder.DropIndex(
                name: "IX_JournalEntry_PartyId1",
                table: "JournalEntry");

            migrationBuilder.DropColumn(
                name: "ClosingEntry",
                table: "JournalEntry");

            migrationBuilder.DropColumn(
                name: "CurrencyId",
                table: "JournalEntry");

            migrationBuilder.DropColumn(
                name: "GeneralLedgerHeaderId",
                table: "JournalEntry");

            migrationBuilder.DropColumn(
                name: "GeneralLedgerHeaderId1",
                table: "JournalEntry");

            migrationBuilder.DropColumn(
                name: "PartyId1",
                table: "JournalEntry");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ClosingEntry",
                table: "JournalEntry",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CurrencyId",
                table: "JournalEntry",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "GeneralLedgerHeaderId",
                table: "JournalEntry",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "GeneralLedgerHeaderId1",
                table: "JournalEntry",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "PartyId1",
                table: "JournalEntry",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_JournalEntry_GeneralLedgerHeaderId1",
                table: "JournalEntry",
                column: "GeneralLedgerHeaderId1");

            migrationBuilder.CreateIndex(
                name: "IX_JournalEntry_PartyId1",
                table: "JournalEntry",
                column: "PartyId1");

            migrationBuilder.AddForeignKey(
                name: "FK_JournalEntry_GeneralLedgerHeader_GeneralLedgerHeaderId1",
                table: "JournalEntry",
                column: "GeneralLedgerHeaderId1",
                principalTable: "GeneralLedgerHeader",
                principalColumn: "GeneralLedgerHeaderId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_JournalEntry_Party_PartyId1",
                table: "JournalEntry",
                column: "PartyId1",
                principalTable: "Party",
                principalColumn: "PartyId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
