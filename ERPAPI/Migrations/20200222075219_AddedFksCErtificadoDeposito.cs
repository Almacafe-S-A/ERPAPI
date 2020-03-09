using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class AddedFksCErtificadoDeposito : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Merma",
                table: "CertificadoLine",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "InsuranceId",
                table: "CertificadoDeposito",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "InsurancePolicyId",
                table: "CertificadoDeposito",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CertificadoDeposito_InsuranceId",
                table: "CertificadoDeposito",
                column: "InsuranceId");

            migrationBuilder.CreateIndex(
                name: "IX_CertificadoDeposito_InsurancePolicyId",
                table: "CertificadoDeposito",
                column: "InsurancePolicyId");

            migrationBuilder.AddForeignKey(
                name: "FK_CertificadoDeposito_Insurances_InsuranceId",
                table: "CertificadoDeposito",
                column: "InsuranceId",
                principalTable: "Insurances",
                principalColumn: "InsurancesId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CertificadoDeposito_InsurancePolicy_InsurancePolicyId",
                table: "CertificadoDeposito",
                column: "InsurancePolicyId",
                principalTable: "InsurancePolicy",
                principalColumn: "InsurancePolicyId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CertificadoDeposito_Insurances_InsuranceId",
                table: "CertificadoDeposito");

            migrationBuilder.DropForeignKey(
                name: "FK_CertificadoDeposito_InsurancePolicy_InsurancePolicyId",
                table: "CertificadoDeposito");

            migrationBuilder.DropIndex(
                name: "IX_CertificadoDeposito_InsuranceId",
                table: "CertificadoDeposito");

            migrationBuilder.DropIndex(
                name: "IX_CertificadoDeposito_InsurancePolicyId",
                table: "CertificadoDeposito");

            migrationBuilder.DropColumn(
                name: "Merma",
                table: "CertificadoLine");

            migrationBuilder.DropColumn(
                name: "InsuranceId",
                table: "CertificadoDeposito");

            migrationBuilder.DropColumn(
                name: "InsurancePolicyId",
                table: "CertificadoDeposito");
        }
    }
}
