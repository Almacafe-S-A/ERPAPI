using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class ClienteNatural : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "MontoActivos",
                table: "Customer",
                nullable: true,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<string>(
                name: "Identidad",
                table: "Customer",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<bool>(
                name: "CargosPublicos",
                table: "Customer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Conyugue",
                table: "Customer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Edad",
                table: "Customer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EstadoCivil",
                table: "Customer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Familiares",
                table: "Customer",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaNacimiento",
                table: "Customer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FirmaAuditoriaExterna",
                table: "Customer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GiroActividadNegocio",
                table: "Customer",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "InstitucionSupervisada",
                table: "Customer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LugarNacimiento",
                table: "Customer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Nacionalidad",
                table: "Customer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NombreFuncionario",
                table: "Customer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProfesionOficio",
                table: "Customer",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CargosPublicos",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "Conyugue",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "Edad",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "EstadoCivil",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "Familiares",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "FechaNacimiento",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "FirmaAuditoriaExterna",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "GiroActividadNegocio",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "InstitucionSupervisada",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "LugarNacimiento",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "Nacionalidad",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "NombreFuncionario",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "ProfesionOficio",
                table: "Customer");

            migrationBuilder.AlterColumn<double>(
                name: "MontoActivos",
                table: "Customer",
                nullable: false,
                oldClrType: typeof(double),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Identidad",
                table: "Customer",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
