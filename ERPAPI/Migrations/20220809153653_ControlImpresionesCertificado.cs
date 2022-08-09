using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class ControlImpresionesCertificado : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Impresiones",
                table: "CertificadoDeposito",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "impresionesTalon",
                table: "CertificadoDeposito",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Impresiones",
                table: "CertificadoDeposito");

            migrationBuilder.DropColumn(
                name: "impresionesTalon",
                table: "CertificadoDeposito");
        }
    }
}
