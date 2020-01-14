using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class AgregadoCampoBeneficiario_JournalEntryLine : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PartyId",
                table: "JournalEntryLine",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PartyName",
                table: "JournalEntryLine",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PartyTypeId",
                table: "JournalEntryLine",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "PartyTypeName",
                table: "JournalEntryLine",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PartyId",
                table: "JournalEntryLine");

            migrationBuilder.DropColumn(
                name: "PartyName",
                table: "JournalEntryLine");

            migrationBuilder.DropColumn(
                name: "PartyTypeId",
                table: "JournalEntryLine");

            migrationBuilder.DropColumn(
                name: "PartyTypeName",
                table: "JournalEntryLine");
        }
    }
}
