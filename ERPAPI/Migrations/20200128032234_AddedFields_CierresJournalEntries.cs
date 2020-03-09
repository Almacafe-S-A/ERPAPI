using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class AddedFields_CierresJournalEntries : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM BitacoraCierreContable");

            migrationBuilder.Sql("DELETE FROM CierresJournalEntryLine", true);

            migrationBuilder.AddColumn<int>(
                name: "BitacoraCierreContableId",
                table: "CierresJournalEntryLine",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaCierre",
                table: "CierresJournalEntryLine",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "ApprovedBy",
                table: "CierresJournal",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CierresJournalEntryLine_BitacoraCierreContableId",
                table: "CierresJournalEntryLine",
                column: "BitacoraCierreContableId");

            migrationBuilder.AddForeignKey(
                name: "FK_CierresJournalEntryLine_BitacoraCierreContable_BitacoraCierreContableId",
                table: "CierresJournalEntryLine",
                column: "BitacoraCierreContableId",
                principalTable: "BitacoraCierreContable",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CierresJournalEntryLine_BitacoraCierreContable_BitacoraCierreContableId",
                table: "CierresJournalEntryLine");

            migrationBuilder.DropIndex(
                name: "IX_CierresJournalEntryLine_BitacoraCierreContableId",
                table: "CierresJournalEntryLine");

            migrationBuilder.DropColumn(
                name: "BitacoraCierreContableId",
                table: "CierresJournalEntryLine");

            migrationBuilder.DropColumn(
                name: "FechaCierre",
                table: "CierresJournalEntryLine");

            migrationBuilder.DropColumn(
                name: "ApprovedBy",
                table: "CierresJournal");
        }
    }
}
