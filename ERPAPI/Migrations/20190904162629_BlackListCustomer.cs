using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class BlackListCustomer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccountBalance",
                table: "Account");

            migrationBuilder.AddColumn<long>(
                name: "CityId",
                table: "BlackListCustomers",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "CityName",
                table: "BlackListCustomers",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "CountryId",
                table: "BlackListCustomers",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "CountryName",
                table: "BlackListCustomers",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "StateId",
                table: "BlackListCustomers",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "StateName",
                table: "BlackListCustomers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CityId",
                table: "BlackListCustomers");

            migrationBuilder.DropColumn(
                name: "CityName",
                table: "BlackListCustomers");

            migrationBuilder.DropColumn(
                name: "CountryId",
                table: "BlackListCustomers");

            migrationBuilder.DropColumn(
                name: "CountryName",
                table: "BlackListCustomers");

            migrationBuilder.DropColumn(
                name: "StateId",
                table: "BlackListCustomers");

            migrationBuilder.DropColumn(
                name: "StateName",
                table: "BlackListCustomers");

            migrationBuilder.AddColumn<double>(
                name: "AccountBalance",
                table: "Account",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
