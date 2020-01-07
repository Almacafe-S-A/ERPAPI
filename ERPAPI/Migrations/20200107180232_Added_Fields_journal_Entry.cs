using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class Added_Fields_journal_Entry : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApprovedBy",
                table: "JournalEntry",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ApprovedDate",
                table: "JournalEntry",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PartyName",
                table: "JournalEntry",
                nullable: true);

           
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApprovedBy",
                table: "JournalEntry");

            migrationBuilder.DropColumn(
                name: "ApprovedDate",
                table: "JournalEntry");

            migrationBuilder.DropColumn(
                name: "PartyName",
                table: "JournalEntry");
        }
    }
}
