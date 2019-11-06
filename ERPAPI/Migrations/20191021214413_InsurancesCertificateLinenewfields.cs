using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class InsurancesCertificateLinenewfields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "WarehouseId",
                table: "InsurancesCertificateLine",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "WarehouseName",
                table: "InsurancesCertificateLine",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WarehouseId",
                table: "InsurancesCertificateLine");

            migrationBuilder.DropColumn(
                name: "WarehouseName",
                table: "InsurancesCertificateLine");
        }
    }
}
