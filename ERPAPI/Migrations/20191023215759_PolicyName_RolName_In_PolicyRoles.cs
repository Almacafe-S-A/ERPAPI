using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class PolicyName_RolName_In_PolicyRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PolicyName",
                table: "PolicyRoles",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RolName",
                table: "PolicyRoles",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PolicyName",
                table: "PolicyRoles");

            migrationBuilder.DropColumn(
                name: "RolName",
                table: "PolicyRoles");
        }
    }
}
