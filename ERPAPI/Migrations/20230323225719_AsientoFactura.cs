using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class AsientoFactura : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "JournalEntryId",
                table: "Invoice",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Invoice_JournalEntryId",
                table: "Invoice",
                column: "JournalEntryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Invoice_JournalEntry_JournalEntryId",
                table: "Invoice",
                column: "JournalEntryId",
                principalTable: "JournalEntry",
                principalColumn: "JournalEntryId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invoice_JournalEntry_JournalEntryId",
                table: "Invoice");

            migrationBuilder.DropIndex(
                name: "IX_Invoice_JournalEntryId",
                table: "Invoice");

            migrationBuilder.DropColumn(
                name: "JournalEntryId",
                table: "Invoice");
        }
    }
}
