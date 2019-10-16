using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class DebitNoteId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DebitNoteLine_DebitNote_DebitNoteId1",
                table: "DebitNoteLine");

            migrationBuilder.DropIndex(
                name: "IX_DebitNoteLine_DebitNoteId1",
                table: "DebitNoteLine");

            migrationBuilder.DropColumn(
                name: "DebitNoteId1",
                table: "DebitNoteLine");

            migrationBuilder.AlterColumn<long>(
                name: "CustomerId",
                table: "ProformaInvoice",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<long>(
                name: "DebitNoteId",
                table: "DebitNoteLine",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.CreateIndex(
                name: "IX_DebitNoteLine_DebitNoteId",
                table: "DebitNoteLine",
                column: "DebitNoteId");

            migrationBuilder.AddForeignKey(
                name: "FK_DebitNoteLine_DebitNote_DebitNoteId",
                table: "DebitNoteLine",
                column: "DebitNoteId",
                principalTable: "DebitNote",
                principalColumn: "DebitNoteId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DebitNoteLine_DebitNote_DebitNoteId",
                table: "DebitNoteLine");

            migrationBuilder.DropIndex(
                name: "IX_DebitNoteLine_DebitNoteId",
                table: "DebitNoteLine");

            migrationBuilder.AlterColumn<int>(
                name: "CustomerId",
                table: "ProformaInvoice",
                nullable: false,
                oldClrType: typeof(long));

            migrationBuilder.AlterColumn<int>(
                name: "DebitNoteId",
                table: "DebitNoteLine",
                nullable: false,
                oldClrType: typeof(long));

            migrationBuilder.AddColumn<long>(
                name: "DebitNoteId1",
                table: "DebitNoteLine",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DebitNoteLine_DebitNoteId1",
                table: "DebitNoteLine",
                column: "DebitNoteId1");

            migrationBuilder.AddForeignKey(
                name: "FK_DebitNoteLine_DebitNote_DebitNoteId1",
                table: "DebitNoteLine",
                column: "DebitNoteId1",
                principalTable: "DebitNote",
                principalColumn: "DebitNoteId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
