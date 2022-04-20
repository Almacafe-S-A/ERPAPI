using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class SaldoCertificarInventarioFisico : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "MaxKardex",
                table: "Kardex",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<decimal>(
                name: "SaldoCertificado",
                table: "InventarioBodegaHabilitada",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "SaldoporCertificar",
                table: "InventarioBodegaHabilitada",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaxKardex",
                table: "Kardex");

            migrationBuilder.DropColumn(
                name: "SaldoCertificado",
                table: "InventarioBodegaHabilitada");

            migrationBuilder.DropColumn(
                name: "SaldoporCertificar",
                table: "InventarioBodegaHabilitada");
        }
    }
}
