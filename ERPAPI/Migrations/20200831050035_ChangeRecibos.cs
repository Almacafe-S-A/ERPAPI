using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class ChangeRecibos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RecibosAsociados",
                table: "CertificadoDeposito",
                newName: "Recibos");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Recibos",
                table: "CertificadoDeposito",
                newName: "RecibosAsociados");
        }
    }
}
