using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class RecursosHumanos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bitacora",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IdOperacion = table.Column<long>(nullable: true),
                    HoraInicio = table.Column<DateTime>(nullable: true),
                    HoraFin = table.Column<DateTime>(nullable: true),
                    Accion = table.Column<string>(nullable: true),
                    NoReferencia = table.Column<string>(nullable: true),
                    Descripcion = table.Column<string>(nullable: true),
                    QueryEjecuto = table.Column<string>(nullable: true),
                    UsuarioEjecucion = table.Column<string>(nullable: true),
                    UsuarioModificacion = table.Column<string>(nullable: true),
                    UsuarioCreacion = table.Column<string>(nullable: true),
                    FechaModificacion = table.Column<DateTime>(nullable: true),
                    FechaCreacion = table.Column<DateTime>(nullable: true),
                    IdEstado = table.Column<long>(nullable: false),
                    Estado = table.Column<string>(nullable: true),
                    ResultadoSerializado = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bitacora", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CalculoPlanilla",
                columns: table => new
                {
                    Idcalculo = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Fechainicio = table.Column<DateTime>(nullable: true),
                    Fechafin = table.Column<DateTime>(nullable: true),
                    Descripcion = table.Column<string>(nullable: true),
                    IdPlanilla = table.Column<long>(nullable: true),
                    TasaCambio = table.Column<decimal>(nullable: true),
                    IdEstado = table.Column<long>(nullable: false),
                    Estado = table.Column<string>(nullable: true),
                    FechaCreacion = table.Column<DateTime>(nullable: true),
                    FechaModificacion = table.Column<DateTime>(nullable: true),
                    UsuarioCreacion = table.Column<string>(nullable: true),
                    UsuarioModificacion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalculoPlanilla", x => x.Idcalculo);
                });

            migrationBuilder.CreateTable(
                name: "CalculoPlanillaDetalle",
                columns: table => new
                {
                    Iddetallecalculo = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Idempleado = table.Column<long>(nullable: true),
                    IdCalculo = table.Column<long>(nullable: true),
                    IdPuesto = table.Column<long>(nullable: true),
                    IdFormula = table.Column<long>(nullable: true),
                    ValorFormula = table.Column<decimal>(nullable: true),
                    IdtipoFormula = table.Column<int>(nullable: true),
                    NombreTipoFormula = table.Column<string>(nullable: true),
                    FormulaEjecutada = table.Column<string>(nullable: true),
                    Nombreempleado = table.Column<string>(nullable: true),
                    NombreFormula = table.Column<string>(nullable: true),
                    IdOrdenFormula = table.Column<long>(nullable: true),
                    Fechacreacion = table.Column<DateTime>(nullable: true),
                    FechaModificacion = table.Column<DateTime>(nullable: true),
                    UsuarioCreacion = table.Column<string>(nullable: true),
                    UsuarioModificacion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalculoPlanillaDetalle", x => x.Iddetallecalculo);
                });

            migrationBuilder.CreateTable(
                name: "City",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    State_Id = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_City", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Country",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SortName = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    PhoneCode = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Country", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    IdEmpleado = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    NombreEmpleado = table.Column<string>(nullable: true),
                    Correo = table.Column<string>(nullable: true),
                    Puesto = table.Column<long>(nullable: true),
                    FechaNacimiento = table.Column<DateTime>(nullable: true),
                    FechaIngreso = table.Column<DateTime>(nullable: true),
                    Salario = table.Column<decimal>(nullable: true),
                    Estado = table.Column<string>(nullable: true),
                    Identidad = table.Column<string>(nullable: true),
                    FechaEgreso = table.Column<DateTime>(nullable: true),
                    Direccion = table.Column<string>(nullable: true),
                    Genero = table.Column<string>(nullable: true),
                    IdEstado = table.Column<long>(nullable: true),
                    NombreEstado = table.Column<string>(nullable: true),
                    Ciudad = table.Column<string>(nullable: true),
                    IdPais = table.Column<long>(nullable: true),
                    NombrePais = table.Column<string>(nullable: true),
                    IdCiudad = table.Column<long>(nullable: true),
                    NombreCiudad = table.Column<string>(nullable: true),
                    MonedaSalario = table.Column<long>(nullable: true),
                    Userid = table.Column<string>(nullable: true),
                    Idsescalas = table.Column<long>(nullable: false),
                    IdActivoinactivo = table.Column<long>(nullable: true),
                    Foto = table.Column<string>(nullable: true),
                    IdBanco = table.Column<long>(nullable: true),
                    CuentaBanco = table.Column<string>(nullable: true),
                    Usuariocreacion = table.Column<string>(nullable: true),
                    Usuariomodificacion = table.Column<string>(nullable: true),
                    FechaCreacion = table.Column<DateTime>(nullable: true),
                    FechaModificacion = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.IdEmpleado);
                });

            migrationBuilder.CreateTable(
                name: "Escala",
                columns: table => new
                {
                    IdEscala = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    NombreEscala = table.Column<string>(nullable: true),
                    DescripcionEscala = table.Column<string>(nullable: true),
                    ValorInicial = table.Column<decimal>(nullable: true),
                    ValorFinal = table.Column<decimal>(nullable: true),
                    Porcentaje = table.Column<decimal>(nullable: true),
                    ValorCalculo = table.Column<decimal>(nullable: true),
                    Idpadre = table.Column<long>(nullable: true),
                    IdEstado = table.Column<long>(nullable: true),
                    Estado = table.Column<string>(nullable: true),
                    FechaCreacion = table.Column<DateTime>(nullable: true),
                    FechaModificacion = table.Column<DateTime>(nullable: true),
                    Usuariocreacion = table.Column<string>(nullable: true),
                    Usuariomodificacion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Escala", x => x.IdEscala);
                });

            migrationBuilder.CreateTable(
                name: "Formula",
                columns: table => new
                {
                    IdFormula = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    NombreFormula = table.Column<string>(nullable: true),
                    CalculoFormula = table.Column<string>(nullable: true),
                    IdEstado = table.Column<long>(nullable: true),
                    NombreEstado = table.Column<string>(nullable: true),
                    IdTipoFormula = table.Column<int>(nullable: true),
                    NombreTipoformula = table.Column<string>(nullable: true),
                    FechaCreacion = table.Column<DateTime>(nullable: true),
                    FechaModificacion = table.Column<DateTime>(nullable: true),
                    UsuarioCreacion = table.Column<string>(nullable: true),
                    UsuarioModificacion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Formula", x => x.IdFormula);
                });

            migrationBuilder.CreateTable(
                name: "FormulasAplicadas",
                columns: table => new
                {
                    IdFormulaAplicada = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IdFormula = table.Column<long>(nullable: true),
                    NombreFormula = table.Column<string>(nullable: true),
                    IdEstado = table.Column<long>(nullable: true),
                    Estado = table.Column<string>(nullable: true),
                    IdEmpleado = table.Column<long>(nullable: true),
                    FormulaEmpleada = table.Column<string>(nullable: true),
                    MultiplicarPor = table.Column<decimal>(nullable: true),
                    IdCalculo = table.Column<long>(nullable: true),
                    FechaCreacion = table.Column<DateTime>(nullable: true),
                    FechaModificacion = table.Column<DateTime>(nullable: true),
                    UsuarioCreacion = table.Column<string>(nullable: true),
                    UsuarioModificacion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormulasAplicadas", x => x.IdFormulaAplicada);
                });

            migrationBuilder.CreateTable(
                name: "HoursWorked",
                columns: table => new
                {
                    IdHorastrabajadas = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IdEmpleado = table.Column<long>(nullable: true),
                    FechaEntrada = table.Column<DateTime>(nullable: true),
                    NombreEmpleado = table.Column<string>(nullable: true),
                    UsuarioCreacion = table.Column<string>(nullable: true),
                    UsuarioModificacion = table.Column<string>(nullable: true),
                    FechaCreacion = table.Column<DateTime>(nullable: true),
                    FechaModificacion = table.Column<DateTime>(nullable: true),
                    EsFeriado = table.Column<bool>(nullable: true),
                    MultiplicaHoras = table.Column<decimal>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HoursWorked", x => x.IdHorastrabajadas);
                });

            migrationBuilder.CreateTable(
                name: "OrdenFormula",
                columns: table => new
                {
                    Idordenformula = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IdPlanilla = table.Column<long>(nullable: true),
                    Idformula = table.Column<long>(nullable: true),
                    Orden = table.Column<int>(nullable: true),
                    IdEstado = table.Column<long>(nullable: false),
                    Estado = table.Column<string>(nullable: true),
                    FechaCreacion = table.Column<DateTime>(nullable: true),
                    FechaModificacion = table.Column<DateTime>(nullable: true),
                    UsuarioCreacion = table.Column<string>(nullable: true),
                    UsuarioModificacion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrdenFormula", x => x.Idordenformula);
                });

            migrationBuilder.CreateTable(
                name: "Payroll",
                columns: table => new
                {
                    IdPlanilla = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    NombrePlanilla = table.Column<string>(nullable: true),
                    DescripcionPlanilla = table.Column<string>(nullable: true),
                    IdEstado = table.Column<long>(nullable: false),
                    Estado = table.Column<string>(nullable: true),
                    UsuarioCreacion = table.Column<string>(nullable: true),
                    UsuarioModificacion = table.Column<string>(nullable: true),
                    FechaCreacion = table.Column<DateTime>(nullable: true),
                    FechaModificacion = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payroll", x => x.IdPlanilla);
                });

            migrationBuilder.CreateTable(
                name: "PayrollEmployee",
                columns: table => new
                {
                    IdPlanillaempleado = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IdPlanilla = table.Column<long>(nullable: true),
                    IdEmpleado = table.Column<long>(nullable: true),
                    IdEstado = table.Column<long>(nullable: true),
                    Estado = table.Column<string>(nullable: true),
                    FechaIngreso = table.Column<DateTime>(nullable: true),
                    FechaCreacion = table.Column<DateTime>(nullable: true),
                    FechaModificacion = table.Column<DateTime>(nullable: true),
                    UsuarioCreacion = table.Column<string>(nullable: true),
                    UsuarioModificacion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PayrollEmployee", x => x.IdPlanillaempleado);
                });

            migrationBuilder.CreateTable(
                name: "Reconciliacion",
                columns: table => new
                {
                    IdReconciliacion = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DescripcionReconciliacion = table.Column<string>(nullable: true),
                    FechaReconciliacion = table.Column<DateTime>(nullable: true),
                    IdCalculoplanilla = table.Column<long>(nullable: true),
                    FechaAplicacion = table.Column<DateTime>(nullable: true),
                    TotalReconciliacion = table.Column<decimal>(nullable: true),
                    SaldoEmpleado = table.Column<decimal>(nullable: true),
                    FechaFinReconciliacion = table.Column<DateTime>(nullable: true),
                    Tasacambio = table.Column<decimal>(nullable: true),
                    IdEstado = table.Column<long>(nullable: false),
                    Estado = table.Column<string>(nullable: true),
                    Fechacreacion = table.Column<DateTime>(nullable: true),
                    FechaModificacion = table.Column<DateTime>(nullable: true),
                    UsuarioCreacion = table.Column<string>(nullable: true),
                    Usuariomodificacion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reconciliacion", x => x.IdReconciliacion);
                });

            migrationBuilder.CreateTable(
                name: "ReconciliacionDetalle",
                columns: table => new
                {
                    IdDetallereconciliacion = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IdReconciliacion = table.Column<long>(nullable: true),
                    IdEmpleado = table.Column<long>(nullable: true),
                    Year = table.Column<int>(nullable: true),
                    Month = table.Column<int>(nullable: true),
                    Dia = table.Column<int>(nullable: true),
                    Descripcion = table.Column<string>(nullable: true),
                    Salario = table.Column<decimal>(nullable: true),
                    Horasextras = table.Column<decimal>(nullable: true),
                    Bonos = table.Column<decimal>(nullable: true),
                    OtrosIngresos = table.Column<decimal>(nullable: true),
                    SalarioRecibido = table.Column<decimal>(nullable: true),
                    IdPlanilla = table.Column<long>(nullable: true),
                    FechaCreacion = table.Column<DateTime>(nullable: true),
                    FechaModificacion = table.Column<DateTime>(nullable: true),
                    UsuarioCreacion = table.Column<string>(nullable: true),
                    UsuarioModificacion = table.Column<string>(nullable: true),
                    Deducciones = table.Column<decimal>(nullable: true),
                    CatorceSalarioProporcional = table.Column<decimal>(nullable: true),
                    TrecesalarioProporcional = table.Column<decimal>(nullable: true),
                    Quincesalarioproporcional = table.Column<decimal>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReconciliacionDetalle", x => x.IdDetallereconciliacion);
                });

            migrationBuilder.CreateTable(
                name: "ReconciliacionEscala",
                columns: table => new
                {
                    IdEscalareconciliacion = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IdEmpleado = table.Column<long>(nullable: true),
                    IdEscala = table.Column<long>(nullable: true),
                    DescripcionEscala = table.Column<string>(nullable: true),
                    IdConcepto = table.Column<long>(nullable: true),
                    NombreConcepto = table.Column<string>(nullable: true),
                    MontoEscala = table.Column<decimal>(nullable: true),
                    IdReconciliacion = table.Column<long>(nullable: true),
                    IdPlanilla = table.Column<long>(nullable: true),
                    Montoretenido = table.Column<decimal>(nullable: true),
                    DiferenciaPorretener = table.Column<decimal>(nullable: true),
                    MesesRestantes = table.Column<int>(nullable: true),
                    MesesEjecutados = table.Column<int>(nullable: true),
                    Montotrecesalario = table.Column<decimal>(nullable: true),
                    Montocatorcesalario = table.Column<decimal>(nullable: true),
                    Montoquincesalario = table.Column<decimal>(nullable: true),
                    FechaCreacion = table.Column<DateTime>(nullable: true),
                    FechaModificacion = table.Column<DateTime>(nullable: true),
                    UsuarioCreacion = table.Column<string>(nullable: true),
                    UsuarioModificacion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReconciliacionEscala", x => x.IdEscalareconciliacion);
                });

            migrationBuilder.CreateTable(
                name: "ReconciliacionGasto",
                columns: table => new
                {
                    Idreconciliaciongasto = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Idreconciliacion = table.Column<long>(nullable: true),
                    Descripciongasto = table.Column<string>(nullable: true),
                    Montogasto = table.Column<decimal>(nullable: true),
                    IdEmpleado = table.Column<long>(nullable: true),
                    IdPlanilla = table.Column<long>(nullable: true),
                    Fechaaplicacion = table.Column<DateTime>(nullable: true),
                    Fechacreacion = table.Column<DateTime>(nullable: true),
                    Fechamodificacion = table.Column<DateTime>(nullable: true),
                    Usuariocreacion = table.Column<string>(nullable: true),
                    Usuariomodificacion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReconciliacionGasto", x => x.Idreconciliaciongasto);
                });

            migrationBuilder.CreateTable(
                name: "HoursWorkedDetail",
                columns: table => new
                {
                    IdDetallehorastrabajadas = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IdHorasTrabajadas = table.Column<long>(nullable: true),
                    Horaentrada = table.Column<DateTime>(nullable: true),
                    Horasalida = table.Column<DateTime>(nullable: true),
                    Multiplicahoras = table.Column<decimal>(nullable: true),
                    UsuarioCreacion = table.Column<string>(nullable: true),
                    UsuarioModificacion = table.Column<string>(nullable: true),
                    FechaCreacion = table.Column<DateTime>(nullable: true),
                    FechaModificacion = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HoursWorkedDetail", x => x.IdDetallehorastrabajadas);
                    table.ForeignKey(
                        name: "FK_HoursWorkedDetail_HoursWorked_IdHorasTrabajadas",
                        column: x => x.IdHorasTrabajadas,
                        principalTable: "HoursWorked",
                        principalColumn: "IdHorastrabajadas",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HoursWorkedDetail_IdHorasTrabajadas",
                table: "HoursWorkedDetail",
                column: "IdHorasTrabajadas");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bitacora");

            migrationBuilder.DropTable(
                name: "CalculoPlanilla");

            migrationBuilder.DropTable(
                name: "CalculoPlanillaDetalle");

            migrationBuilder.DropTable(
                name: "City");

            migrationBuilder.DropTable(
                name: "Country");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Escala");

            migrationBuilder.DropTable(
                name: "Formula");

            migrationBuilder.DropTable(
                name: "FormulasAplicadas");

            migrationBuilder.DropTable(
                name: "HoursWorkedDetail");

            migrationBuilder.DropTable(
                name: "OrdenFormula");

            migrationBuilder.DropTable(
                name: "Payroll");

            migrationBuilder.DropTable(
                name: "PayrollEmployee");

            migrationBuilder.DropTable(
                name: "Reconciliacion");

            migrationBuilder.DropTable(
                name: "ReconciliacionDetalle");

            migrationBuilder.DropTable(
                name: "ReconciliacionEscala");

            migrationBuilder.DropTable(
                name: "ReconciliacionGasto");

            migrationBuilder.DropTable(
                name: "HoursWorked");
        }
    }
}
