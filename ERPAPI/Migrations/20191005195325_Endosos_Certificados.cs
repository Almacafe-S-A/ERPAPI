using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class Endosos_Certificados : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "CertificadoLineId",
                table: "EndososTalonLine",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "CertificadoLineId",
                table: "EndososCertificadosLine",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<double>(
                name: "Quantity",
                table: "EndososCertificadosLine",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<long>(
                name: "CertificadoLineId",
                table: "EndososBonoLine",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<double>(
                name: "Quantity",
                table: "EndososBonoLine",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "SaldoEndoso",
                table: "CertificadoLine",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CertificadoLineId",
                table: "EndososTalonLine");

            migrationBuilder.DropColumn(
                name: "CertificadoLineId",
                table: "EndososCertificadosLine");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "EndososCertificadosLine");

            migrationBuilder.DropColumn(
                name: "CertificadoLineId",
                table: "EndososBonoLine");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "EndososBonoLine");

            migrationBuilder.DropColumn(
                name: "SaldoEndoso",
                table: "CertificadoLine");
        }
    }
}
