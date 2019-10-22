using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class Add_field_Account_to_VendorInvoice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        protected override void Down(MigrationBuilder migrationBuilder)
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
        }
    }
}
