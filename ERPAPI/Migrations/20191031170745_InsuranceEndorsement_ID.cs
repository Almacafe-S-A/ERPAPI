using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class InsuranceEndorsement_ID : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "InsuranceEndorsementLine",
                newName: "InsuranceEndorsementLineId");

            migrationBuilder.RenameColumn(
                name: "WarehouseID",
                table: "InsuranceEndorsement",
                newName: "WarehouseId");

            migrationBuilder.RenameColumn(
                name: "WharehouseTypeId",
                table: "InsuranceEndorsement",
                newName: "WarehouseTypeId");

            migrationBuilder.RenameColumn(
                name: "CertificateBalalnce",
                table: "InsuranceEndorsement",
                newName: "TotalCertificateBalalnce");

            migrationBuilder.RenameColumn(
                name: "AssuredDifernce",
                table: "InsuranceEndorsement",
                newName: "TotalAssuredDifernce");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "InsuranceEndorsement",
                newName: "InsuranceEndorsementId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "InsuranceEndorsementLineId",
                table: "InsuranceEndorsementLine",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "WarehouseId",
                table: "InsuranceEndorsement",
                newName: "WarehouseID");

            migrationBuilder.RenameColumn(
                name: "WarehouseTypeId",
                table: "InsuranceEndorsement",
                newName: "WharehouseTypeId");

            migrationBuilder.RenameColumn(
                name: "TotalCertificateBalalnce",
                table: "InsuranceEndorsement",
                newName: "CertificateBalalnce");

            migrationBuilder.RenameColumn(
                name: "TotalAssuredDifernce",
                table: "InsuranceEndorsement",
                newName: "AssuredDifernce");

            migrationBuilder.RenameColumn(
                name: "InsuranceEndorsementId",
                table: "InsuranceEndorsement",
                newName: "Id");
        }
    }
}
