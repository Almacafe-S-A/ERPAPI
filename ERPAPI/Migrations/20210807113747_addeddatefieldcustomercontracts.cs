using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class addeddatefieldcustomercontracts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DelegateSalary",
                table: "CustomerContract");

            migrationBuilder.DropColumn(
                name: "Mercancias",
                table: "CustomerContract");

            migrationBuilder.DropColumn(
                name: "Resolution",
                table: "CustomerContract");

            migrationBuilder.DropColumn(
                name: "WarehouseRequirements",
                table: "CustomerContract");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "DelegateSalary",
                table: "CustomerContract",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "Mercancias",
                table: "CustomerContract",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Resolution",
                table: "CustomerContract",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WarehouseRequirements",
                table: "CustomerContract",
                nullable: true);
        }
    }
}
