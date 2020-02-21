using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class HorariosEmpleado : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EmpleadoHorarios",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EmpleadoId = table.Column<long>(nullable: false),
                    HorarioId = table.Column<long>(nullable: false),
                    IdEstado = table.Column<long>(nullable: false),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false),
                    UsuarioModificacion = table.Column<string>(nullable: true),
                    UsuarioCreacion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmpleadoHorarios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmpleadoHorarios_Employees_EmpleadoId",
                        column: x => x.EmpleadoId,
                        principalTable: "Employees",
                        principalColumn: "IdEmpleado",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmpleadoHorarios_Horarios_HorarioId",
                        column: x => x.HorarioId,
                        principalTable: "Horarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmpleadoHorarios_Estados_IdEstado",
                        column: x => x.IdEstado,
                        principalTable: "Estados",
                        principalColumn: "IdEstado",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmpleadoHorarios_EmpleadoId",
                table: "EmpleadoHorarios",
                column: "EmpleadoId");

            migrationBuilder.CreateIndex(
                name: "IX_EmpleadoHorarios_HorarioId",
                table: "EmpleadoHorarios",
                column: "HorarioId");

            migrationBuilder.CreateIndex(
                name: "IX_EmpleadoHorarios_IdEstado",
                table: "EmpleadoHorarios",
                column: "IdEstado");

            migrationBuilder.CreateIndex(
                name: "IX_EmpleadoHorarios_EmpleadoYHorario",
                table:"EmpleadoHorarios",
                columns:new [] {"EmpleadoId","HorarioId"},
                unique:true
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmpleadoHorarios");
        }
    }
}
