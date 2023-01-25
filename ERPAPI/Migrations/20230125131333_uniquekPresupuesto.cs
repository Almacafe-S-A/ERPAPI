using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class uniquekPresupuesto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Presupuesto_CostCenterId",
                table: "Presupuesto");

            migrationBuilder.AlterColumn<long>(
                name: "NoInicio",
                table: "NumeracionSAR",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "NoFin",
                table: "NumeracionSAR",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Presupuesto_CostCenterId_PeriodoId_AccountigId",
                table: "Presupuesto",
                columns: new[] { "CostCenterId", "PeriodoId", "AccountigId" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Presupuesto_CostCenterId_PeriodoId_AccountigId",
                table: "Presupuesto");

            migrationBuilder.AlterColumn<string>(
                name: "NoInicio",
                table: "NumeracionSAR",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AlterColumn<string>(
                name: "NoFin",
                table: "NumeracionSAR",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.CreateIndex(
                name: "IX_Presupuesto_CostCenterId",
                table: "Presupuesto",
                column: "CostCenterId");
        }
    }
}
