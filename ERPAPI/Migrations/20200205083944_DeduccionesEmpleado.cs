using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class DeduccionesEmpleado : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.CreateTable(
                name: "DeduccionesEmpleados",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EmpleadoId = table.Column<long>(nullable: false),
                    DeductionId = table.Column<long>(nullable: false),
                    Monto = table.Column<float>(nullable: false),
                    VigenciaInicio = table.Column<DateTime>(nullable: false),
                    VigenciaFinaliza = table.Column<DateTime>(nullable: false),
                    EstadoId = table.Column<int>(nullable: false),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false),
                    UsuarioModificacion = table.Column<string>(nullable: true),
                    UsuarioCreacion = table.Column<string>(nullable: true),
                    EmpeladoId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeduccionesEmpleados", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeduccionesEmpleados_Deduction_DeductionId",
                        column: x => x.DeductionId,
                        principalTable: "Deduction",
                        principalColumn: "DeductionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DeduccionesEmpleados_Employees_EmpeladoId",
                        column: x => x.EmpeladoId,
                        principalTable: "Employees",
                        principalColumn: "IdEmpleado",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DeduccionesEmpleados_DeductionId",
                table: "DeduccionesEmpleados",
                column: "DeductionId");

            migrationBuilder.CreateIndex(
                name: "IX_DeduccionesEmpleados_EmpeladoId",
                table: "DeduccionesEmpleados",
                column: "EmpeladoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DeduccionesEmpleados");
        }

    }
}
