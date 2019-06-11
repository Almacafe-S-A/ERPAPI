using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class RelacionCertificadoDepositoFacturas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "CertificadoDepositoId",
                table: "ProformaInvoice",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "CertificadoDepositoId",
                table: "Invoice",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CertificadoDepositoId",
                table: "ProformaInvoice");

            migrationBuilder.DropColumn(
                name: "CertificadoDepositoId",
                table: "Invoice");
        }
    }
}
