using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class VendorTypeForeignKey_Vendor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "VendorTypeId",
                table: "Vendor",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.CreateIndex(
                name: "IX_Vendor_VendorTypeId",
                table: "Vendor",
                column: "VendorTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vendor_VendorType_VendorTypeId",
                table: "Vendor",
                column: "VendorTypeId",
                principalTable: "VendorType",
                principalColumn: "VendorTypeId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vendor_VendorType_VendorTypeId",
                table: "Vendor");

            migrationBuilder.DropIndex(
                name: "IX_Vendor_VendorTypeId",
                table: "Vendor");

            migrationBuilder.AlterColumn<int>(
                name: "VendorTypeId",
                table: "Vendor",
                nullable: false,
                oldClrType: typeof(long));
        }
    }
}
