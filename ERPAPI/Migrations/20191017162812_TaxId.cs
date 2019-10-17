using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class TaxId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "TaxId",
                table: "SalesOrderLine",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "TaxId",
                table: "ProformaInvoiceLine",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "TaxId",
                table: "InvoiceLine",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "TaxId",
                table: "DebitNoteLine",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "TaxId",
                table: "CreditNoteLine",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TaxId",
                table: "SalesOrderLine");

            migrationBuilder.DropColumn(
                name: "TaxId",
                table: "ProformaInvoiceLine");

            migrationBuilder.DropColumn(
                name: "TaxId",
                table: "InvoiceLine");

            migrationBuilder.DropColumn(
                name: "TaxId",
                table: "DebitNoteLine");

            migrationBuilder.DropColumn(
                name: "TaxId",
                table: "CreditNoteLine");
        }
    }
}
