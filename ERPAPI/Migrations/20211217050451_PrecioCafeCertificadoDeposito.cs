using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class PrecioCafeCertificadoDeposito : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PrecioCafeId",
                table: "CertificadoDeposito",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CertificadoDeposito_PrecioCafeId",
                table: "CertificadoDeposito",
                column: "PrecioCafeId");

            migrationBuilder.AddForeignKey(
                name: "FK_CertificadoDeposito_PrecioCafe_PrecioCafeId",
                table: "CertificadoDeposito",
                column: "PrecioCafeId",
                principalTable: "PrecioCafe",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CertificadoDeposito_PrecioCafe_PrecioCafeId",
                table: "CertificadoDeposito");

            migrationBuilder.DropIndex(
                name: "IX_CertificadoDeposito_PrecioCafeId",
                table: "CertificadoDeposito");

            migrationBuilder.DropColumn(
                name: "PrecioCafeId",
                table: "CertificadoDeposito");
        }
    }
}
