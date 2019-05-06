using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class SalesOrderBranchName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "BranchName",
                table: "SalesOrder",
                nullable: true,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "BranchName",
                table: "SalesOrder",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
