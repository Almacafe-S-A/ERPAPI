using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class AddedFieldsLiquidacions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "CantidadRecibida",
                table: "LiquidacionLine",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalOtrosImpuestos",
                table: "Liquidacion",
                type: "money",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CantidadRecibida",
                table: "LiquidacionLine");

            migrationBuilder.DropColumn(
                name: "TotalOtrosImpuestos",
                table: "Liquidacion");
        }
    }
}
