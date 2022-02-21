using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class InventariosFisicosKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InventarioFisicoLines_InventarioFisico_InventarioFisicoId",
                table: "InventarioFisicoLines");

            migrationBuilder.AlterColumn<int>(
                name: "InventarioFisicoId",
                table: "InventarioFisicoLines",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_InventarioFisicoLines_InventarioFisico_InventarioFisicoId",
                table: "InventarioFisicoLines",
                column: "InventarioFisicoId",
                principalTable: "InventarioFisico",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InventarioFisicoLines_InventarioFisico_InventarioFisicoId",
                table: "InventarioFisicoLines");

            migrationBuilder.AlterColumn<int>(
                name: "InventarioFisicoId",
                table: "InventarioFisicoLines",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_InventarioFisicoLines_InventarioFisico_InventarioFisicoId",
                table: "InventarioFisicoLines",
                column: "InventarioFisicoId",
                principalTable: "InventarioFisico",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
