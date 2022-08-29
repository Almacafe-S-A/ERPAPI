using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class CamposEmpresaClientePersonaNatural : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DireccionEmpresaPN",
                table: "Customer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NombreEmpresaPN",
                table: "Customer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TelefonoEmpresaPN",
                table: "Customer",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DireccionEmpresaPN",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "NombreEmpresaPN",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "TelefonoEmpresaPN",
                table: "Customer");
        }
    }
}
