using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class clientes_complementfields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ClienteRecoger",
                table: "Customer",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "ConfirmacionCorreo",
                table: "Customer",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "DireccionEnvio",
                table: "Customer",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "EnviarlaMensajero",
                table: "Customer",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "GrupoEconomico",
                table: "Customer",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "MontoActivos",
                table: "Customer",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "MontoIngresosAnuales",
                table: "Customer",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "PerteneceEmpresa",
                table: "Customer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Proveedor1",
                table: "Customer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Proveedor2",
                table: "Customer",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClienteRecoger",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "ConfirmacionCorreo",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "DireccionEnvio",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "EnviarlaMensajero",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "GrupoEconomico",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "MontoActivos",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "MontoIngresosAnuales",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "PerteneceEmpresa",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "Proveedor1",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "Proveedor2",
                table: "Customer");
        }
    }
}
