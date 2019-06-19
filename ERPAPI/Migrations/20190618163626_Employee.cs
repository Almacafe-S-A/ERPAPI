using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class Employee : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Extension",
                table: "Employees",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaFinContrato",
                table: "Employees",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "IdDepartamento",
                table: "Employees",
                nullable: false,
                defaultValue: 0);

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

            migrationBuilder.AddColumn<string>(
                name: "NombreDepartamento",
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

            migrationBuilder.AddColumn<string>(
                name: "Notas",
                table: "Employees",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Telefono",
                table: "Employees",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "FormulasConcepto",
                columns: table => new
                {
                    IdformulaConcepto = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Idformula = table.Column<long>(nullable: true),
                    IdConcepto = table.Column<long>(nullable: true),
                    NombreConcepto = table.Column<string>(nullable: true),
                    FechaCreacion = table.Column<DateTime>(nullable: true),
                    FechaModificacion = table.Column<DateTime>(nullable: true),
                    UsuarioCreacion = table.Column<string>(nullable: true),
                    UsuarioModificacion = table.Column<string>(nullable: true),
                    EstructuraConcepto = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormulasConcepto", x => x.IdformulaConcepto);
                });

            migrationBuilder.CreateTable(
                name: "FormulasConFormulas",
                columns: table => new
                {
                    IdFormulaconformula = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IdFormula = table.Column<long>(nullable: true),
                    IdFormulachild = table.Column<long>(nullable: true),
                    NombreFormulachild = table.Column<string>(nullable: true),
                    EstructuraConcepto = table.Column<string>(nullable: true),
                    FechaCreacion = table.Column<DateTime>(nullable: true),
                    Fechamodificacion = table.Column<DateTime>(nullable: true),
                    UsuarioCreacion = table.Column<string>(nullable: true),
                    UsuarioModificacion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormulasConFormulas", x => x.IdFormulaconformula);
                });

            migrationBuilder.CreateTable(
                name: "Incapacidades",
                columns: table => new
                {
                    Idincapacidad = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FechaInicio = table.Column<DateTime>(nullable: true),
                    FechaFin = table.Column<DateTime>(nullable: true),
                    IdEmpleado = table.Column<long>(nullable: true),
                    DescripcionIncapacidad = table.Column<string>(nullable: true),
                    FechaCreacion = table.Column<DateTime>(nullable: true),
                    FechaModificacion = table.Column<DateTime>(nullable: true),
                    UsuarioCreacion = table.Column<string>(nullable: true),
                    UsuarioModificacion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Incapacidades", x => x.Idincapacidad);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FormulasConcepto");

            migrationBuilder.DropTable(
                name: "FormulasConFormulas");

            migrationBuilder.DropTable(
                name: "Incapacidades");

            migrationBuilder.DropColumn(
                name: "Extension",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "FechaFinContrato",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "IdDepartamento",
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
                name: "NombreDepartamento",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "NombrePuesto",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "NombreSucursal",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "Notas",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "Telefono",
                table: "Employees");
        }
    }
}
