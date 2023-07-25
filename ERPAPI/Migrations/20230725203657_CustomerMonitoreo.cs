using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class CustomerMonitoreo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ActividadEconomicaId",
                table: "Customer",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "GeneroId",
                table: "Customer",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ProfesionId",
                table: "Customer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Customer_ActividadEconomicaId",
                table: "Customer",
                column: "ActividadEconomicaId");

            migrationBuilder.CreateIndex(
                name: "IX_Customer_GeneroId",
                table: "Customer",
                column: "GeneroId");

            migrationBuilder.CreateIndex(
                name: "IX_Customer_ProfesionId",
                table: "Customer",
                column: "ProfesionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Customer_ElementoConfiguracion_ActividadEconomicaId",
                table: "Customer",
                column: "ActividadEconomicaId",
                principalTable: "ElementoConfiguracion",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Customer_ElementoConfiguracion_GeneroId",
                table: "Customer",
                column: "GeneroId",
                principalTable: "ElementoConfiguracion",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Customer_ElementoConfiguracion_ProfesionId",
                table: "Customer",
                column: "ProfesionId",
                principalTable: "ElementoConfiguracion",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customer_ElementoConfiguracion_ActividadEconomicaId",
                table: "Customer");

            migrationBuilder.DropForeignKey(
                name: "FK_Customer_ElementoConfiguracion_GeneroId",
                table: "Customer");

            migrationBuilder.DropForeignKey(
                name: "FK_Customer_ElementoConfiguracion_ProfesionId",
                table: "Customer");

            migrationBuilder.DropIndex(
                name: "IX_Customer_ActividadEconomicaId",
                table: "Customer");

            migrationBuilder.DropIndex(
                name: "IX_Customer_GeneroId",
                table: "Customer");

            migrationBuilder.DropIndex(
                name: "IX_Customer_ProfesionId",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "ActividadEconomicaId",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "GeneroId",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "ProfesionId",
                table: "Customer");
        }
    }
}
