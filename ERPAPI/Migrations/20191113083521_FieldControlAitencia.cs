using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class FieldControlAitencia : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "TipoAsistencia",
                table: "ControlAsistencias",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "TipoAsistencia",
                table: "ControlAsistencias",
                nullable: true,
                oldClrType: typeof(long),
                oldNullable: true);
        }
    }
}
