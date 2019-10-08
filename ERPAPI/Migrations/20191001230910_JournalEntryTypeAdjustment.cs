using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class JournalEntryTypeAdjustment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AccountName",
                table: "JournalEntryLine",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "TotalCredit",
                table: "JournalEntry",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "TotalDebit",
                table: "JournalEntry",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "TypeOfAdjustmentId",
                table: "JournalEntry",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "TypeOfAdjustmentName",
                table: "JournalEntry",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccountName",
                table: "JournalEntryLine");

            migrationBuilder.DropColumn(
                name: "TotalCredit",
                table: "JournalEntry");

            migrationBuilder.DropColumn(
                name: "TotalDebit",
                table: "JournalEntry");

            migrationBuilder.DropColumn(
                name: "TypeOfAdjustmentId",
                table: "JournalEntry");

            migrationBuilder.DropColumn(
                name: "TypeOfAdjustmentName",
                table: "JournalEntry");
        }
    }
}
