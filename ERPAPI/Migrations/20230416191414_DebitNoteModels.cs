using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class DebitNoteModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NúmeroDEI",
                table: "DebitNote");

            migrationBuilder.AlterColumn<long>(
                name: "CustomerId",
                table: "DebitNote",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<long>(
                name: "JournalEntryId",
                table: "DebitNote",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NumeroDEI",
                table: "DebitNote",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Saldo",
                table: "DebitNote",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateIndex(
                name: "IX_DebitNote_CustomerId",
                table: "DebitNote",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_DebitNote_JournalEntryId",
                table: "DebitNote",
                column: "JournalEntryId");

            migrationBuilder.AddForeignKey(
                name: "FK_DebitNote_Customer_CustomerId",
                table: "DebitNote",
                column: "CustomerId",
                principalTable: "Customer",
                principalColumn: "CustomerId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DebitNote_JournalEntry_JournalEntryId",
                table: "DebitNote",
                column: "JournalEntryId",
                principalTable: "JournalEntry",
                principalColumn: "JournalEntryId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DebitNote_Customer_CustomerId",
                table: "DebitNote");

            migrationBuilder.DropForeignKey(
                name: "FK_DebitNote_JournalEntry_JournalEntryId",
                table: "DebitNote");

            migrationBuilder.DropIndex(
                name: "IX_DebitNote_CustomerId",
                table: "DebitNote");

            migrationBuilder.DropIndex(
                name: "IX_DebitNote_JournalEntryId",
                table: "DebitNote");

            migrationBuilder.DropColumn(
                name: "JournalEntryId",
                table: "DebitNote");

            migrationBuilder.DropColumn(
                name: "NumeroDEI",
                table: "DebitNote");

            migrationBuilder.DropColumn(
                name: "Saldo",
                table: "DebitNote");

            migrationBuilder.AlterColumn<int>(
                name: "CustomerId",
                table: "DebitNote",
                nullable: false,
                oldClrType: typeof(long));

            migrationBuilder.AddColumn<int>(
                name: "NúmeroDEI",
                table: "DebitNote",
                nullable: false,
                defaultValue: 0);
        }
    }
}
