using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class InvoicePaymentsAdjustments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "InvoivceId",
                table: "InvoicePaymentsLine",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "NoDocumento",
                table: "InvoicePaymentsLine",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TipoDocumento",
                table: "InvoicePaymentsLine",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "NoDocumentos",
                table: "InvoicePayments",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_InvoicePaymentsLine_InvoivceId",
                table: "InvoicePaymentsLine",
                column: "InvoivceId");

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

            migrationBuilder.DropIndex(
                name: "IX_InvoicePaymentsLine_InvoivceId",
                table: "InvoicePaymentsLine");

            migrationBuilder.DropColumn(
                name: "InvoivceId",
                table: "InvoicePaymentsLine");

            migrationBuilder.DropColumn(
                name: "NoDocumento",
                table: "InvoicePaymentsLine");

            migrationBuilder.DropColumn(
                name: "TipoDocumento",
                table: "InvoicePaymentsLine");

            migrationBuilder.DropColumn(
                name: "NoDocumentos",
                table: "InvoicePayments");
        }
    }
}
