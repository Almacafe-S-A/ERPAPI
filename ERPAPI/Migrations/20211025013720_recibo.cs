using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class recibo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ReciboId",
                table: "SolicitudCertificadoLine",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "CantidadDisponible",
                table: "CertificadoLine",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ReciboId",
                table: "CertificadoLine",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReciboId",
                table: "SolicitudCertificadoLine");

            migrationBuilder.DropColumn(
                name: "CantidadDisponible",
                table: "CertificadoLine");

            migrationBuilder.DropColumn(
                name: "ReciboId",
                table: "CertificadoLine");
        }
    }
}
