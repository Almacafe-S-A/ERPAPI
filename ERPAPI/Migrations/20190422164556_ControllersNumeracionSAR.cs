using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class ControllersNumeracionSAR : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IdCAI",
                table: "NumeracionSAR",
                newName: "IdNumeracion");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IdNumeracion",
                table: "NumeracionSAR",
                newName: "IdCAI");
        }
    }
}
