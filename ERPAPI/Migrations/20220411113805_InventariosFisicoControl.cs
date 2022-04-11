using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class InventariosFisicoControl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Control",
                table: "InventarioFisico",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "EstadoId",
                table: "InventarioFisico",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EstadoName",
                table: "InventarioFisico",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_InventarioFisico_EstadoId",
                table: "InventarioFisico",
                column: "EstadoId");

            migrationBuilder.AddForeignKey(
                name: "FK_InventarioFisico_Estados_EstadoId",
                table: "InventarioFisico",
                column: "EstadoId",
                principalTable: "Estados",
                principalColumn: "IdEstado",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InventarioFisico_Estados_EstadoId",
                table: "InventarioFisico");

            migrationBuilder.DropIndex(
                name: "IX_InventarioFisico_EstadoId",
                table: "InventarioFisico");

            migrationBuilder.DropColumn(
                name: "Control",
                table: "InventarioFisico");

            migrationBuilder.DropColumn(
                name: "EstadoId",
                table: "InventarioFisico");

            migrationBuilder.DropColumn(
                name: "EstadoName",
                table: "InventarioFisico");
        }
    }
}
