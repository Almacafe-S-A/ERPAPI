using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class FkEndososcetificados : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_EndososCertificados_IdCD",
                table: "EndososCertificados",
                column: "IdCD");

            migrationBuilder.AddForeignKey(
                name: "FK_EndososCertificados_CertificadoDeposito_IdCD",
                table: "EndososCertificados",
                column: "IdCD",
                principalTable: "CertificadoDeposito",
                principalColumn: "IdCD",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EndososCertificados_CertificadoDeposito_IdCD",
                table: "EndososCertificados");

            migrationBuilder.DropIndex(
                name: "IX_EndososCertificados_IdCD",
                table: "EndososCertificados");
        }
    }
}
