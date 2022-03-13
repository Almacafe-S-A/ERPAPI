using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class InventarioFisicoLineCampos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "No",
                table: "InventarioFisicoLines",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UnitOfMeasureId",
                table: "InventarioFisicoLines",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UnitOfMeasureId1",
                table: "InventarioFisicoLines",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_InventarioFisicoLines_UnitOfMeasureId1",
                table: "InventarioFisicoLines",
                column: "UnitOfMeasureId1");

            migrationBuilder.AddForeignKey(
                name: "FK_InventarioFisicoLines_UnitOfMeasure_UnitOfMeasureId1",
                table: "InventarioFisicoLines",
                column: "UnitOfMeasureId1",
                principalTable: "UnitOfMeasure",
                principalColumn: "UnitOfMeasureId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InventarioFisicoLines_UnitOfMeasure_UnitOfMeasureId1",
                table: "InventarioFisicoLines");

            migrationBuilder.DropIndex(
                name: "IX_InventarioFisicoLines_UnitOfMeasureId1",
                table: "InventarioFisicoLines");

            migrationBuilder.DropColumn(
                name: "No",
                table: "InventarioFisicoLines");

            migrationBuilder.DropColumn(
                name: "UnitOfMeasureId",
                table: "InventarioFisicoLines");

            migrationBuilder.DropColumn(
                name: "UnitOfMeasureId1",
                table: "InventarioFisicoLines");
        }
    }
}
