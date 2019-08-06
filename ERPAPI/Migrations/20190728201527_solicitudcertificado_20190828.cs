using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class solicitudcertificado_20190828 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "CenterCostId",
                table: "SolicitudCertificadoLine",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "CenterCostName",
                table: "SolicitudCertificadoLine",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "ValorImpuestos",
                table: "SolicitudCertificadoLine",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CenterCostId",
                table: "SolicitudCertificadoLine");

            migrationBuilder.DropColumn(
                name: "CenterCostName",
                table: "SolicitudCertificadoLine");

            migrationBuilder.DropColumn(
                name: "ValorImpuestos",
                table: "SolicitudCertificadoLine");
        }
    }
}
