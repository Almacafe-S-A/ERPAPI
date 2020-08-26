using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class RemoveFields_InsuranceCertificates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DerechosFiscales",
                table: "InsurancesCertificateLines");

            migrationBuilder.DropColumn(
                name: "Obsservaciones",
                table: "InsurancesCertificateLines");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "DerechosFiscales",
                table: "InsurancesCertificateLines",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Obsservaciones",
                table: "InsurancesCertificateLines",
                nullable: true);
        }
    }
}
