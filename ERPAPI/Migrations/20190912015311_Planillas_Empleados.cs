using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class Planillas_Empleados : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdPlanilla",
                table: "Employees",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "NombreContacto",
                table: "Employees",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TelefonoContacto",
                table: "Employees",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TipoSangre",
                table: "Employees",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Planilla",
                columns: table => new
                {
                    IdPlanilla = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TipoPlanilla = table.Column<string>(nullable: true),
                    Descripcion = table.Column<string>(nullable: true),
                    EstadoId = table.Column<long>(nullable: false),
                    Usuariocreacion = table.Column<string>(nullable: true),
                    Usuariomodificacion = table.Column<string>(nullable: true),
                    FechaCreacion = table.Column<DateTime>(nullable: true),
                    FechaModificacion = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Planilla", x => x.IdPlanilla);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Planilla");

            migrationBuilder.DropColumn(
                name: "IdPlanilla",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "NombreContacto",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "TelefonoContacto",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "TipoSangre",
                table: "Employees");
        }
    }
}
