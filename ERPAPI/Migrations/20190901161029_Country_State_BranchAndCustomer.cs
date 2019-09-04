using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class Country_State_BranchAndCustomer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CityId",
                table: "Customer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CountryId",
                table: "Customer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "CountryName",
                table: "Customer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StateId",
                table: "Customer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CityId",
                table: "Branch",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CountryId",
                table: "Branch",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "CountryName",
                table: "Branch",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StateId",
                table: "Branch",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CityId",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "CountryId",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "CountryName",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "StateId",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "CityId",
                table: "Branch");

            migrationBuilder.DropColumn(
                name: "CountryId",
                table: "Branch");

            migrationBuilder.DropColumn(
                name: "CountryName",
                table: "Branch");

            migrationBuilder.DropColumn(
                name: "StateId",
                table: "Branch");
        }
    }
}
