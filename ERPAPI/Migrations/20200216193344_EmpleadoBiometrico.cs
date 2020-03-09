using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class EmpleadoBiometrico : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EmpleadosBiometrico",
                columns: table => new
                {
                    EmpleadoId = table.Column<long>(nullable: false),
                    BiometricoId = table.Column<long>(nullable: false),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false),
                    UsuarioModificacion = table.Column<string>(nullable: true),
                    UsuarioCreacion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmpleadosBiometrico", x => x.EmpleadoId);
                    table.ForeignKey(
                        name: "FK_EmpleadosBiometrico_Employees_EmpleadoId",
                        column: x => x.EmpleadoId,
                        principalTable: "Employees",
                        principalColumn: "IdEmpleado",
                        onDelete: ReferentialAction.Restrict);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmpleadosBiometrico");
        }
    }
}
