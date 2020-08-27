using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class CuentaValorResidualActivo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ResidualValueFixedAssetAccountingId",
                table: "FixedAssetGroup",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_FixedAssetGroup_ResidualValueFixedAssetAccountingId",
                table: "FixedAssetGroup",
                column: "ResidualValueFixedAssetAccountingId");

            migrationBuilder.AddForeignKey(
                name: "FK_FixedAssetGroup_Accounting_ResidualValueFixedAssetAccountingId",
                table: "FixedAssetGroup",
                column: "ResidualValueFixedAssetAccountingId",
                principalTable: "Accounting",
                principalColumn: "AccountId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FixedAssetGroup_Accounting_ResidualValueFixedAssetAccountingId",
                table: "FixedAssetGroup");

            migrationBuilder.DropIndex(
                name: "IX_FixedAssetGroup_ResidualValueFixedAssetAccountingId",
                table: "FixedAssetGroup");

            migrationBuilder.DropColumn(
                name: "ResidualValueFixedAssetAccountingId",
                table: "FixedAssetGroup");
        }
    }
}
