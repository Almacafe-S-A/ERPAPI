using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class warehousetypevalue : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CustomerName",
                table: "InsuranceCertificate",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DatePlace",
                table: "InsuranceCertificate",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InsuranceName",
                table: "InsuranceCertificate",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InsurancePolicyNumber",
                table: "InsuranceCertificate",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "WarehouseId",
                table: "CertificadoLine",
                nullable: false,
                oldClrType: typeof(long));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CustomerName",
                table: "InsuranceCertificate");

            migrationBuilder.DropColumn(
                name: "DatePlace",
                table: "InsuranceCertificate");

            migrationBuilder.DropColumn(
                name: "InsuranceName",
                table: "InsuranceCertificate");

            migrationBuilder.DropColumn(
                name: "InsurancePolicyNumber",
                table: "InsuranceCertificate");

            migrationBuilder.AlterColumn<long>(
                name: "WarehouseId",
                table: "CertificadoLine",
                nullable: false,
                oldClrType: typeof(int));
        }
    }
}
