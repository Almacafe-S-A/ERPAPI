using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class fkInventarioFisicoExistencias : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExistenciaBodegaHabilitadas_InventarioBodegaHabilitada_InventarioBodegaHabilitadaId",
                table: "ExistenciaBodegaHabilitadas");

            migrationBuilder.DropIndex(
                name: "IX_ExistenciaBodegaHabilitadas_InventarioBodegaHabilitadaId",
                table: "ExistenciaBodegaHabilitadas");

            migrationBuilder.DropColumn(
                name: "InventarioBodegaHabilitadaId",
                table: "ExistenciaBodegaHabilitadas");

            migrationBuilder.RenameColumn(
                name: "InventarioBodegaHabilidaId",
                table: "ExistenciaBodegaHabilitadas",
                newName: "InventarioFisicoId");

            migrationBuilder.AddColumn<bool>(
                name: "Max",
                table: "ExistenciaBodegaHabilitadas",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_ExistenciaBodegaHabilitadas_InventarioFisicoId",
                table: "ExistenciaBodegaHabilitadas",
                column: "InventarioFisicoId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExistenciaBodegaHabilitadas_InventarioFisico_InventarioFisicoId",
                table: "ExistenciaBodegaHabilitadas",
                column: "InventarioFisicoId",
                principalTable: "InventarioFisico",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExistenciaBodegaHabilitadas_InventarioFisico_InventarioFisicoId",
                table: "ExistenciaBodegaHabilitadas");

            migrationBuilder.DropIndex(
                name: "IX_ExistenciaBodegaHabilitadas_InventarioFisicoId",
                table: "ExistenciaBodegaHabilitadas");

            migrationBuilder.DropColumn(
                name: "Max",
                table: "ExistenciaBodegaHabilitadas");

            migrationBuilder.RenameColumn(
                name: "InventarioFisicoId",
                table: "ExistenciaBodegaHabilitadas",
                newName: "InventarioBodegaHabilidaId");

            migrationBuilder.AddColumn<int>(
                name: "InventarioBodegaHabilitadaId",
                table: "ExistenciaBodegaHabilitadas",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ExistenciaBodegaHabilitadas_InventarioBodegaHabilitadaId",
                table: "ExistenciaBodegaHabilitadas",
                column: "InventarioBodegaHabilitadaId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExistenciaBodegaHabilitadas_InventarioBodegaHabilitada_InventarioBodegaHabilitadaId",
                table: "ExistenciaBodegaHabilitadas",
                column: "InventarioBodegaHabilitadaId",
                principalTable: "InventarioBodegaHabilitada",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
