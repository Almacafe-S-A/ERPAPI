using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class JorunalEntryOptional_InvoicePayment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InvoicePayments_JournalEntry_JournalId",
                table: "InvoicePayments");

            migrationBuilder.AlterColumn<long>(
                name: "JournalId",
                table: "InvoicePayments",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AddForeignKey(
                name: "FK_InvoicePayments_JournalEntry_JournalId",
                table: "InvoicePayments",
                column: "JournalId",
                principalTable: "JournalEntry",
                principalColumn: "JournalEntryId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InvoicePayments_JournalEntry_JournalId",
                table: "InvoicePayments");

            migrationBuilder.AlterColumn<long>(
                name: "JournalId",
                table: "InvoicePayments",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_InvoicePayments_JournalEntry_JournalId",
                table: "InvoicePayments",
                column: "JournalId",
                principalTable: "JournalEntry",
                principalColumn: "JournalEntryId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
