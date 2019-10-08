using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class journalEntry_PartyTypeName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PartyTypeName",
                table: "JournalEntry",
                nullable: true,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "PartyTypeName",
                table: "JournalEntry",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
