using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class JournalEntryConfiguration_Products : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "SubProductId",
                table: "JournalEntryConfigurationLine",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "SubProductName",
                table: "JournalEntryConfigurationLine",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "CostCenterId",
                table: "JournalEntryConfiguration",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "CostCenterName",
                table: "JournalEntryConfiguration",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ProductId",
                table: "JournalEntryConfiguration",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "ProductName",
                table: "JournalEntryConfiguration",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SubProductId",
                table: "JournalEntryConfigurationLine");

            migrationBuilder.DropColumn(
                name: "SubProductName",
                table: "JournalEntryConfigurationLine");

            migrationBuilder.DropColumn(
                name: "CostCenterId",
                table: "JournalEntryConfiguration");

            migrationBuilder.DropColumn(
                name: "CostCenterName",
                table: "JournalEntryConfiguration");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "JournalEntryConfiguration");

            migrationBuilder.DropColumn(
                name: "ProductName",
                table: "JournalEntryConfiguration");
        }
    }
}
