using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class AddedfieldsLiquidacion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "TotalDerechos",
                table: "LiquidacionLine",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UOM",
                table: "LiquidacionLine",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "ValorUnitarioDerechos",
                table: "LiquidacionLine",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NoFactura",
                table: "Liquidacion",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NoPoliza",
                table: "Liquidacion",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalDerechos",
                table: "LiquidacionLine");

            migrationBuilder.DropColumn(
                name: "UOM",
                table: "LiquidacionLine");

            migrationBuilder.DropColumn(
                name: "ValorUnitarioDerechos",
                table: "LiquidacionLine");

            migrationBuilder.DropColumn(
                name: "NoFactura",
                table: "Liquidacion");

            migrationBuilder.DropColumn(
                name: "NoPoliza",
                table: "Liquidacion");
        }
    }
}
