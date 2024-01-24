using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class AccountserviceSubService2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BranchId",
                table: "SolicitudCertificadoDeposito",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BranchName",
                table: "SolicitudCertificadoDeposito",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "AccountId",
                table: "ProductRelation",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AccountName",
                table: "ProductRelation",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SolicitudCertificadoDeposito_BranchId",
                table: "SolicitudCertificadoDeposito",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductRelation_AccountId",
                table: "ProductRelation",
                column: "AccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductRelation_Accounting_AccountId",
                table: "ProductRelation",
                column: "AccountId",
                principalTable: "Accounting",
                principalColumn: "AccountId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SolicitudCertificadoDeposito_Branch_BranchId",
                table: "SolicitudCertificadoDeposito",
                column: "BranchId",
                principalTable: "Branch",
                principalColumn: "BranchId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductRelation_Accounting_AccountId",
                table: "ProductRelation");

            migrationBuilder.DropForeignKey(
                name: "FK_SolicitudCertificadoDeposito_Branch_BranchId",
                table: "SolicitudCertificadoDeposito");

            migrationBuilder.DropIndex(
                name: "IX_SolicitudCertificadoDeposito_BranchId",
                table: "SolicitudCertificadoDeposito");

            migrationBuilder.DropIndex(
                name: "IX_ProductRelation_AccountId",
                table: "ProductRelation");

            migrationBuilder.DropColumn(
                name: "BranchId",
                table: "SolicitudCertificadoDeposito");

            migrationBuilder.DropColumn(
                name: "BranchName",
                table: "SolicitudCertificadoDeposito");

            migrationBuilder.DropColumn(
                name: "AccountId",
                table: "ProductRelation");

            migrationBuilder.DropColumn(
                name: "AccountName",
                table: "ProductRelation");
        }
    }
}
