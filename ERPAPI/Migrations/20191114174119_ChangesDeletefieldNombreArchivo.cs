using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class ChangesDeletefieldNombreArchivo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "NombreArchivo",
                table: "Conciliacion",
                nullable: true,
                oldClrType: typeof(string));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "NombreArchivo",
                table: "Conciliacion",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
