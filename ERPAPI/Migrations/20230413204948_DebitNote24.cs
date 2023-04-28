using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class DebitNote24 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DebitNote_Invoice_InvoiceId",
                table: "DebitNote");

            migrationBuilder.AlterColumn<int>(
                name: "InvoiceId",
                table: "DebitNote",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_DebitNote_Invoice_InvoiceId",
                table: "DebitNote",
                column: "InvoiceId",
                principalTable: "Invoice",
                principalColumn: "InvoiceId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DebitNote_Invoice_InvoiceId",
                table: "DebitNote");

            migrationBuilder.AlterColumn<int>(
                name: "InvoiceId",
                table: "DebitNote",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_DebitNote_Invoice_InvoiceId",
                table: "DebitNote",
                column: "InvoiceId",
                principalTable: "Invoice",
                principalColumn: "InvoiceId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
