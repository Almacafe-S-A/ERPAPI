using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class PlanillasYBonificaciones : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Usuariomodificacion",
                table: "TipoPlanillas",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Usuariocreacion",
                table: "TipoPlanillas",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "TipoPlanilla",
                table: "TipoPlanillas",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Descripcion",
                table: "TipoPlanillas",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Bonificaciones",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EmpleadoId = table.Column<long>(nullable: false),
                    TipoId = table.Column<long>(nullable: false),
                    Monto = table.Column<double>(nullable: false),
                    FechaBono = table.Column<DateTime>(nullable: false),
                    EstadoId = table.Column<long>(nullable: false),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false),
                    UsuarioModificacion = table.Column<string>(nullable: false),
                    UsuarioCreacion = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bonificaciones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bonificaciones_Employees_EmpleadoId",
                        column: x => x.EmpleadoId,
                        principalTable: "Employees",
                        principalColumn: "IdEmpleado",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Bonificaciones_Estados_EstadoId",
                        column: x => x.EstadoId,
                        principalTable: "Estados",
                        principalColumn: "IdEstado",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ConfiguracionVacaciones",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Antiguedad = table.Column<int>(nullable: false),
                    DiasVacaciones = table.Column<int>(nullable: false),
                    EstadoId = table.Column<long>(nullable: false),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false),
                    UsuarioModificacion = table.Column<string>(nullable: false),
                    UsuarioCreacion = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConfiguracionVacaciones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConfiguracionVacaciones_Estados_EstadoId",
                        column: x => x.EstadoId,
                        principalTable: "Estados",
                        principalColumn: "IdEstado",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PagosISR",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Periodo = table.Column<long>(nullable: false),
                    EmpleadoId = table.Column<long>(nullable: false),
                    TotalAnual = table.Column<double>(nullable: false),
                    PagoAcumulado = table.Column<double>(nullable: false),
                    Saldo = table.Column<double>(nullable: false),
                    EstadoId = table.Column<long>(nullable: false),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false),
                    UsuarioModificacion = table.Column<string>(nullable: false),
                    UsuarioCreacion = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PagosISR", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PagosISR_Employees_EmpleadoId",
                        column: x => x.EmpleadoId,
                        principalTable: "Employees",
                        principalColumn: "IdEmpleado",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PagosISR_Estados_EstadoId",
                        column: x => x.EstadoId,
                        principalTable: "Estados",
                        principalColumn: "IdEstado",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Planillas",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TipoPlanillaId = table.Column<long>(nullable: false),
                    Nombre = table.Column<string>(nullable: false),
                    Periodo = table.Column<int>(nullable: false),
                    Mes = table.Column<int>(nullable: false),
                    TotalEmpleados = table.Column<int>(nullable: false),
                    TotalPlanilla = table.Column<double>(nullable: false),
                    EstadoId = table.Column<long>(nullable: false),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false),
                    UsuarioModificacion = table.Column<string>(nullable: false),
                    UsuarioCreacion = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Planillas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Planillas_Estados_EstadoId",
                        column: x => x.EstadoId,
                        principalTable: "Estados",
                        principalColumn: "IdEstado",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Planillas_TipoPlanillas_TipoPlanillaId",
                        column: x => x.TipoPlanillaId,
                        principalTable: "TipoPlanillas",
                        principalColumn: "IdTipoPlanilla",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TiposBonificaciones",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(nullable: false),
                    EstadoId = table.Column<long>(nullable: false),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false),
                    UsuarioModificacion = table.Column<string>(nullable: false),
                    UsuarioCreacion = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TiposBonificaciones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TiposBonificaciones_Estados_EstadoId",
                        column: x => x.EstadoId,
                        principalTable: "Estados",
                        principalColumn: "IdEstado",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DetallePlanillas",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PlanillaId = table.Column<long>(nullable: false),
                    EmpleadoId = table.Column<long>(nullable: false),
                    MontoBruto = table.Column<double>(nullable: false),
                    TotalDeducciones = table.Column<double>(nullable: false),
                    MontoNeto = table.Column<double>(nullable: false),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false),
                    UsuarioModificacion = table.Column<string>(nullable: false),
                    UsuarioCreacion = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetallePlanillas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DetallePlanillas_Planillas_PlanillaId",
                        column: x => x.PlanillaId,
                        principalTable: "Planillas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DeduccionesPlanilla",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DetallePlanillaId = table.Column<long>(nullable: false),
                    DeduccionId = table.Column<long>(nullable: false),
                    NombreDeduccion = table.Column<string>(nullable: false),
                    Monto = table.Column<float>(nullable: false),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false),
                    UsuarioModificacion = table.Column<string>(nullable: false),
                    UsuarioCreacion = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeduccionesPlanilla", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeduccionesPlanilla_DetallePlanillas_DetallePlanillaId",
                        column: x => x.DetallePlanillaId,
                        principalTable: "DetallePlanillas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Inasistencias_TipoInasistencia",
                table: "Inasistencias",
                column: "TipoInasistencia");

            migrationBuilder.CreateIndex(
                name: "IX_Bonificaciones_EmpleadoId",
                table: "Bonificaciones",
                column: "EmpleadoId");

            migrationBuilder.CreateIndex(
                name: "IX_Bonificaciones_EstadoId",
                table: "Bonificaciones",
                column: "EstadoId");

            migrationBuilder.CreateIndex(
                name: "IX_ConfiguracionVacaciones_EstadoId",
                table: "ConfiguracionVacaciones",
                column: "EstadoId");

            migrationBuilder.CreateIndex(
                name: "IX_DeduccionesPlanilla_DetallePlanillaId",
                table: "DeduccionesPlanilla",
                column: "DetallePlanillaId");

            migrationBuilder.CreateIndex(
                name: "IX_DetallePlanillas_PlanillaId",
                table: "DetallePlanillas",
                column: "PlanillaId");

            migrationBuilder.CreateIndex(
                name: "IX_PagosISR_EmpleadoId",
                table: "PagosISR",
                column: "EmpleadoId");

            migrationBuilder.CreateIndex(
                name: "IX_PagosISR_EstadoId",
                table: "PagosISR",
                column: "EstadoId");

            migrationBuilder.CreateIndex(
                name: "IX_Planillas_EstadoId",
                table: "Planillas",
                column: "EstadoId");

            migrationBuilder.CreateIndex(
                name: "IX_Planillas_TipoPlanillaId",
                table: "Planillas",
                column: "TipoPlanillaId");

            migrationBuilder.CreateIndex(
                name: "IX_TiposBonificaciones_EstadoId",
                table: "TiposBonificaciones",
                column: "EstadoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Inasistencias_ElementoConfiguracion_TipoInasistencia",
                table: "Inasistencias",
                column: "TipoInasistencia",
                principalTable: "ElementoConfiguracion",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Inasistencias_ElementoConfiguracion_TipoInasistencia",
                table: "Inasistencias");

            migrationBuilder.DropTable(
                name: "Bonificaciones");

            migrationBuilder.DropTable(
                name: "ConfiguracionVacaciones");

            migrationBuilder.DropTable(
                name: "DeduccionesPlanilla");

            migrationBuilder.DropTable(
                name: "PagosISR");

            migrationBuilder.DropTable(
                name: "TiposBonificaciones");

            migrationBuilder.DropTable(
                name: "DetallePlanillas");

            migrationBuilder.DropTable(
                name: "Planillas");

            migrationBuilder.DropIndex(
                name: "IX_Inasistencias_TipoInasistencia",
                table: "Inasistencias");

            migrationBuilder.AlterColumn<string>(
                name: "Usuariomodificacion",
                table: "TipoPlanillas",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Usuariocreacion",
                table: "TipoPlanillas",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "TipoPlanilla",
                table: "TipoPlanillas",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Descripcion",
                table: "TipoPlanillas",
                nullable: true,
                oldClrType: typeof(string));
        }
    }
}
