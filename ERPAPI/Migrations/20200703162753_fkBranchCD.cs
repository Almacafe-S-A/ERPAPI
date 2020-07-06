using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class fkBranchCD : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "BranchId",
                table: "CertificadoDeposito",
                nullable: false,
                oldClrType: typeof(long));

            migrationBuilder.CreateIndex(
                name: "IX_CertificadoDeposito_BranchId",
                table: "CertificadoDeposito",
                column: "BranchId");

            migrationBuilder.AddForeignKey(
                name: "FK_CertificadoDeposito_Branch_BranchId",
                table: "CertificadoDeposito",
                column: "BranchId",
                principalTable: "Branch",
                principalColumn: "BranchId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CertificadoDeposito_Branch_BranchId",
                table: "CertificadoDeposito");

            migrationBuilder.DropIndex(
                name: "IX_CertificadoDeposito_BranchId",
                table: "CertificadoDeposito");

            migrationBuilder.AlterColumn<long>(
                name: "BranchId",
                table: "CertificadoDeposito",
                nullable: false,
                oldClrType: typeof(int));
        }
    }
}
