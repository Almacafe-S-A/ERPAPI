using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class InvoiceLineSubService : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "InvoiceLineId",
                table: "SubServicesWareHouse",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "UnitOfMeasureId",
                table: "InvoiceLine",
                nullable: false,
                oldClrType: typeof(long));

            migrationBuilder.AddColumn<long>(
                name: "SubservicesWarehouseId",
                table: "InvoiceLine",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SubServicesWareHouse_InvoiceLineId",
                table: "SubServicesWareHouse",
                column: "InvoiceLineId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceLine_SubservicesWarehouseId",
                table: "InvoiceLine",
                column: "SubservicesWarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceLine_UnitOfMeasureId",
                table: "InvoiceLine",
                column: "UnitOfMeasureId");

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceLine_SubServicesWareHouse_SubservicesWarehouseId",
                table: "InvoiceLine",
                column: "SubservicesWarehouseId",
                principalTable: "SubServicesWareHouse",
                principalColumn: "SubServicesWareHouseId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceLine_UnitOfMeasure_UnitOfMeasureId",
                table: "InvoiceLine",
                column: "UnitOfMeasureId",
                principalTable: "UnitOfMeasure",
                principalColumn: "UnitOfMeasureId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SubServicesWareHouse_InvoiceLine_InvoiceLineId",
                table: "SubServicesWareHouse",
                column: "InvoiceLineId",
                principalTable: "InvoiceLine",
                principalColumn: "InvoiceLineId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceLine_SubServicesWareHouse_SubservicesWarehouseId",
                table: "InvoiceLine");

            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceLine_UnitOfMeasure_UnitOfMeasureId",
                table: "InvoiceLine");

            migrationBuilder.DropForeignKey(
                name: "FK_SubServicesWareHouse_InvoiceLine_InvoiceLineId",
                table: "SubServicesWareHouse");

            migrationBuilder.DropIndex(
                name: "IX_SubServicesWareHouse_InvoiceLineId",
                table: "SubServicesWareHouse");

            migrationBuilder.DropIndex(
                name: "IX_InvoiceLine_SubservicesWarehouseId",
                table: "InvoiceLine");

            migrationBuilder.DropIndex(
                name: "IX_InvoiceLine_UnitOfMeasureId",
                table: "InvoiceLine");

            migrationBuilder.DropColumn(
                name: "InvoiceLineId",
                table: "SubServicesWareHouse");

            migrationBuilder.DropColumn(
                name: "SubservicesWarehouseId",
                table: "InvoiceLine");

            migrationBuilder.AlterColumn<long>(
                name: "UnitOfMeasureId",
                table: "InvoiceLine",
                nullable: false,
                oldClrType: typeof(int));
        }
    }
}
