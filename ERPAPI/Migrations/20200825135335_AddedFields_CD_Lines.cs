using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class AddedFields_CD_Lines : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "DerechosFiscales",
                table: "CertificadoLine",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "Observaciones",
                table: "CertificadoLine",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DerechosFiscales",
                table: "CertificadoLine");

            migrationBuilder.DropColumn(
                name: "Observaciones",
                table: "CertificadoLine");
        }
    }
}
