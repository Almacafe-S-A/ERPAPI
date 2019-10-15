using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class permitirnulosJournalEntry : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "EstadoId",
                table: "JournalEntry",
                nullable: true,
                oldClrType: typeof(long));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "EstadoId",
                table: "JournalEntry",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);
        }
    }
}
