using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class InvoiceSubService : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceLine_SubServicesWareHouse_SubservicesWarehouseId",
                table: "InvoiceLine");

            migrationBuilder.RenameColumn(
                name: "SubservicesWarehouseId",
                table: "InvoiceLine",
                newName: "CustomerAreaId");

            migrationBuilder.RenameIndex(
                name: "IX_InvoiceLine_SubservicesWarehouseId",
                table: "InvoiceLine",
                newName: "IX_InvoiceLine_CustomerAreaId");

            migrationBuilder.AddColumn<int>(
                name: "InvoiceId",
                table: "SubServicesWareHouse",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "InvoiceId",
                table: "CustomerArea",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SubServicesWareHouse_InvoiceId",
                table: "SubServicesWareHouse",
                column: "InvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerArea_InvoiceId",
                table: "CustomerArea",
                column: "InvoiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerArea_Invoice_InvoiceId",
                table: "CustomerArea",
                column: "InvoiceId",
                principalTable: "Invoice",
                principalColumn: "InvoiceId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceLine_CustomerArea_CustomerAreaId",
                table: "InvoiceLine",
                column: "CustomerAreaId",
                principalTable: "CustomerArea",
                principalColumn: "CustomerAreaId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SubServicesWareHouse_Invoice_InvoiceId",
                table: "SubServicesWareHouse",
                column: "InvoiceId",
                principalTable: "Invoice",
                principalColumn: "InvoiceId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerArea_Invoice_InvoiceId",
                table: "CustomerArea");

            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceLine_CustomerArea_CustomerAreaId",
                table: "InvoiceLine");

            migrationBuilder.DropForeignKey(
                name: "FK_SubServicesWareHouse_Invoice_InvoiceId",
                table: "SubServicesWareHouse");

            migrationBuilder.DropIndex(
                name: "IX_SubServicesWareHouse_InvoiceId",
                table: "SubServicesWareHouse");

            migrationBuilder.DropIndex(
                name: "IX_CustomerArea_InvoiceId",
                table: "CustomerArea");

            migrationBuilder.DropColumn(
                name: "InvoiceId",
                table: "SubServicesWareHouse");

            migrationBuilder.DropColumn(
                name: "InvoiceId",
                table: "CustomerArea");

            migrationBuilder.RenameColumn(
                name: "CustomerAreaId",
                table: "InvoiceLine",
                newName: "SubservicesWarehouseId");

            migrationBuilder.RenameIndex(
                name: "IX_InvoiceLine_CustomerAreaId",
                table: "InvoiceLine",
                newName: "IX_InvoiceLine_SubservicesWarehouseId");

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceLine_SubServicesWareHouse_SubservicesWarehouseId",
                table: "InvoiceLine",
                column: "SubservicesWarehouseId",
                principalTable: "SubServicesWareHouse",
                principalColumn: "SubServicesWareHouseId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
