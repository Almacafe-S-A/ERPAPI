using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class CambiosBoletaSalida : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "UnitOfMeasureId",
                table: "BoletaDeSalidaLines",
                nullable: true,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "SubProductId",
                table: "BoletaDeSalida",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.CreateIndex(
                name: "IX_BoletaDeSalidaLines_SubProductId",
                table: "BoletaDeSalidaLines",
                column: "SubProductId");

            migrationBuilder.CreateIndex(
                name: "IX_BoletaDeSalidaLines_UnitOfMeasureId",
                table: "BoletaDeSalidaLines",
                column: "UnitOfMeasureId");

            migrationBuilder.AddForeignKey(
                name: "FK_BoletaDeSalidaLines_SubProduct_SubProductId",
                table: "BoletaDeSalidaLines",
                column: "SubProductId",
                principalTable: "SubProduct",
                principalColumn: "SubproductId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BoletaDeSalidaLines_UnitOfMeasure_UnitOfMeasureId",
                table: "BoletaDeSalidaLines",
                column: "UnitOfMeasureId",
                principalTable: "UnitOfMeasure",
                principalColumn: "UnitOfMeasureId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BoletaDeSalidaLines_SubProduct_SubProductId",
                table: "BoletaDeSalidaLines");

            migrationBuilder.DropForeignKey(
                name: "FK_BoletaDeSalidaLines_UnitOfMeasure_UnitOfMeasureId",
                table: "BoletaDeSalidaLines");

            migrationBuilder.DropIndex(
                name: "IX_BoletaDeSalidaLines_SubProductId",
                table: "BoletaDeSalidaLines");

            migrationBuilder.DropIndex(
                name: "IX_BoletaDeSalidaLines_UnitOfMeasureId",
                table: "BoletaDeSalidaLines");

            migrationBuilder.AlterColumn<long>(
                name: "UnitOfMeasureId",
                table: "BoletaDeSalidaLines",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "SubProductId",
                table: "BoletaDeSalida",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);
        }
    }
}
