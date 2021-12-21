using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class SaldosCertificado : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "CantidadDisponibleAutorizar",
                table: "CertificadoLine",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Saldo",
                table: "CertificadoLine",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CantidadDisponibleAutorizar",
                table: "CertificadoLine");

            migrationBuilder.DropColumn(
                name: "Saldo",
                table: "CertificadoLine");
        }
    }
}
