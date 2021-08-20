using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class TipoMonedaTasaCambioLiquidacion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "TasaCambio",
                table: "Liquidacion",
                type: "money",
                nullable: false,
                oldClrType: typeof(decimal));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "TasaCambio",
                table: "Liquidacion",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "money");
        }
    }
}
