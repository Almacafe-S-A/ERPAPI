using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class RemoveFieldsEmployee : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ciudad",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "Estado",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "IdBanco",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "IdCiudad",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "IdDepartamento",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "IdEstado",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "IdPais",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "IdPuesto",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "IdSucursal",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "IdTipoContrato",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "Idsescalas",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "MonedaSalario",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "NombreCiudad",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "NombreDepartamento",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "NombreEstado",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "NombrePais",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "NombrePuesto",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "NombreSucursal",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "Puesto",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "Userid",
                table: "Employees");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Ciudad",
                table: "Employees",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Estado",
                table: "Employees",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "IdBanco",
                table: "Employees",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "IdCiudad",
                table: "Employees",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IdDepartamento",
                table: "Employees",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<long>(
                name: "IdEstado",
                table: "Employees",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "IdPais",
                table: "Employees",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IdPuesto",
                table: "Employees",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IdSucursal",
                table: "Employees",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IdTipoContrato",
                table: "Employees",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<long>(
                name: "Idsescalas",
                table: "Employees",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "MonedaSalario",
                table: "Employees",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NombreCiudad",
                table: "Employees",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NombreDepartamento",
                table: "Employees",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NombreEstado",
                table: "Employees",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NombrePais",
                table: "Employees",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NombrePuesto",
                table: "Employees",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NombreSucursal",
                table: "Employees",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "Puesto",
                table: "Employees",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Userid",
                table: "Employees",
                nullable: true);
        }
    }
}
