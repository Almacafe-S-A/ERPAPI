using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class VendorInvoiceFieldsAccounting : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VendorInvoice_Accounting_AccountId",
                table: "VendorInvoice");

            migrationBuilder.DropIndex(
                name: "IX_VendorInvoice_AccountId",
                table: "VendorInvoice");

            migrationBuilder.DropColumn(
                name: "AccountId",
                table: "VendorInvoice");

            migrationBuilder.AddColumn<long>(
                name: "AccountIdCredito",
                table: "VendorInvoice",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "AccountIdGasto",
                table: "VendorInvoice",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AccountNameCredito",
                table: "VendorInvoice",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AccountNameGasto",
                table: "VendorInvoice",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "CostCenterId",
                table: "VendorInvoice",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CostCenterName",
                table: "VendorInvoice",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_VendorInvoice_AccountIdCredito",
                table: "VendorInvoice",
                column: "AccountIdCredito");

            migrationBuilder.CreateIndex(
                name: "IX_VendorInvoice_AccountIdGasto",
                table: "VendorInvoice",
                column: "AccountIdGasto");

            migrationBuilder.CreateIndex(
                name: "IX_VendorInvoice_CostCenterId",
                table: "VendorInvoice",
                column: "CostCenterId");

            migrationBuilder.AddForeignKey(
                name: "FK_VendorInvoice_Accounting_AccountIdCredito",
                table: "VendorInvoice",
                column: "AccountIdCredito",
                principalTable: "Accounting",
                principalColumn: "AccountId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VendorInvoice_Accounting_AccountIdGasto",
                table: "VendorInvoice",
                column: "AccountIdGasto",
                principalTable: "Accounting",
                principalColumn: "AccountId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VendorInvoice_CostCenter_CostCenterId",
                table: "VendorInvoice",
                column: "CostCenterId",
                principalTable: "CostCenter",
                principalColumn: "CostCenterId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VendorInvoice_Accounting_AccountIdCredito",
                table: "VendorInvoice");

            migrationBuilder.DropForeignKey(
                name: "FK_VendorInvoice_Accounting_AccountIdGasto",
                table: "VendorInvoice");

            migrationBuilder.DropForeignKey(
                name: "FK_VendorInvoice_CostCenter_CostCenterId",
                table: "VendorInvoice");

            migrationBuilder.DropIndex(
                name: "IX_VendorInvoice_AccountIdCredito",
                table: "VendorInvoice");

            migrationBuilder.DropIndex(
                name: "IX_VendorInvoice_AccountIdGasto",
                table: "VendorInvoice");

            migrationBuilder.DropIndex(
                name: "IX_VendorInvoice_CostCenterId",
                table: "VendorInvoice");

            migrationBuilder.DropColumn(
                name: "AccountIdCredito",
                table: "VendorInvoice");

            migrationBuilder.DropColumn(
                name: "AccountIdGasto",
                table: "VendorInvoice");

            migrationBuilder.DropColumn(
                name: "AccountNameCredito",
                table: "VendorInvoice");

            migrationBuilder.DropColumn(
                name: "AccountNameGasto",
                table: "VendorInvoice");

            migrationBuilder.DropColumn(
                name: "CostCenterId",
                table: "VendorInvoice");

            migrationBuilder.DropColumn(
                name: "CostCenterName",
                table: "VendorInvoice");

            migrationBuilder.AddColumn<long>(
                name: "AccountId",
                table: "VendorInvoice",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_VendorInvoice_AccountId",
                table: "VendorInvoice",
                column: "AccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_VendorInvoice_Accounting_AccountId",
                table: "VendorInvoice",
                column: "AccountId",
                principalTable: "Accounting",
                principalColumn: "AccountId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
