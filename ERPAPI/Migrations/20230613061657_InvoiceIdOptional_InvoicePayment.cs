using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class InvoiceIdOptional_InvoicePayment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InvoicePaymentsLine_Invoice_InvoivceId",
                table: "InvoicePaymentsLine");

            migrationBuilder.AlterColumn<int>(
                name: "InvoivceId",
                table: "InvoicePaymentsLine",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_InvoicePaymentsLine_Invoice_InvoivceId",
                table: "InvoicePaymentsLine",
                column: "InvoivceId",
                principalTable: "Invoice",
                principalColumn: "InvoiceId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InvoicePaymentsLine_Invoice_InvoivceId",
                table: "InvoicePaymentsLine");

            migrationBuilder.AlterColumn<int>(
                name: "InvoivceId",
                table: "InvoicePaymentsLine",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_InvoicePaymentsLine_Invoice_InvoivceId",
                table: "InvoicePaymentsLine",
                column: "InvoivceId",
                principalTable: "Invoice",
                principalColumn: "InvoiceId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
