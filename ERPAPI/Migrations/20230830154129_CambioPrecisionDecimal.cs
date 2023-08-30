using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
	public partial class CambioPrecisionDecimal : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.AlterColumn<decimal>(
				name: "SaldoLibros",
				table: "InventarioFisicoLines",
				type: "decimal(18, 6)", // Nueva precisión
				nullable: false,
				oldClrType: typeof(decimal),
				oldType: "decimal(18, 2)"); // Precisión anterior

			migrationBuilder.AlterColumn<decimal>(
				name: "InventarioFisicoCantidad",
				table: "InventarioFisicoLines",
				type: "decimal(18, 6)", // Nueva precisión
				nullable: false,
				oldClrType: typeof(decimal),
				oldType: "decimal(18, 2)"); // Precisión anterior

			migrationBuilder.AlterColumn<decimal>(
				name: "Diferencia",
				table: "InventarioFisicoLines",
				type: "decimal(18, 6)", // Nueva precisión
				nullable: false,
				oldClrType: typeof(decimal),
				oldType: "decimal(18, 2)"); // Precisión anterior

			migrationBuilder.AlterColumn<decimal>(
				name: "NSacos",
				table: "InventarioFisicoLines",
				type: "decimal(18, 6)", // Nueva precisión
				nullable: true,
				oldClrType: typeof(decimal),
				oldType: "decimal(18, 2)"); // Precisión anterior

			migrationBuilder.AlterColumn<decimal>(
				name: "FactorSacos",
				table: "InventarioFisicoLines",
				type: "decimal(18, 6)", // Nueva precisión
				nullable: true,
				oldClrType: typeof(decimal),
				oldType: "decimal(18, 2)"); // Precisión anterior
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.AlterColumn<decimal>(
				name: "SaldoLibros",
				table: "InventarioFisicoLines",
				type: "decimal(18, 2)", // Precisión original
				nullable: false,
				oldClrType: typeof(decimal),
				oldType: "decimal(18, 6)"); // Nueva precisión

			migrationBuilder.AlterColumn<decimal>(
				name: "InventarioFisicoCantidad",
				table: "InventarioFisicoLines",
				type: "decimal(18, 2)", // Precisión original
				nullable: false,
				oldClrType: typeof(decimal),
				oldType: "decimal(18, 6)"); // Nueva precisión

			migrationBuilder.AlterColumn<decimal>(
				name: "Diferencia",
				table: "InventarioFisicoLines",
				type: "decimal(18, 2)", // Precisión original
				nullable: false,
				oldClrType: typeof(decimal),
				oldType: "decimal(18, 6)"); // Nueva precisión

			migrationBuilder.AlterColumn<decimal>(
				name: "NSacos",
				table: "InventarioFisicoLines",
				type: "decimal(18, 2)", // Precisión original
				nullable: true,
				oldClrType: typeof(decimal),
				oldType: "decimal(18, 6)"); // Nueva precisión

			migrationBuilder.AlterColumn<decimal>(
				name: "FactorSacos",
				table: "InventarioFisicoLines",
				type: "decimal(18, 2)", // Precisión original
				nullable: true,
				oldClrType: typeof(decimal),
				oldType: "decimal(18, 6)"); // Nueva precisión
		}
	}
}
