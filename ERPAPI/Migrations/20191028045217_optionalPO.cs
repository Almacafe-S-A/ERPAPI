using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class optionalPO : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VendorInvoice_PurchaseOrder_PurchaseOrderId",
                table: "VendorInvoice");

            migrationBuilder.AlterColumn<int>(
                name: "PurchaseOrderId",
                table: "VendorInvoice",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_VendorInvoice_PurchaseOrder_PurchaseOrderId",
                table: "VendorInvoice",
                column: "PurchaseOrderId",
                principalTable: "PurchaseOrder",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VendorInvoice_PurchaseOrder_PurchaseOrderId",
                table: "VendorInvoice");

            migrationBuilder.AlterColumn<int>(
                name: "PurchaseOrderId",
                table: "VendorInvoice",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_VendorInvoice_PurchaseOrder_PurchaseOrderId",
                table: "VendorInvoice",
                column: "PurchaseOrderId",
                principalTable: "PurchaseOrder",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
