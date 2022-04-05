using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class Kardex : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_KardexLine_Kardex_KardexId",
                table: "KardexLine");

            migrationBuilder.DropIndex(
                name: "IX_KardexLine_KardexId",
                table: "KardexLine");

            migrationBuilder.DropColumn(
                name: "ControlEstibaName",
                table: "Kardex");

            migrationBuilder.DropColumn(
                name: "CostCenterId",
                table: "Kardex");

            migrationBuilder.DropColumn(
                name: "CostCenterName",
                table: "Kardex");

            migrationBuilder.DropColumn(
                name: "CurrencyId",
                table: "Kardex");

            migrationBuilder.DropColumn(
                name: "CurrencyName",
                table: "Kardex");

            migrationBuilder.DropColumn(
                name: "FechaCreacion",
                table: "Kardex");

            migrationBuilder.DropColumn(
                name: "FechaModificacion",
                table: "Kardex");

            migrationBuilder.DropColumn(
                name: "Max",
                table: "Kardex");

            migrationBuilder.DropColumn(
                name: "QuantityEntryCD",
                table: "Kardex");

            migrationBuilder.DropColumn(
                name: "QuantityOutCD",
                table: "Kardex");

            migrationBuilder.DropColumn(
                name: "TotalCD",
                table: "Kardex");

            migrationBuilder.DropColumn(
                name: "UsuarioCreacion",
                table: "Kardex");

            migrationBuilder.DropColumn(
                name: "UsuarioModificacion",
                table: "Kardex");

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalBags",
                table: "Kardex",
                nullable: false,
                oldClrType: typeof(decimal),
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Total",
                table: "Kardex",
                nullable: false,
                oldClrType: typeof(decimal),
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "QuantityOutBags",
                table: "Kardex",
                nullable: false,
                oldClrType: typeof(decimal),
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "QuantityOut",
                table: "Kardex",
                nullable: false,
                oldClrType: typeof(decimal),
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "QuantityEntryBags",
                table: "Kardex",
                nullable: false,
                oldClrType: typeof(decimal),
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "QuantityEntry",
                table: "Kardex",
                nullable: false,
                oldClrType: typeof(decimal),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "TotalBags",
                table: "Kardex",
                nullable: true,
                oldClrType: typeof(decimal));

            migrationBuilder.AlterColumn<decimal>(
                name: "Total",
                table: "Kardex",
                nullable: true,
                oldClrType: typeof(decimal));

            migrationBuilder.AlterColumn<decimal>(
                name: "QuantityOutBags",
                table: "Kardex",
                nullable: true,
                oldClrType: typeof(decimal));

            migrationBuilder.AlterColumn<decimal>(
                name: "QuantityOut",
                table: "Kardex",
                nullable: true,
                oldClrType: typeof(decimal));

            migrationBuilder.AlterColumn<decimal>(
                name: "QuantityEntryBags",
                table: "Kardex",
                nullable: true,
                oldClrType: typeof(decimal));

            migrationBuilder.AlterColumn<decimal>(
                name: "QuantityEntry",
                table: "Kardex",
                nullable: true,
                oldClrType: typeof(decimal));

            migrationBuilder.AddColumn<string>(
                name: "ControlEstibaName",
                table: "Kardex",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "CostCenterId",
                table: "Kardex",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CostCenterName",
                table: "Kardex",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "CurrencyId",
                table: "Kardex",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CurrencyName",
                table: "Kardex",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaCreacion",
                table: "Kardex",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaModificacion",
                table: "Kardex",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "Max",
                table: "Kardex",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "QuantityEntryCD",
                table: "Kardex",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "QuantityOutCD",
                table: "Kardex",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalCD",
                table: "Kardex",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UsuarioCreacion",
                table: "Kardex",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UsuarioModificacion",
                table: "Kardex",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_KardexLine_KardexId",
                table: "KardexLine",
                column: "KardexId");

            migrationBuilder.AddForeignKey(
                name: "FK_KardexLine_Kardex_KardexId",
                table: "KardexLine",
                column: "KardexId",
                principalTable: "Kardex",
                principalColumn: "KardexId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
