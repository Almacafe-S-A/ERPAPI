using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class Employees_CommissionPeriodicity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ApplyCommission",
                table: "Employees",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<long>(
                name: "ComisionId",
                table: "Employees",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "CommissionName",
                table: "Employees",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "PeriodicidadId",
                table: "Employees",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "PeriodicityName",
                table: "Employees",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApplyCommission",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "ComisionId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "CommissionName",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "PeriodicidadId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "PeriodicityName",
                table: "Employees");
        }
    }
}
