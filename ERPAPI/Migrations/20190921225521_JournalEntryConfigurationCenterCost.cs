using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class JournalEntryConfigurationCenterCost : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "CenterCostId",
                table: "JournalEntryConfigurationLine",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "CenterCostName",
                table: "JournalEntryConfigurationLine",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "JournalEntryConfiguration",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CenterCostId",
                table: "JournalEntryConfigurationLine");

            migrationBuilder.DropColumn(
                name: "CenterCostName",
                table: "JournalEntryConfigurationLine");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "JournalEntryConfiguration");
        }
    }
}
