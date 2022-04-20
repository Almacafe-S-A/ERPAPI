using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class SaldoCertificarInventarioFisic : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SaldoporCertificar",
                table: "InventarioBodegaHabilitada");

            migrationBuilder.AddColumn<decimal>(
                name: "SaldoPendienteCertificar",
                table: "InventarioBodegaHabilitada",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SaldoPendienteCertificar",
                table: "InventarioBodegaHabilitada");

            migrationBuilder.AddColumn<decimal>(
                name: "SaldoporCertificar",
                table: "InventarioBodegaHabilitada",
                nullable: true);
        }
    }
}
