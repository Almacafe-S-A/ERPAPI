using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class PrecioCafeTipos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "TotalUSDEgreso",
                table: "PrecioCafe",
                type: "Money",
                nullable: false,
                oldClrType: typeof(decimal));

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalLPSIngreso",
                table: "PrecioCafe",
                type: "Money",
                nullable: true,
                oldClrType: typeof(decimal),
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalLPSEgreso",
                table: "PrecioCafe",
                type: "Money",
                nullable: true,
                oldClrType: typeof(decimal),
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "PrecioQQOro",
                table: "PrecioCafe",
                type: "Money",
                nullable: true,
                oldClrType: typeof(decimal),
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "PrecioQQCalidadesInferiores",
                table: "PrecioCafe",
                type: "Money",
                nullable: true,
                oldClrType: typeof(decimal),
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "PercioQQPergamino",
                table: "PrecioCafe",
                type: "Money",
                nullable: true,
                oldClrType: typeof(decimal),
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "NetoLPSIngreso",
                table: "PrecioCafe",
                type: "Money",
                nullable: true,
                oldClrType: typeof(decimal),
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "NetoLPSConsumoInterno",
                table: "PrecioCafe",
                type: "Money",
                nullable: false,
                oldClrType: typeof(decimal));

            migrationBuilder.AlterColumn<decimal>(
                name: "BrutoLPSIngreso",
                table: "PrecioCafe",
                type: "Money",
                nullable: true,
                oldClrType: typeof(decimal),
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "BrutoLPSConsumoInterno",
                table: "PrecioCafe",
                type: "Money",
                nullable: false,
                oldClrType: typeof(decimal));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "TotalUSDEgreso",
                table: "PrecioCafe",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "Money");

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalLPSIngreso",
                table: "PrecioCafe",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "Money",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalLPSEgreso",
                table: "PrecioCafe",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "Money",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "PrecioQQOro",
                table: "PrecioCafe",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "Money",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "PrecioQQCalidadesInferiores",
                table: "PrecioCafe",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "Money",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "PercioQQPergamino",
                table: "PrecioCafe",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "Money",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "NetoLPSIngreso",
                table: "PrecioCafe",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "Money",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "NetoLPSConsumoInterno",
                table: "PrecioCafe",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "Money");

            migrationBuilder.AlterColumn<decimal>(
                name: "BrutoLPSIngreso",
                table: "PrecioCafe",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "Money",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "BrutoLPSConsumoInterno",
                table: "PrecioCafe",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "Money");
        }
    }
}
