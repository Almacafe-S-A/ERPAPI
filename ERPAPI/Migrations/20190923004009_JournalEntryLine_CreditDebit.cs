using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class JournalEntryLine_CreditDebit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DrCr",
                table: "JournalEntryLine");

            migrationBuilder.RenameColumn(
                name: "Amount",
                table: "JournalEntryLine",
                newName: "DebitSy");

            migrationBuilder.AddColumn<double>(
                name: "Credit",
                table: "JournalEntryLine",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "CreditME",
                table: "JournalEntryLine",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "CreditSy",
                table: "JournalEntryLine",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Debit",
                table: "JournalEntryLine",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "DebitME",
                table: "JournalEntryLine",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Credit",
                table: "JournalEntryLine");

            migrationBuilder.DropColumn(
                name: "CreditME",
                table: "JournalEntryLine");

            migrationBuilder.DropColumn(
                name: "CreditSy",
                table: "JournalEntryLine");

            migrationBuilder.DropColumn(
                name: "Debit",
                table: "JournalEntryLine");

            migrationBuilder.DropColumn(
                name: "DebitME",
                table: "JournalEntryLine");

            migrationBuilder.RenameColumn(
                name: "DebitSy",
                table: "JournalEntryLine",
                newName: "Amount");

            migrationBuilder.AddColumn<int>(
                name: "DrCr",
                table: "JournalEntryLine",
                nullable: false,
                defaultValue: 0);
        }
    }
}
