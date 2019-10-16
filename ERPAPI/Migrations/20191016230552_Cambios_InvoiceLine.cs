using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class Cambios_InvoiceLine : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VendorInvoiceLine_Invoice_InvoiceId",
                table: "VendorInvoiceLine");

            migrationBuilder.DropForeignKey(
                name: "FK_VendorInvoiceLine_VendorInvoice_VendorInvoiceId",
                table: "VendorInvoiceLine");

            migrationBuilder.DropIndex(
                name: "IX_VendorInvoiceLine_InvoiceId",
                table: "VendorInvoiceLine");

            migrationBuilder.DropColumn(
                name: "InvoiceId",
                table: "VendorInvoiceLine");

            migrationBuilder.RenameColumn(
                name: "InvoiceLineId",
                table: "VendorInvoiceLine",
                newName: "VendorInvoiceLineId");

            migrationBuilder.AlterColumn<int>(
                name: "VendorInvoiceId",
                table: "VendorInvoiceLine",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_VendorInvoiceLine_VendorInvoice_VendorInvoiceId",
                table: "VendorInvoiceLine",
                column: "VendorInvoiceId",
                principalTable: "VendorInvoice",
                principalColumn: "VendorInvoiceId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VendorInvoiceLine_VendorInvoice_VendorInvoiceId",
                table: "VendorInvoiceLine");

            migrationBuilder.RenameColumn(
                name: "VendorInvoiceLineId",
                table: "VendorInvoiceLine",
                newName: "InvoiceLineId");

            migrationBuilder.AlterColumn<int>(
                name: "VendorInvoiceId",
                table: "VendorInvoiceLine",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "InvoiceId",
                table: "VendorInvoiceLine",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_VendorInvoiceLine_InvoiceId",
                table: "VendorInvoiceLine",
                column: "InvoiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_VendorInvoiceLine_Invoice_InvoiceId",
                table: "VendorInvoiceLine",
                column: "InvoiceId",
                principalTable: "Invoice",
                principalColumn: "InvoiceId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VendorInvoiceLine_VendorInvoice_VendorInvoiceId",
                table: "VendorInvoiceLine",
                column: "VendorInvoiceId",
                principalTable: "VendorInvoice",
                principalColumn: "VendorInvoiceId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
