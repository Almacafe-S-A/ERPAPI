using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class Listados_Socios_FirmasAutorizadas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Listados",
                table: "CustomerPartners",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Listados",
                table: "CustomerAuthorizedSignature",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Listados",
                table: "CustomerPartners");

            migrationBuilder.DropColumn(
                name: "Listados",
                table: "CustomerAuthorizedSignature");
        }
    }
}
