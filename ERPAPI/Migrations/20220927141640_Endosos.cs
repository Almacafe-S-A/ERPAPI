using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class Endosos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "DerechosFiscales",
                table: "EndososCertificadosLine",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Pda",
                table: "EndososCertificadosLine",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "ValorUnitarioDerechos",
                table: "EndososCertificadosLine",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DerechosFiscales",
                table: "EndososCertificadosLine");

            migrationBuilder.DropColumn(
                name: "Pda",
                table: "EndososCertificadosLine");

            migrationBuilder.DropColumn(
                name: "ValorUnitarioDerechos",
                table: "EndososCertificadosLine");
        }
    }
}
