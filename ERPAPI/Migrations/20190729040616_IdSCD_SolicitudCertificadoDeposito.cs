using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class IdSCD_SolicitudCertificadoDeposito : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SolicitudCertificadoLine_SolicitudCertificadoDeposito_IdCD",
                table: "SolicitudCertificadoLine");

            migrationBuilder.RenameColumn(
                name: "IdCD",
                table: "SolicitudCertificadoLine",
                newName: "IdSCD");

            migrationBuilder.RenameIndex(
                name: "IX_SolicitudCertificadoLine_IdCD",
                table: "SolicitudCertificadoLine",
                newName: "IX_SolicitudCertificadoLine_IdSCD");

            migrationBuilder.RenameColumn(
                name: "IdCD",
                table: "SolicitudCertificadoDeposito",
                newName: "IdSCD");

            migrationBuilder.AddForeignKey(
                name: "FK_SolicitudCertificadoLine_SolicitudCertificadoDeposito_IdSCD",
                table: "SolicitudCertificadoLine",
                column: "IdSCD",
                principalTable: "SolicitudCertificadoDeposito",
                principalColumn: "IdSCD",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SolicitudCertificadoLine_SolicitudCertificadoDeposito_IdSCD",
                table: "SolicitudCertificadoLine");

            migrationBuilder.RenameColumn(
                name: "IdSCD",
                table: "SolicitudCertificadoLine",
                newName: "IdCD");

            migrationBuilder.RenameIndex(
                name: "IX_SolicitudCertificadoLine_IdSCD",
                table: "SolicitudCertificadoLine",
                newName: "IX_SolicitudCertificadoLine_IdCD");

            migrationBuilder.RenameColumn(
                name: "IdSCD",
                table: "SolicitudCertificadoDeposito",
                newName: "IdCD");

            migrationBuilder.AddForeignKey(
                name: "FK_SolicitudCertificadoLine_SolicitudCertificadoDeposito_IdCD",
                table: "SolicitudCertificadoLine",
                column: "IdCD",
                principalTable: "SolicitudCertificadoDeposito",
                principalColumn: "IdCD",
                onDelete: ReferentialAction.Cascade);
        }
    }
}