using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class NombreProducto_Cliente : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceLine_SalesOrder_SalesOrderId",
                table: "InvoiceLine");

            migrationBuilder.RenameColumn(
                name: "SalesOrderId",
                table: "InvoiceLine",
                newName: "InvoiceId");

            migrationBuilder.RenameIndex(
                name: "IX_InvoiceLine_SalesOrderId",
                table: "InvoiceLine",
                newName: "IX_InvoiceLine_InvoiceId");

            migrationBuilder.AddColumn<string>(
                name: "ProductName",
                table: "SalesOrderLine",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CustomerName",
                table: "SalesOrder",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProductName",
                table: "ProformaInvoiceLine",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CustomerName",
                table: "ProformaInvoice",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProductName",
                table: "InvoiceLine",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CustomerName",
                table: "Invoice",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceLine_Invoice_InvoiceId",
                table: "InvoiceLine",
                column: "InvoiceId",
                principalTable: "Invoice",
                principalColumn: "InvoiceId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceLine_Invoice_InvoiceId",
                table: "InvoiceLine");

            migrationBuilder.DropColumn(
                name: "ProductName",
                table: "SalesOrderLine");

            migrationBuilder.DropColumn(
                name: "CustomerName",
                table: "SalesOrder");

            migrationBuilder.DropColumn(
                name: "ProductName",
                table: "ProformaInvoiceLine");

            migrationBuilder.DropColumn(
                name: "CustomerName",
                table: "ProformaInvoice");

            migrationBuilder.DropColumn(
                name: "ProductName",
                table: "InvoiceLine");

            migrationBuilder.DropColumn(
                name: "CustomerName",
                table: "Invoice");

            migrationBuilder.RenameColumn(
                name: "InvoiceId",
                table: "InvoiceLine",
                newName: "SalesOrderId");

            migrationBuilder.RenameIndex(
                name: "IX_InvoiceLine_InvoiceId",
                table: "InvoiceLine",
                newName: "IX_InvoiceLine_SalesOrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceLine_SalesOrder_SalesOrderId",
                table: "InvoiceLine",
                column: "SalesOrderId",
                principalTable: "SalesOrder",
                principalColumn: "SalesOrderId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
