using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class Eliminar_campos_vendorinvoice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VendorInvoice_CostCenter_CostCenterId",
                table: "VendorInvoice");

            migrationBuilder.DropIndex(
                name: "IX_VendorInvoice_CostCenterId",
                table: "VendorInvoice");

            migrationBuilder.DropColumn(
                name: "DiscountAmount",
                table: "VendorInvoiceLine");

            migrationBuilder.DropColumn(
                name: "DiscountPercentage",
                table: "VendorInvoiceLine");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "VendorInvoiceLine");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "VendorInvoiceLine");

            migrationBuilder.DropColumn(
                name: "SubTotal",
                table: "VendorInvoiceLine");

            migrationBuilder.DropColumn(
                name: "UnitOfMeasureId",
                table: "VendorInvoiceLine");

            migrationBuilder.DropColumn(
                name: "UnitOfMeasureName",
                table: "VendorInvoiceLine");

            migrationBuilder.DropColumn(
                name: "CostCenterId",
                table: "VendorInvoice");

            migrationBuilder.DropColumn(
                name: "Sucursal",
                table: "VendorInvoice");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "DiscountAmount",
                table: "VendorInvoiceLine",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "DiscountPercentage",
                table: "VendorInvoiceLine",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Price",
                table: "VendorInvoiceLine",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Quantity",
                table: "VendorInvoiceLine",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "SubTotal",
                table: "VendorInvoiceLine",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<long>(
                name: "UnitOfMeasureId",
                table: "VendorInvoiceLine",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "UnitOfMeasureName",
                table: "VendorInvoiceLine",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "CostCenterId",
                table: "VendorInvoice",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Sucursal",
                table: "VendorInvoice",
                nullable: true);

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
        }
    }
}
