using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class RetentionReceipt_CAIBranch : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BranchCode",
                table: "RetentionReceipt",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VendorCAI",
                table: "RetentionReceipt",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BranchCode",
                table: "RetentionReceipt");

            migrationBuilder.DropColumn(
                name: "VendorCAI",
                table: "RetentionReceipt");
        }
    }
}
