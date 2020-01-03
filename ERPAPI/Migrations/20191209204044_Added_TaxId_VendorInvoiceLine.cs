using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class Added_TaxId_VendorInvoiceLine : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "TaxId",
                table: "VendorInvoiceLine",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_VendorInvoiceLine_TaxId",
                table: "VendorInvoiceLine",
                column: "TaxId");

            migrationBuilder.AddForeignKey(
                name: "FK_VendorInvoiceLine_Tax_TaxId",
                table: "VendorInvoiceLine",
                column: "TaxId",
                principalTable: "Tax",
                principalColumn: "TaxId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VendorInvoiceLine_Tax_TaxId",
                table: "VendorInvoiceLine");

            migrationBuilder.DropIndex(
                name: "IX_VendorInvoiceLine_TaxId",
                table: "VendorInvoiceLine");

            migrationBuilder.DropColumn(
                name: "TaxId",
                table: "VendorInvoiceLine");
        }
    }
}
