/********************************************************************************************************
-- NAME   :  CRUDPurchaseOrderLine
-- PROPOSE:  show record PurchaseOrderLine
REVISIONS:
version              Date                Author                        Description
----------           -------------       ---------------               -------------------------------
1.0                  09/12/2019          Marvin.Guillen                 Changes of Add fields TaxPercentage, TaxAmount ,DiscountAmount, DiscountPercentage,  SubTotal, Total, Amount
********************************************************************************************************/


using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class AddFieldsPurchaseOrderLine : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Amount",
                table: "PurchaseOrderLine",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "DiscountAmount",
                table: "PurchaseOrderLine",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DiscountPercentage",
                table: "PurchaseOrderLine",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "SubTotal",
                table: "PurchaseOrderLine",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "TaxAmount",
                table: "PurchaseOrderLine",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "TaxPercentage",
                table: "PurchaseOrderLine",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Total",
                table: "PurchaseOrderLine",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Amount",
                table: "PurchaseOrderLine");

            migrationBuilder.DropColumn(
                name: "DiscountAmount",
                table: "PurchaseOrderLine");

            migrationBuilder.DropColumn(
                name: "DiscountPercentage",
                table: "PurchaseOrderLine");

            migrationBuilder.DropColumn(
                name: "SubTotal",
                table: "PurchaseOrderLine");

            migrationBuilder.DropColumn(
                name: "TaxAmount",
                table: "PurchaseOrderLine");

            migrationBuilder.DropColumn(
                name: "TaxPercentage",
                table: "PurchaseOrderLine");

            migrationBuilder.DropColumn(
                name: "Total",
                table: "PurchaseOrderLine");
        }
    }
}
