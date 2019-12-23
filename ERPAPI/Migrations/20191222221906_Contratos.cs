using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class Contratos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Contrato",
                columns: table => new
                {
                    ContratoId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BranchId = table.Column<int>(nullable: true),
                    CustomerId = table.Column<long>(nullable: false),
                    Fecha = table.Column<DateTime>(nullable: false),
                    Fecha_inicio = table.Column<DateTime>(nullable: false),
                    Valor_prima = table.Column<double>(nullable: false),
                    Valor_Contrato = table.Column<double>(nullable: false),
                    Saldo_Contrato = table.Column<double>(nullable: false),
                    Dias_mora = table.Column<int>(nullable: false),
                    Estado = table.Column<int>(nullable: false),
                    Valor_cuota = table.Column<double>(nullable: false),
                    Cuotas_pagadas = table.Column<int>(nullable: false),
                    Cuotas_pendiente = table.Column<int>(nullable: false),
                    Proxima_fecha_de_pago = table.Column<DateTime>(nullable: false),
                    Ultima_fecha_de_pago = table.Column<DateTime>(nullable: false),
                    Fecha_de_vencimiento = table.Column<DateTime>(nullable: false),
                    Plazo = table.Column<int>(nullable: false),
                    Tasa_de_Interes = table.Column<double>(nullable: false),
                    UsuarioCreacion = table.Column<string>(nullable: false),
                    UsuarioModificacion = table.Column<string>(nullable: false),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contrato", x => x.ContratoId);
                    table.ForeignKey(
                        name: "FK_Contrato_Branch_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branch",
                        principalColumn: "BranchId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Contrato_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Contrato_detalle",
                columns: table => new
                {
                    Contrato_detalleId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ContratoId = table.Column<long>(nullable: false),
                    ProductId = table.Column<long>(nullable: false),
                    Cantidad = table.Column<double>(nullable: false),
                    Precio = table.Column<double>(nullable: false),
                    Monto = table.Column<double>(nullable: false),
                    Serie = table.Column<string>(maxLength: 100, nullable: true),
                    Modelo = table.Column<string>(maxLength: 100, nullable: true),
                    UsuarioCreacion = table.Column<string>(nullable: false),
                    UsuarioModificacion = table.Column<string>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contrato_detalle", x => x.Contrato_detalleId);
                    table.ForeignKey(
                        name: "FK_Contrato_detalle_Contrato_ContratoId",
                        column: x => x.ContratoId,
                        principalTable: "Contrato",
                        principalColumn: "ContratoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Contrato_movimientos",
                columns: table => new
                {
                    Contrato_movimientosId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ContratoId = table.Column<long>(nullable: false),
                    Fechamovimiento = table.Column<DateTime>(nullable: false),
                    BranchId = table.Column<int>(nullable: true),
                    tipo_movimiento = table.Column<int>(nullable: false),
                    Valorcapital = table.Column<double>(nullable: false),
                    Forma_pago = table.Column<int>(nullable: false),
                    EmployeesId = table.Column<long>(nullable: true),
                    UsuarioCreacion = table.Column<string>(nullable: false),
                    UsuarioModificacion = table.Column<string>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contrato_movimientos", x => x.Contrato_movimientosId);
                    table.ForeignKey(
                        name: "FK_Contrato_movimientos_Branch_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branch",
                        principalColumn: "BranchId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Contrato_movimientos_Contrato_ContratoId",
                        column: x => x.ContratoId,
                        principalTable: "Contrato",
                        principalColumn: "ContratoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Contrato_movimientos_Employees_EmployeesId",
                        column: x => x.EmployeesId,
                        principalTable: "Employees",
                        principalColumn: "IdEmpleado",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Contrato_plan_pagos",
                columns: table => new
                {
                    Nro_cuota = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ContratoId = table.Column<long>(nullable: false),
                    Fechacuota = table.Column<DateTime>(nullable: false),
                    Valorcapital = table.Column<double>(nullable: false),
                    Valorintereses = table.Column<double>(nullable: false),
                    Valorseguros = table.Column<double>(nullable: false),
                    Interesesmoratorios = table.Column<double>(nullable: false),
                    Valorotroscargos = table.Column<double>(nullable: false),
                    Estadocuota = table.Column<short>(nullable: false),
                    Valorpagado = table.Column<double>(nullable: false),
                    Fechapago = table.Column<DateTime>(nullable: false),
                    Recibopago = table.Column<string>(nullable: true),
                    UsuarioCreacion = table.Column<string>(nullable: false),
                    UsuarioModificacion = table.Column<string>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contrato_plan_pagos", x => new { x.Nro_cuota, x.ContratoId });
                    table.UniqueConstraint("AK_Contrato_plan_pagos_ContratoId_Nro_cuota", x => new { x.ContratoId, x.Nro_cuota });
                    table.ForeignKey(
                        name: "FK_Contrato_plan_pagos_Contrato_ContratoId",
                        column: x => x.ContratoId,
                        principalTable: "Contrato",
                        principalColumn: "ContratoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Contrato_BranchId",
                table: "Contrato",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_Contrato_CustomerId",
                table: "Contrato",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Contrato_detalle_ContratoId",
                table: "Contrato_detalle",
                column: "ContratoId");

            migrationBuilder.CreateIndex(
                name: "IX_Contrato_movimientos_BranchId",
                table: "Contrato_movimientos",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_Contrato_movimientos_ContratoId",
                table: "Contrato_movimientos",
                column: "ContratoId");

            migrationBuilder.CreateIndex(
                name: "IX_Contrato_movimientos_EmployeesId",
                table: "Contrato_movimientos",
                column: "EmployeesId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Contrato_detalle");

            migrationBuilder.DropTable(
                name: "Contrato_movimientos");

            migrationBuilder.DropTable(
                name: "Contrato_plan_pagos");

            migrationBuilder.DropTable(
                name: "Contrato");
        }
    }
}
