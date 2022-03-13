using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class InventarioFisicodet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InventarioBodegaHabilitada_UnitOfMeasure_UnitOfMeasureId1",
                table: "InventarioBodegaHabilitada");

            migrationBuilder.DropForeignKey(
                name: "FK_InventarioFisicoLines_UnitOfMeasure_UnitOfMeasureId1",
                table: "InventarioFisicoLines");

            migrationBuilder.DropIndex(
                name: "IX_InventarioFisicoLines_UnitOfMeasureId1",
                table: "InventarioFisicoLines");

            migrationBuilder.DropIndex(
                name: "IX_InventarioBodegaHabilitada_UnitOfMeasureId1",
                table: "InventarioBodegaHabilitada");

            migrationBuilder.DropColumn(
                name: "UnitOfMeasureId1",
                table: "InventarioFisicoLines");

            migrationBuilder.DropColumn(
                name: "UnitOfMeasureId1",
                table: "InventarioBodegaHabilitada");

            migrationBuilder.AlterColumn<int>(
                name: "UnitOfMeasureId",
                table: "InventarioFisicoLines",
                nullable: true,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "UnitOfMeasureId",
                table: "InventarioBodegaHabilitada",
                nullable: true,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_InventarioFisicoLines_UnitOfMeasureId",
                table: "InventarioFisicoLines",
                column: "UnitOfMeasureId");

            migrationBuilder.CreateIndex(
                name: "IX_InventarioBodegaHabilitada_UnitOfMeasureId",
                table: "InventarioBodegaHabilitada",
                column: "UnitOfMeasureId");

            migrationBuilder.AddForeignKey(
                name: "FK_InventarioBodegaHabilitada_UnitOfMeasure_UnitOfMeasureId",
                table: "InventarioBodegaHabilitada",
                column: "UnitOfMeasureId",
                principalTable: "UnitOfMeasure",
                principalColumn: "UnitOfMeasureId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_InventarioFisicoLines_UnitOfMeasure_UnitOfMeasureId",
                table: "InventarioFisicoLines",
                column: "UnitOfMeasureId",
                principalTable: "UnitOfMeasure",
                principalColumn: "UnitOfMeasureId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InventarioBodegaHabilitada_UnitOfMeasure_UnitOfMeasureId",
                table: "InventarioBodegaHabilitada");

            migrationBuilder.DropForeignKey(
                name: "FK_InventarioFisicoLines_UnitOfMeasure_UnitOfMeasureId",
                table: "InventarioFisicoLines");

            migrationBuilder.DropIndex(
                name: "IX_InventarioFisicoLines_UnitOfMeasureId",
                table: "InventarioFisicoLines");

            migrationBuilder.DropIndex(
                name: "IX_InventarioBodegaHabilitada_UnitOfMeasureId",
                table: "InventarioBodegaHabilitada");

            migrationBuilder.AlterColumn<long>(
                name: "UnitOfMeasureId",
                table: "InventarioFisicoLines",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UnitOfMeasureId1",
                table: "InventarioFisicoLines",
                nullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "UnitOfMeasureId",
                table: "InventarioBodegaHabilitada",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UnitOfMeasureId1",
                table: "InventarioBodegaHabilitada",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_InventarioFisicoLines_UnitOfMeasureId1",
                table: "InventarioFisicoLines",
                column: "UnitOfMeasureId1");

            migrationBuilder.CreateIndex(
                name: "IX_InventarioBodegaHabilitada_UnitOfMeasureId1",
                table: "InventarioBodegaHabilitada",
                column: "UnitOfMeasureId1");

            migrationBuilder.AddForeignKey(
                name: "FK_InventarioBodegaHabilitada_UnitOfMeasure_UnitOfMeasureId1",
                table: "InventarioBodegaHabilitada",
                column: "UnitOfMeasureId1",
                principalTable: "UnitOfMeasure",
                principalColumn: "UnitOfMeasureId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_InventarioFisicoLines_UnitOfMeasure_UnitOfMeasureId1",
                table: "InventarioFisicoLines",
                column: "UnitOfMeasureId1",
                principalTable: "UnitOfMeasure",
                principalColumn: "UnitOfMeasureId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
