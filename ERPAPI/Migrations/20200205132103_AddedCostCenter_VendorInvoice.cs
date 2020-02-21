using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class AddedCostCenter_VendorInvoice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "CostCenterId",
                table: "VendorInvoiceLine",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "CostCenterId",
                table: "VendorInvoice",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_VendorInvoiceLine_CostCenterId",
                table: "VendorInvoiceLine",
                column: "CostCenterId");

            migrationBuilder.CreateIndex(
                name: "IX_VendorInvoice_CostCenterId",
                table: "VendorInvoice",
                column: "CostCenterId");

            migrationBuilder.AddForeignKey(
                name: "FK_VendorInvoice_CostCenter_CostCenterId",
                table: "VendorInvoice",
                column: "CostCenterId",
                principalTable: "CostCenter",
                principalColumn: "CostCenterId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VendorInvoiceLine_CostCenter_CostCenterId",
                table: "VendorInvoiceLine",
                column: "CostCenterId",
                principalTable: "CostCenter",
                principalColumn: "CostCenterId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VendorInvoice_CostCenter_CostCenterId",
                table: "VendorInvoice");

            migrationBuilder.DropForeignKey(
                name: "FK_VendorInvoiceLine_CostCenter_CostCenterId",
                table: "VendorInvoiceLine");

            migrationBuilder.DropIndex(
                name: "IX_VendorInvoiceLine_CostCenterId",
                table: "VendorInvoiceLine");

            migrationBuilder.DropIndex(
                name: "IX_VendorInvoice_CostCenterId",
                table: "VendorInvoice");

            migrationBuilder.DropColumn(
                name: "CostCenterId",
                table: "VendorInvoiceLine");

            migrationBuilder.DropColumn(
                name: "CostCenterId",
                table: "VendorInvoice");
        }
    }
}
