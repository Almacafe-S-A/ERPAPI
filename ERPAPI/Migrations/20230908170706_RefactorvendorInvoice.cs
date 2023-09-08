using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class RefactorvendorInvoice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VendorInvoice_PurchaseOrder_PurchaseOrderId",
                table: "VendorInvoice");

            migrationBuilder.DropTable(
                name: "VendorInvoiceLine");

            migrationBuilder.DropIndex(
                name: "IX_VendorInvoice_PurchaseOrderId",
                table: "VendorInvoice");

            migrationBuilder.DropColumn(
                name: "Amount",
                table: "VendorInvoice");

            migrationBuilder.DropColumn(
                name: "Correo",
                table: "VendorInvoice");

            migrationBuilder.DropColumn(
                name: "Currency",
                table: "VendorInvoice");

            migrationBuilder.DropColumn(
                name: "CurrencyId",
                table: "VendorInvoice");

            migrationBuilder.DropColumn(
                name: "CurrencyName",
                table: "VendorInvoice");

            migrationBuilder.DropColumn(
                name: "Direccion",
                table: "VendorInvoice");

            migrationBuilder.DropColumn(
                name: "Discount",
                table: "VendorInvoice");

            migrationBuilder.DropColumn(
                name: "Freight",
                table: "VendorInvoice");

            migrationBuilder.DropColumn(
                name: "OrderDate",
                table: "VendorInvoice");

            migrationBuilder.DropColumn(
                name: "PurchaseOrderId",
                table: "VendorInvoice");

            migrationBuilder.DropColumn(
                name: "RTN",
                table: "VendorInvoice");

            migrationBuilder.DropColumn(
                name: "ReceivedDate",
                table: "VendorInvoice");

            migrationBuilder.DropColumn(
                name: "ShipmentId",
                table: "VendorInvoice");

            migrationBuilder.DropColumn(
                name: "SubTotal",
                table: "VendorInvoice");

            migrationBuilder.DropColumn(
                name: "Tax18",
                table: "VendorInvoice");

            migrationBuilder.DropColumn(
                name: "Tefono",
                table: "VendorInvoice");

            migrationBuilder.DropColumn(
                name: "TotalExonerado",
                table: "VendorInvoice");

            migrationBuilder.DropColumn(
                name: "TotalGravado18",
                table: "VendorInvoice");

            migrationBuilder.RenameColumn(
                name: "VendorRefNumber",
                table: "VendorInvoice",
                newName: "VendorRTN");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "VendorRTN",
                table: "VendorInvoice",
                newName: "VendorRefNumber");

            migrationBuilder.AddColumn<decimal>(
                name: "Amount",
                table: "VendorInvoice",
                type: "Money",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "Correo",
                table: "VendorInvoice",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Currency",
                table: "VendorInvoice",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "CurrencyId",
                table: "VendorInvoice",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "CurrencyName",
                table: "VendorInvoice",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Direccion",
                table: "VendorInvoice",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Discount",
                table: "VendorInvoice",
                type: "Money",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Freight",
                table: "VendorInvoice",
                type: "Money",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<DateTime>(
                name: "OrderDate",
                table: "VendorInvoice",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "PurchaseOrderId",
                table: "VendorInvoice",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RTN",
                table: "VendorInvoice",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ReceivedDate",
                table: "VendorInvoice",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "ShipmentId",
                table: "VendorInvoice",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "SubTotal",
                table: "VendorInvoice",
                type: "Money",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Tax18",
                table: "VendorInvoice",
                type: "Money",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "Tefono",
                table: "VendorInvoice",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalExonerado",
                table: "VendorInvoice",
                type: "Money",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalGravado18",
                table: "VendorInvoice",
                type: "Money",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateTable(
                name: "VendorInvoiceLine",
                columns: table => new
                {
                    VendorInvoiceLineId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AccountId = table.Column<long>(nullable: false),
                    Amount = table.Column<decimal>(nullable: false),
                    CostCenterId = table.Column<long>(nullable: true),
                    CostCenterName = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    ItemName = table.Column<string>(nullable: true),
                    TaxAmount = table.Column<decimal>(nullable: false),
                    TaxCode = table.Column<string>(nullable: true),
                    TaxId = table.Column<long>(nullable: true),
                    TaxPercentage = table.Column<decimal>(nullable: false),
                    Total = table.Column<decimal>(nullable: false),
                    VendorInvoiceId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VendorInvoiceLine", x => x.VendorInvoiceLineId);
                    table.ForeignKey(
                        name: "FK_VendorInvoiceLine_Accounting_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounting",
                        principalColumn: "AccountId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VendorInvoiceLine_CostCenter_CostCenterId",
                        column: x => x.CostCenterId,
                        principalTable: "CostCenter",
                        principalColumn: "CostCenterId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VendorInvoiceLine_Tax_TaxId",
                        column: x => x.TaxId,
                        principalTable: "Tax",
                        principalColumn: "TaxId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VendorInvoiceLine_VendorInvoice_VendorInvoiceId",
                        column: x => x.VendorInvoiceId,
                        principalTable: "VendorInvoice",
                        principalColumn: "VendorInvoiceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VendorInvoice_PurchaseOrderId",
                table: "VendorInvoice",
                column: "PurchaseOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_VendorInvoiceLine_AccountId",
                table: "VendorInvoiceLine",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_VendorInvoiceLine_CostCenterId",
                table: "VendorInvoiceLine",
                column: "CostCenterId");

            migrationBuilder.CreateIndex(
                name: "IX_VendorInvoiceLine_TaxId",
                table: "VendorInvoiceLine",
                column: "TaxId");

            migrationBuilder.CreateIndex(
                name: "IX_VendorInvoiceLine_VendorInvoiceId",
                table: "VendorInvoiceLine",
                column: "VendorInvoiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_VendorInvoice_PurchaseOrder_PurchaseOrderId",
                table: "VendorInvoice",
                column: "PurchaseOrderId",
                principalTable: "PurchaseOrder",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
