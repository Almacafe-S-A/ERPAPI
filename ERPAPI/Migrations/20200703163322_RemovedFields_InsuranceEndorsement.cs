using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class RemovedFields_InsuranceEndorsement : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalAssuredDifernce",
                table: "InsuranceEndorsement");

            migrationBuilder.DropColumn(
                name: "TotalCertificateBalalnce",
                table: "InsuranceEndorsement");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "TotalAssuredDifernce",
                table: "InsuranceEndorsement",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalCertificateBalalnce",
                table: "InsuranceEndorsement",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
