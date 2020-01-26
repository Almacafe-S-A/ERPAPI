using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class AddedFields_FixedAssetGroup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "JournalEntryLine",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 60,
                oldNullable: true);

            migrationBuilder.AddColumn<long>(
                name: "DepreciationAccountingId",
                table: "FixedAssetGroup",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "FixedAssetAccountingId",
                table: "FixedAssetGroup",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_FixedAssetGroup_DepreciationAccountingId",
                table: "FixedAssetGroup",
                column: "DepreciationAccountingId");

            migrationBuilder.CreateIndex(
                name: "IX_FixedAssetGroup_FixedAssetAccountingId",
                table: "FixedAssetGroup",
                column: "FixedAssetAccountingId");

            migrationBuilder.AddForeignKey(
                name: "FK_FixedAssetGroup_Accounting_DepreciationAccountingId",
                table: "FixedAssetGroup",
                column: "DepreciationAccountingId",
                principalTable: "Accounting",
                principalColumn: "AccountId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FixedAssetGroup_Accounting_FixedAssetAccountingId",
                table: "FixedAssetGroup",
                column: "FixedAssetAccountingId",
                principalTable: "Accounting",
                principalColumn: "AccountId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FixedAssetGroup_Accounting_DepreciationAccountingId",
                table: "FixedAssetGroup");

            migrationBuilder.DropForeignKey(
                name: "FK_FixedAssetGroup_Accounting_FixedAssetAccountingId",
                table: "FixedAssetGroup");

            migrationBuilder.DropIndex(
                name: "IX_FixedAssetGroup_DepreciationAccountingId",
                table: "FixedAssetGroup");

            migrationBuilder.DropIndex(
                name: "IX_FixedAssetGroup_FixedAssetAccountingId",
                table: "FixedAssetGroup");

            migrationBuilder.DropColumn(
                name: "DepreciationAccountingId",
                table: "FixedAssetGroup");

            migrationBuilder.DropColumn(
                name: "FixedAssetAccountingId",
                table: "FixedAssetGroup");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "JournalEntryLine",
                maxLength: 60,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
