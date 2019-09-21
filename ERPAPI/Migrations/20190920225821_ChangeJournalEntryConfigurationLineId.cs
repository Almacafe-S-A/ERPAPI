using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class ChangeJournalEntryConfigurationLineId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "JournalEntryConfigurationId",
                table: "JournalEntryConfigurationLine",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "JournalEntryConfigurationId",
                table: "JournalEntryConfigurationLine",
                nullable: true,
                oldClrType: typeof(long));
        }
    }
}
