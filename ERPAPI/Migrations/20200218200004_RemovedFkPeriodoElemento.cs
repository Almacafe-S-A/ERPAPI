using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class RemovedFkPeriodoElemento : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Presupuesto_ElementoConfiguracion_Periodo",
                table: "Presupuesto");

            migrationBuilder.DropIndex(
                name: "IX_Presupuesto_Periodo",
                table: "Presupuesto");

            migrationBuilder.DropColumn(
                name: "Periodo",
                table: "Presupuesto");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "Periodo",
                table: "Presupuesto",
                nullable: false,
                defaultValue: 0L);

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
                onDelete: ReferentialAction.Cascade);
        }
    }
}
