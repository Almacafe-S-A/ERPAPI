using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class Salary_Schedule : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "ScheduleSubservices",
                nullable: true,
                oldClrType: typeof(double));

            migrationBuilder.AddColumn<string>(
                name: "Estado",
                table: "ScheduleSubservices",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "FactorHora",
                table: "ScheduleSubservices",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<long>(
                name: "IdEstado",
                table: "ScheduleSubservices",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<double>(
                name: "HourlySalary",
                table: "EmployeeExtraHoursDetail",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Estado",
                table: "ScheduleSubservices");

            migrationBuilder.DropColumn(
                name: "FactorHora",
                table: "ScheduleSubservices");

            migrationBuilder.DropColumn(
                name: "IdEstado",
                table: "ScheduleSubservices");

            migrationBuilder.DropColumn(
                name: "HourlySalary",
                table: "EmployeeExtraHoursDetail");

            migrationBuilder.AlterColumn<double>(
                name: "Description",
                table: "ScheduleSubservices",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
