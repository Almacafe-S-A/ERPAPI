using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class JournalEntryConfiguration_RenameBranchId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CostCenterName",
                table: "JournalEntryConfiguration",
                newName: "BranchName");

            migrationBuilder.RenameColumn(
                name: "CostCenterId",
                table: "JournalEntryConfiguration",
                newName: "BranchId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "BranchName",
                table: "JournalEntryConfiguration",
                newName: "CostCenterName");

            migrationBuilder.RenameColumn(
                name: "BranchId",
                table: "JournalEntryConfiguration",
                newName: "CostCenterId");
        }
    }
}
