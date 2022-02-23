using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class InventarioFisicoCorreccion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InventarioFisico_InventarioFisico_InventarioFisicoId",
                table: "InventarioFisico");

            migrationBuilder.DropIndex(
                name: "IX_InventarioFisico_InventarioFisicoId",
                table: "InventarioFisico");

            migrationBuilder.DropColumn(
                name: "InventarioFisicoId",
                table: "InventarioFisico");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "InventarioFisicoId",
                table: "InventarioFisico",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_InventarioFisico_InventarioFisicoId",
                table: "InventarioFisico",
                column: "InventarioFisicoId");

            migrationBuilder.AddForeignKey(
                name: "FK_InventarioFisico_InventarioFisico_InventarioFisicoId",
                table: "InventarioFisico",
                column: "InventarioFisicoId",
                principalTable: "InventarioFisico",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
