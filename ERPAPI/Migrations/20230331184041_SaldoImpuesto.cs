using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class SaldoImpuesto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InvoicePayments_Invoice_InvoivceId",
                table: "InvoicePayments");

            migrationBuilder.DropIndex(
                name: "IX_InvoicePayments_InvoivceId",
                table: "InvoicePayments");

            migrationBuilder.DropColumn(
                name: "InvoivceId",
                table: "InvoicePayments");

            migrationBuilder.AddColumn<decimal>(
                name: "SaldoImpuesto",
                table: "Invoice",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SaldoImpuesto",
                table: "Invoice");

            migrationBuilder.AddColumn<int>(
                name: "InvoivceId",
                table: "InvoicePayments",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_InvoicePayments_InvoivceId",
                table: "InvoicePayments",
                column: "InvoivceId");

            migrationBuilder.AddForeignKey(
                name: "FK_InvoicePayments_Invoice_InvoivceId",
                table: "InvoicePayments",
                column: "InvoivceId",
                principalTable: "Invoice",
                principalColumn: "InvoiceId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
