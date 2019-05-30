using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class WareHouse_LinesInvoices : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "UnitOfMeasureId",
                table: "SalesOrderLine",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "UnitOfMeasureName",
                table: "SalesOrderLine",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "CenterCostId",
                table: "ProformaInvoiceLine",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "UnitOfMeasureId",
                table: "ProformaInvoiceLine",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "UnitOfMeasureName",
                table: "ProformaInvoiceLine",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "WareHouseId",
                table: "ProformaInvoiceLine",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "CenterCostId",
                table: "InvoiceLine",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "UnitOfMeasureId",
                table: "InvoiceLine",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "UnitOfMeasureName",
                table: "InvoiceLine",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "WareHouseId",
                table: "InvoiceLine",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "BranchId",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UnitOfMeasureId",
                table: "SalesOrderLine");

            migrationBuilder.DropColumn(
                name: "UnitOfMeasureName",
                table: "SalesOrderLine");

            migrationBuilder.DropColumn(
                name: "CenterCostId",
                table: "ProformaInvoiceLine");

            migrationBuilder.DropColumn(
                name: "UnitOfMeasureId",
                table: "ProformaInvoiceLine");

            migrationBuilder.DropColumn(
                name: "UnitOfMeasureName",
                table: "ProformaInvoiceLine");

            migrationBuilder.DropColumn(
                name: "WareHouseId",
                table: "ProformaInvoiceLine");

            migrationBuilder.DropColumn(
                name: "CenterCostId",
                table: "InvoiceLine");

            migrationBuilder.DropColumn(
                name: "UnitOfMeasureId",
                table: "InvoiceLine");

            migrationBuilder.DropColumn(
                name: "UnitOfMeasureName",
                table: "InvoiceLine");

            migrationBuilder.DropColumn(
                name: "WareHouseId",
                table: "InvoiceLine");

            migrationBuilder.DropColumn(
                name: "BranchId",
                table: "AspNetUsers");
        }
    }
}
