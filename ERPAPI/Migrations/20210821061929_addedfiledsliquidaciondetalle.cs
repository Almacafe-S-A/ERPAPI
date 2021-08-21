using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class addedfiledsliquidaciondetalle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "PrecioUnitarioCIF",
                table: "LiquidacionLine",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "ValorTotalCIF",
                table: "LiquidacionLine",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PrecioUnitarioCIF",
                table: "LiquidacionLine");

            migrationBuilder.DropColumn(
                name: "ValorTotalCIF",
                table: "LiquidacionLine");
        }
    }
}
