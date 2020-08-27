using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class fixedassetaccountignResidual : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_FixedAsset_FixedAssetGroupId",
                table: "FixedAsset",
                column: "FixedAssetGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_FixedAsset_FixedAssetGroup_FixedAssetGroupId",
                table: "FixedAsset",
                column: "FixedAssetGroupId",
                principalTable: "FixedAssetGroup",
                principalColumn: "FixedAssetGroupId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FixedAsset_FixedAssetGroup_FixedAssetGroupId",
                table: "FixedAsset");

            migrationBuilder.DropIndex(
                name: "IX_FixedAsset_FixedAssetGroupId",
                table: "FixedAsset");
        }
    }
}
