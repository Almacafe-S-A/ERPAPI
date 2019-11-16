using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class ScheduleSubservices_Alimentacion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Almuerzo",
                table: "ScheduleSubservices",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Cena",
                table: "ScheduleSubservices",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Desayuno",
                table: "ScheduleSubservices",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Transporte",
                table: "ScheduleSubservices",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "Motivo",
                table: "EmployeeExtraHours",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Almuerzo",
                table: "ScheduleSubservices");

            migrationBuilder.DropColumn(
                name: "Cena",
                table: "ScheduleSubservices");

            migrationBuilder.DropColumn(
                name: "Desayuno",
                table: "ScheduleSubservices");

            migrationBuilder.DropColumn(
                name: "Transporte",
                table: "ScheduleSubservices");

            migrationBuilder.DropColumn(
                name: "Motivo",
                table: "EmployeeExtraHours");
        }
    }
}
