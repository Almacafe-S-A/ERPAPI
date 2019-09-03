using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class EntryJournalfields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "JournalEntryLine",
                maxLength: 60,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Num",
                table: "JournalEntryLine",
                maxLength: 30,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DatePosted",
                table: "JournalEntry",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "TypeJournalName",
                table: "JournalEntry",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "JournalEntryLine");

            migrationBuilder.DropColumn(
                name: "Num",
                table: "JournalEntryLine");

            migrationBuilder.DropColumn(
                name: "DatePosted",
                table: "JournalEntry");

            migrationBuilder.DropColumn(
                name: "TypeJournalName",
                table: "JournalEntry");
        }
    }
}
