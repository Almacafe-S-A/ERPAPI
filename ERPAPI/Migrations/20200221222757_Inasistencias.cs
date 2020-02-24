using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class Inasistencias : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Inasistencias",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Fecha = table.Column<DateTime>(nullable: false),
                    IdEmpleado = table.Column<long>(nullable: false),
                    Observacion = table.Column<string>(nullable: true),
                    TipoInasistencia = table.Column<long>(nullable: false),
                    IdEstado = table.Column<long>(nullable: false),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false),
                    UsuarioModificacion = table.Column<string>(nullable: true),
                    UsuarioCreacion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inasistencias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Inasistencias_Employees_IdEmpleado",
                        column: x => x.IdEmpleado,
                        principalTable: "Employees",
                        principalColumn: "IdEmpleado",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Inasistencias_Estados_IdEstado",
                        column: x => x.IdEstado,
                        principalTable: "Estados",
                        principalColumn: "IdEstado",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Inasistencias_IdEmpleado",
                table: "Inasistencias",
                column: "IdEmpleado");

            migrationBuilder.CreateIndex(
                name: "IX_Inasistencias_IdEstado",
                table: "Inasistencias",
                column: "IdEstado");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Inasistencias");
        }
    }
}
