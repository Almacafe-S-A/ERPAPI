using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class RTNGErenteGeneral_Cliente : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Identidad",
                table: "Customer",
                newName: "RTNGerenteGeneral");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RTNGerenteGeneral",
                table: "Customer",
                newName: "Identidad");
        }
    }
}
