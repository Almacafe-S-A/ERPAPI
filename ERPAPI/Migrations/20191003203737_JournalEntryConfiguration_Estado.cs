using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class JournalEntryConfiguration_Estado : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "EstadoId",
                table: "JournalEntryConfiguration",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "EstadoName",
                table: "JournalEntryConfiguration",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EstadoId",
                table: "JournalEntryConfiguration");

            migrationBuilder.DropColumn(
                name: "EstadoName",
                table: "JournalEntryConfiguration");
        }
    }
}
