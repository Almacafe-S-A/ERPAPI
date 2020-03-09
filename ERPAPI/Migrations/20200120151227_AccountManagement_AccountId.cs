using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class AccountManagement_AccountId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM CheckAccountLines", true);
            migrationBuilder.Sql("DELETE FROM CheckAccount", true);
            migrationBuilder.Sql("DELETE FROM AccountManagement", true);

            migrationBuilder.AddColumn<long>(
                name: "AccountId",
                table: "AccountManagement",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddForeignKey(
                name: "FK_AccountManagement_Accounting_AccountId",
                table: "AccountManagement",
                column: "AccountId",
                principalTable: "Accounting",
                principalColumn: "AccountId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccountManagement_Accounting_AccountId",
                table: "AccountManagement");

            migrationBuilder.DropColumn(
                name: "AccountId",
                table: "AccountManagement");
        }
    }
}
