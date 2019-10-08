using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class coscentername_journaEntry : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "CostCenterName",
                table: "JournalEntryLine",
                nullable: true,
                oldClrType: typeof(long));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "CostCenterName",
                table: "JournalEntryLine",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
