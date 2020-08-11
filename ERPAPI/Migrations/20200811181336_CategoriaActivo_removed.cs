using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class CategoriaActivo_removed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Warehouse_ElementoConfiguracion_CategoriaActivoId",
                table: "Warehouse");

            migrationBuilder.DropIndex(
                name: "IX_Warehouse_CategoriaActivoId",
                table: "Warehouse");

            migrationBuilder.DropColumn(
                name: "CategoriaActivoId",
                table: "Warehouse");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "CategoriaActivoId",
                table: "Warehouse",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Warehouse_CategoriaActivoId",
                table: "Warehouse",
                column: "CategoriaActivoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Warehouse_ElementoConfiguracion_CategoriaActivoId",
                table: "Warehouse",
                column: "CategoriaActivoId",
                principalTable: "ElementoConfiguracion",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
