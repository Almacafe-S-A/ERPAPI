using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class ChangeNameFieldEstiba_Kardex : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ControlEstibaId",
                table: "Kardex",
                newName: "Estiba");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Estiba",
                table: "Kardex",
                newName: "ControlEstibaId");
        }
    }
}
