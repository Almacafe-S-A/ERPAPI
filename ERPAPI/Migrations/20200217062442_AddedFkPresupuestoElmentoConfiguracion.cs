using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class AddedFkPresupuestoElmentoConfiguracion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "Periodo",
                table: "Presupuesto",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.CreateIndex(
                name: "IX_Presupuesto_Periodo",
                table: "Presupuesto",
                column: "Periodo");

            migrationBuilder.AddForeignKey(
                name: "FK_Presupuesto_ElementoConfiguracion_Periodo",
                table: "Presupuesto",
                column: "Periodo",
                principalTable: "ElementoConfiguracion",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Presupuesto_ElementoConfiguracion_Periodo",
                table: "Presupuesto");

            migrationBuilder.DropIndex(
                name: "IX_Presupuesto_Periodo",
                table: "Presupuesto");

            migrationBuilder.AlterColumn<int>(
                name: "Periodo",
                table: "Presupuesto",
                nullable: false,
                oldClrType: typeof(long));
        }
    }
}
