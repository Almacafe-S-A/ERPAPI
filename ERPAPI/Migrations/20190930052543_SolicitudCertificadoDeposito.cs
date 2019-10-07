using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class SolicitudCertificadoDeposito : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Aduana",
                table: "SolicitudCertificadoDeposito",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ManifiestoNo",
                table: "SolicitudCertificadoDeposito",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Aduana",
                table: "CertificadoDeposito",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ManifiestoNo",
                table: "CertificadoDeposito",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Aduana",
                table: "SolicitudCertificadoDeposito");

            migrationBuilder.DropColumn(
                name: "ManifiestoNo",
                table: "SolicitudCertificadoDeposito");

            migrationBuilder.DropColumn(
                name: "Aduana",
                table: "CertificadoDeposito");

            migrationBuilder.DropColumn(
                name: "ManifiestoNo",
                table: "CertificadoDeposito");
        }
    }
}
