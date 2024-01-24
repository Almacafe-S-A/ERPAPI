using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class SinopsisFacturaNotadePago : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerAcccountStatus_InvoicePayments_InvoicePaymentId",
                table: "CustomerAcccountStatus");

            migrationBuilder.DropColumn(
                name: "CreditNoteTypeId",
                table: "CustomerAcccountStatus");

            migrationBuilder.AddColumn<string>(
                name: "Sinopsis",
                table: "Invoice",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "InvoicePaymentId",
                table: "CustomerAcccountStatus",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<string>(
                name: "NoDocumento",
                table: "CustomerAcccountStatus",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Sinopsis",
                table: "CustomerAcccountStatus",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerAcccountStatus_InvoicePayments_InvoicePaymentId",
                table: "CustomerAcccountStatus",
                column: "InvoicePaymentId",
                principalTable: "InvoicePayments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerAcccountStatus_InvoicePayments_InvoicePaymentId",
                table: "CustomerAcccountStatus");

            migrationBuilder.DropColumn(
                name: "Sinopsis",
                table: "Invoice");

            migrationBuilder.DropColumn(
                name: "NoDocumento",
                table: "CustomerAcccountStatus");

            migrationBuilder.DropColumn(
                name: "Sinopsis",
                table: "CustomerAcccountStatus");

            migrationBuilder.AlterColumn<int>(
                name: "InvoicePaymentId",
                table: "CustomerAcccountStatus",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreditNoteTypeId",
                table: "CustomerAcccountStatus",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerAcccountStatus_InvoicePayments_InvoicePaymentId",
                table: "CustomerAcccountStatus",
                column: "InvoicePaymentId",
                principalTable: "InvoicePayments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
