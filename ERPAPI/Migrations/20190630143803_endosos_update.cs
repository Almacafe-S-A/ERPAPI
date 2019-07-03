using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class endosos_update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_EndososTalonLine_EndososTalonId",
                table: "EndososTalonLine",
                column: "EndososTalonId");

            migrationBuilder.CreateIndex(
                name: "IX_EndososCertificadosLine_EndososCertificadosId",
                table: "EndososCertificadosLine",
                column: "EndososCertificadosId");

            migrationBuilder.CreateIndex(
                name: "IX_EndososBonoLine_EndososBonoId",
                table: "EndososBonoLine",
                column: "EndososBonoId");

            migrationBuilder.AddForeignKey(
                name: "FK_EndososBonoLine_EndososBono_EndososBonoId",
                table: "EndososBonoLine",
                column: "EndososBonoId",
                principalTable: "EndososBono",
                principalColumn: "EndososBonoId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EndososCertificadosLine_EndososCertificados_EndososCertificadosId",
                table: "EndososCertificadosLine",
                column: "EndososCertificadosId",
                principalTable: "EndososCertificados",
                principalColumn: "EndososCertificadosId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EndososTalonLine_EndososTalon_EndososTalonId",
                table: "EndososTalonLine",
                column: "EndososTalonId",
                principalTable: "EndososTalon",
                principalColumn: "EndososTalonId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EndososBonoLine_EndososBono_EndososBonoId",
                table: "EndososBonoLine");

            migrationBuilder.DropForeignKey(
                name: "FK_EndososCertificadosLine_EndososCertificados_EndososCertificadosId",
                table: "EndososCertificadosLine");

            migrationBuilder.DropForeignKey(
                name: "FK_EndososTalonLine_EndososTalon_EndososTalonId",
                table: "EndososTalonLine");

            migrationBuilder.DropIndex(
                name: "IX_EndososTalonLine_EndososTalonId",
                table: "EndososTalonLine");

            migrationBuilder.DropIndex(
                name: "IX_EndososCertificadosLine_EndososCertificadosId",
                table: "EndososCertificadosLine");

            migrationBuilder.DropIndex(
                name: "IX_EndososBonoLine_EndososBonoId",
                table: "EndososBonoLine");
        }
    }
}
