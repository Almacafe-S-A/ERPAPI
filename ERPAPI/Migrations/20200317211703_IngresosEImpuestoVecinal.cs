using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class IngresosEImpuestoVecinal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ImpuestoVecinalConfiguraciones",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    De = table.Column<decimal>(nullable: false),
                    Hasta = table.Column<decimal>(nullable: false),
                    FactorMillar = table.Column<decimal>(nullable: false),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false),
                    UsuarioModificacion = table.Column<string>(nullable: false),
                    UsuarioCreacion = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImpuestoVecinalConfiguraciones", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IngresosAnuales",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Periodo = table.Column<int>(nullable: false),
                    EmpleadoId = table.Column<long>(nullable: false),
                    IngresoAcumulado = table.Column<decimal>(nullable: false),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false),
                    UsuarioModificacion = table.Column<string>(nullable: false),
                    UsuarioCreacion = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IngresosAnuales", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IngresosAnuales_Employees_EmpleadoId",
                        column: x => x.EmpleadoId,
                        principalTable: "Employees",
                        principalColumn: "IdEmpleado",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_IngresosAnuales_EmpleadoId",
                table: "IngresosAnuales",
                column: "EmpleadoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ImpuestoVecinalConfiguraciones");

            migrationBuilder.DropTable(
                name: "IngresosAnuales");
        }
    }
}
