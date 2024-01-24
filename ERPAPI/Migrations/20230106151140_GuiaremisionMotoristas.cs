using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class GuiaremisionMotoristas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Montorista",
                table: "GuiaRemision",
                newName: "Motorista");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Motorista",
                table: "GuiaRemision",
                newName: "Montorista");
        }
    }
}
