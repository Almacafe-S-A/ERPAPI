using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class SegurosEndosadosCampos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FixedAssetGroup_FixedAssetGroup_FixedAssetGroupId1",
                table: "FixedAssetGroup");

            migrationBuilder.DropIndex(
                name: "IX_FixedAssetGroup_FixedAssetGroupId1",
                table: "FixedAssetGroup");

            migrationBuilder.DropColumn(
                name: "FixedAssetGroupId1",
                table: "FixedAssetGroup");

            migrationBuilder.AddColumn<decimal>(
                name: "TotalAssuredDifernce",
                table: "InsuranceEndorsement",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalCertificateBalalnce",
                table: "InsuranceEndorsement",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalAssuredDifernce",
                table: "InsuranceEndorsement");

            migrationBuilder.DropColumn(
                name: "TotalCertificateBalalnce",
                table: "InsuranceEndorsement");

            migrationBuilder.AddColumn<long>(
                name: "FixedAssetGroupId1",
                table: "FixedAssetGroup",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_FixedAssetGroup_FixedAssetGroupId1",
                table: "FixedAssetGroup",
                column: "FixedAssetGroupId1");

            migrationBuilder.AddForeignKey(
                name: "FK_FixedAssetGroup_FixedAssetGroup_FixedAssetGroupId1",
                table: "FixedAssetGroup",
                column: "FixedAssetGroupId1",
                principalTable: "FixedAssetGroup",
                principalColumn: "FixedAssetGroupId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
