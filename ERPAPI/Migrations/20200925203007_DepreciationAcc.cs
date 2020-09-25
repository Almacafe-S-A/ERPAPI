using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class DepreciationAcc : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FixedAssetGroup_Accounting_ResidualValueFixedAssetAccountingId",
                table: "FixedAssetGroup");

            migrationBuilder.RenameColumn(
                name: "ResidualValueFixedAssetAccountingId",
                table: "FixedAssetGroup",
                newName: "AccumulatedDepreciationAccountingId");

            migrationBuilder.RenameIndex(
                name: "IX_FixedAssetGroup_ResidualValueFixedAssetAccountingId",
                table: "FixedAssetGroup",
                newName: "IX_FixedAssetGroup_AccumulatedDepreciationAccountingId");

            migrationBuilder.AddForeignKey(
                name: "FK_FixedAssetGroup_Accounting_AccumulatedDepreciationAccountingId",
                table: "FixedAssetGroup",
                column: "AccumulatedDepreciationAccountingId",
                principalTable: "Accounting",
                principalColumn: "AccountId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FixedAssetGroup_Accounting_AccumulatedDepreciationAccountingId",
                table: "FixedAssetGroup");

            migrationBuilder.RenameColumn(
                name: "AccumulatedDepreciationAccountingId",
                table: "FixedAssetGroup",
                newName: "ResidualValueFixedAssetAccountingId");

            migrationBuilder.RenameIndex(
                name: "IX_FixedAssetGroup_AccumulatedDepreciationAccountingId",
                table: "FixedAssetGroup",
                newName: "IX_FixedAssetGroup_ResidualValueFixedAssetAccountingId");

            migrationBuilder.AddForeignKey(
                name: "FK_FixedAssetGroup_Accounting_ResidualValueFixedAssetAccountingId",
                table: "FixedAssetGroup",
                column: "ResidualValueFixedAssetAccountingId",
                principalTable: "Accounting",
                principalColumn: "AccountId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
