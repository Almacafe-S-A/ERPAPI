using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class BoletaSalida : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GoodsReceivedLine_SubProduct_SubProductId",
                table: "GoodsReceivedLine");

            migrationBuilder.DropColumn(
                name: "Currency",
                table: "GoodsReceived");

            migrationBuilder.DropColumn(
                name: "CurrencyId",
                table: "GoodsReceived");

            migrationBuilder.DropColumn(
                name: "CurrencyName",
                table: "GoodsReceived");

            migrationBuilder.AlterColumn<long>(
                name: "UnitOfMeasureId",
                table: "GoodsReceivedLine",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AlterColumn<long>(
                name: "SubProductId",
                table: "GoodsReceivedLine",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AlterColumn<decimal>(
                name: "QuantitySacos",
                table: "GoodsReceivedLine",
                nullable: true,
                oldClrType: typeof(decimal));

            migrationBuilder.AlterColumn<long>(
                name: "SubProductId",
                table: "GoodsReceived",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AlterColumn<long>(
                name: "IdEstado",
                table: "GoodsReceived",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AlterColumn<long>(
                name: "UnitOfMeasureId",
                table: "BoletaDeSalida",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AddForeignKey(
                name: "FK_GoodsReceivedLine_SubProduct_SubProductId",
                table: "GoodsReceivedLine",
                column: "SubProductId",
                principalTable: "SubProduct",
                principalColumn: "SubproductId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GoodsReceivedLine_SubProduct_SubProductId",
                table: "GoodsReceivedLine");

            migrationBuilder.AlterColumn<long>(
                name: "UnitOfMeasureId",
                table: "GoodsReceivedLine",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "SubProductId",
                table: "GoodsReceivedLine",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "QuantitySacos",
                table: "GoodsReceivedLine",
                nullable: false,
                oldClrType: typeof(decimal),
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "SubProductId",
                table: "GoodsReceived",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "IdEstado",
                table: "GoodsReceived",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Currency",
                table: "GoodsReceived",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CurrencyId",
                table: "GoodsReceived",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "CurrencyName",
                table: "GoodsReceived",
                nullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "UnitOfMeasureId",
                table: "BoletaDeSalida",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_GoodsReceivedLine_SubProduct_SubProductId",
                table: "GoodsReceivedLine",
                column: "SubProductId",
                principalTable: "SubProduct",
                principalColumn: "SubproductId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
