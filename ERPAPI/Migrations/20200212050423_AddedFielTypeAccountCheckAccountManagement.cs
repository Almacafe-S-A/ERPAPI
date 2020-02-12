using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class AddedFielTypeAccountCheckAccountManagement : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "TypeAccountId",
                table: "AccountManagement",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AccountManagement_TypeAccountId",
                table: "AccountManagement",
                column: "TypeAccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_AccountManagement_ElementoConfiguracion_TypeAccountId",
                table: "AccountManagement",
                column: "TypeAccountId",
                principalTable: "ElementoConfiguracion",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccountManagement_ElementoConfiguracion_TypeAccountId",
                table: "AccountManagement");

            migrationBuilder.DropIndex(
                name: "IX_AccountManagement_TypeAccountId",
                table: "AccountManagement");

            migrationBuilder.DropColumn(
                name: "TypeAccountId",
                table: "AccountManagement");
        }
    }
}
