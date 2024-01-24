using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class FKAccountingJournalentry : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JournalEntryLine_Accounting_AccountId1",
                table: "JournalEntryLine");

            migrationBuilder.DropIndex(
                name: "IX_JournalEntryLine_AccountId1",
                table: "JournalEntryLine");

            migrationBuilder.DropColumn(
                name: "AccountId1",
                table: "JournalEntryLine");

            migrationBuilder.AlterColumn<long>(
                name: "AccountId",
                table: "JournalEntryLine",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<string>(
                name: "ModifiedUser",
                table: "JournalEntry",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.CreateIndex(
                name: "IX_JournalEntryLine_AccountId",
                table: "JournalEntryLine",
                column: "AccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_JournalEntryLine_Accounting_AccountId",
                table: "JournalEntryLine",
                column: "AccountId",
                principalTable: "Accounting",
                principalColumn: "AccountId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JournalEntryLine_Accounting_AccountId",
                table: "JournalEntryLine");

            migrationBuilder.DropIndex(
                name: "IX_JournalEntryLine_AccountId",
                table: "JournalEntryLine");

            migrationBuilder.AlterColumn<int>(
                name: "AccountId",
                table: "JournalEntryLine",
                nullable: false,
                oldClrType: typeof(long));

            migrationBuilder.AddColumn<long>(
                name: "AccountId1",
                table: "JournalEntryLine",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ModifiedUser",
                table: "JournalEntry",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_JournalEntryLine_AccountId1",
                table: "JournalEntryLine",
                column: "AccountId1");

            migrationBuilder.AddForeignKey(
                name: "FK_JournalEntryLine_Accounting_AccountId1",
                table: "JournalEntryLine",
                column: "AccountId1",
                principalTable: "Accounting",
                principalColumn: "AccountId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
