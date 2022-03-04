using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class SAldosFisicos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Max",
                table: "Kardex",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "SaldoFisico",
                table: "GoodsReceivedLine",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "SaldoFisicoSacos",
                table: "GoodsReceivedLine",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Max",
                table: "Kardex");

            migrationBuilder.DropColumn(
                name: "SaldoFisico",
                table: "GoodsReceivedLine");

            migrationBuilder.DropColumn(
                name: "SaldoFisicoSacos",
                table: "GoodsReceivedLine");
        }
    }
}
