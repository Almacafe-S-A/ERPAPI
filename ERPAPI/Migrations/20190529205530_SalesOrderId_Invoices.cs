using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class SalesOrderId_Invoices : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "SubProductId",
                table: "ProformaInvoiceLine",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "SubProductName",
                table: "ProformaInvoiceLine",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "SalesOrderId",
                table: "ProformaInvoice",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "SubProductId",
                table: "InvoiceLine",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "SubProductName",
                table: "InvoiceLine",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "SalesOrderId",
                table: "Invoice",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SubProductId",
                table: "ProformaInvoiceLine");

            migrationBuilder.DropColumn(
                name: "SubProductName",
                table: "ProformaInvoiceLine");

            migrationBuilder.DropColumn(
                name: "SalesOrderId",
                table: "ProformaInvoice");

            migrationBuilder.DropColumn(
                name: "SubProductId",
                table: "InvoiceLine");

            migrationBuilder.DropColumn(
                name: "SubProductName",
                table: "InvoiceLine");

            migrationBuilder.DropColumn(
                name: "SalesOrderId",
                table: "Invoice");
        }
    }
}
