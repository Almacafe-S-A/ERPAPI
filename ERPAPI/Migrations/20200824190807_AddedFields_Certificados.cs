using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class AddedFields_Certificados : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Comentario",
                table: "CertificadoDeposito",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "PorcentajeDeudas",
                table: "CertificadoDeposito",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "SituadoEn",
                table: "CertificadoDeposito",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Comentario",
                table: "CertificadoDeposito");

            migrationBuilder.DropColumn(
                name: "PorcentajeDeudas",
                table: "CertificadoDeposito");

            migrationBuilder.DropColumn(
                name: "SituadoEn",
                table: "CertificadoDeposito");
        }
    }
}
