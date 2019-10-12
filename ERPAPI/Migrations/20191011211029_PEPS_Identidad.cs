using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class PEPS_Identidad : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Identidad",
                table: "PEPS",
                nullable: true);

            //migrationBuilder.AlterColumn<string>(
            //    name: "EstadoName",
            //    table: "JournalEntry",
            //    nullable: true,
            //    oldClrType: typeof(int));

            //migrationBuilder.AlterColumn<long>(
            //    name: "EstadoId",
            //    table: "JournalEntry",
            //    nullable: false,
            //    oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Identidad",
                table: "PEPS");

            migrationBuilder.AlterColumn<int>(
                name: "EstadoName",
                table: "JournalEntry",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "EstadoId",
                table: "JournalEntry",
                nullable: false,
                oldClrType: typeof(long));
        }
    }
}
