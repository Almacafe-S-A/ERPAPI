using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class controlpalletsbrachname_productname : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BranchName",
                table: "ControlPallets",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProductName",
                table: "ControlPallets",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BranchName",
                table: "ControlPallets");

            migrationBuilder.DropColumn(
                name: "ProductName",
                table: "ControlPallets");
        }
    }
}
