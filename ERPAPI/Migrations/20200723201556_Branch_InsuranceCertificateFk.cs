using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class Branch_InsuranceCertificateFk : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BranchId",
                table: "InsuranceCertificate",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_InsuranceCertificate_BranchId",
                table: "InsuranceCertificate",
                column: "BranchId");

            migrationBuilder.AddForeignKey(
                name: "FK_InsuranceCertificate_Branch_BranchId",
                table: "InsuranceCertificate",
                column: "BranchId",
                principalTable: "Branch",
                principalColumn: "BranchId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InsuranceCertificate_Branch_BranchId",
                table: "InsuranceCertificate");

            migrationBuilder.DropIndex(
                name: "IX_InsuranceCertificate_BranchId",
                table: "InsuranceCertificate");

            migrationBuilder.DropColumn(
                name: "BranchId",
                table: "InsuranceCertificate");
        }
    }
}
