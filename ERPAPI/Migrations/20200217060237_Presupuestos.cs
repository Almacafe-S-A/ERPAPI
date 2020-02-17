using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class Presupuestos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Presupuesto",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CostCenterId = table.Column<long>(nullable: false),
                    Periodo = table.Column<int>(nullable: false),
                    PresupuestoEnero = table.Column<decimal>(nullable: false),
                    PresupuestoFebrero = table.Column<decimal>(nullable: false),
                    PresupuestoMarzo = table.Column<decimal>(nullable: false),
                    PresupuestoAbril = table.Column<decimal>(nullable: false),
                    PresupuestoMayo = table.Column<decimal>(nullable: false),
                    PresupuestoJunio = table.Column<decimal>(nullable: false),
                    PresupuestoJulio = table.Column<decimal>(nullable: false),
                    PresupuestoAgosto = table.Column<decimal>(nullable: false),
                    PresupuestoSeptiembre = table.Column<decimal>(nullable: false),
                    PresupuestoOctubre = table.Column<decimal>(nullable: false),
                    PresupuestoNoviembre = table.Column<decimal>(nullable: false),
                    PresupuestoDiciembre = table.Column<decimal>(nullable: false),
                    EjecucionEnero = table.Column<decimal>(nullable: false),
                    EjecucionFebrero = table.Column<decimal>(nullable: false),
                    EjecucionMarzo = table.Column<decimal>(nullable: false),
                    EjecucionAbril = table.Column<decimal>(nullable: false),
                    EjecucionMayo = table.Column<decimal>(nullable: false),
                    EjecucionJunio = table.Column<decimal>(nullable: false),
                    EjecucionJulio = table.Column<decimal>(nullable: false),
                    EjecucionAgosto = table.Column<decimal>(nullable: false),
                    EjecucionSeptiembre = table.Column<decimal>(nullable: false),
                    EjecucionOctubre = table.Column<decimal>(nullable: false),
                    EjecucionNoviembre = table.Column<decimal>(nullable: false),
                    EjecucionDiciembre = table.Column<decimal>(nullable: false),
                    AccountigId = table.Column<long>(nullable: false),
                    AccountName = table.Column<string>(nullable: true),
                    TotalMontoPresupuesto = table.Column<decimal>(nullable: false),
                    TotalMontoEjecucion = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Presupuesto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Presupuesto_Accounting_AccountigId",
                        column: x => x.AccountigId,
                        principalTable: "Accounting",
                        principalColumn: "AccountId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Presupuesto_CostCenter_CostCenterId",
                        column: x => x.CostCenterId,
                        principalTable: "CostCenter",
                        principalColumn: "CostCenterId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Presupuesto_AccountigId",
                table: "Presupuesto",
                column: "AccountigId");

            migrationBuilder.CreateIndex(
                name: "IX_Presupuesto_CostCenterId",
                table: "Presupuesto",
                column: "CostCenterId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Presupuesto");
        }
    }
}
