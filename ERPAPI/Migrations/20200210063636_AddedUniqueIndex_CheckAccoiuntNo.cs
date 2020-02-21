using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class AddedUniqueIndex_CheckAccoiuntNo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "CheckAccountNo",
                table: "CheckAccount",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CheckAccount_CheckAccountNo",
                table: "CheckAccount",
                column: "CheckAccountNo",
                unique: true,
                filter: "[CheckAccountNo] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CheckAccount_CheckAccountNo",
                table: "CheckAccount");

            migrationBuilder.AlterColumn<string>(
                name: "CheckAccountNo",
                table: "CheckAccount",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
