using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class typebusinesspartner_journalentry : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PartyTypeId",
                table: "JournalEntry",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PartyTypeName",
                table: "JournalEntry",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PartyTypeId",
                table: "JournalEntry");

            migrationBuilder.DropColumn(
                name: "PartyTypeName",
                table: "JournalEntry");
        }
    }
}
