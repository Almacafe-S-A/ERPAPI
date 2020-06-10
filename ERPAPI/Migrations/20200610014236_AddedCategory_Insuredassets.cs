using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class AddedCategory_Insuredassets : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "CategoryId",
                table: "InsuredAssets",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_InsuredAssets_CategoryId",
                table: "InsuredAssets",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_InsuredAssets_ElementoConfiguracion_CategoryId",
                table: "InsuredAssets",
                column: "CategoryId",
                principalTable: "ElementoConfiguracion",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InsuredAssets_ElementoConfiguracion_CategoryId",
                table: "InsuredAssets");

            migrationBuilder.DropIndex(
                name: "IX_InsuredAssets_CategoryId",
                table: "InsuredAssets");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "InsuredAssets");
        }
    }
}
