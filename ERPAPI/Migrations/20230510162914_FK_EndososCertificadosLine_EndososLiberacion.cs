using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class FK_EndososCertificadosLine_EndososLiberacion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_EndososLiberacion_EndososLineId",
                table: "EndososLiberacion",
                column: "EndososLineId");

            migrationBuilder.AddForeignKey(
                name: "FK_EndososLiberacion_EndososCertificadosLine_EndososLineId",
                table: "EndososLiberacion",
                column: "EndososLineId",
                principalTable: "EndososCertificadosLine",
                principalColumn: "EndososCertificadosLineId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EndososLiberacion_EndososCertificadosLine_EndososLineId",
                table: "EndososLiberacion");

            migrationBuilder.DropIndex(
                name: "IX_EndososLiberacion_EndososLineId",
                table: "EndososLiberacion");
        }
    }
}
