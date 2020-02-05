using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class EmployeesExtraHours : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM EmployeeExtraHours");

            migrationBuilder.Sql("DELETE FROM EmployeeExtraHoursDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeExtraHoursDetail_EmployeeExtraHours_EmployeeExtraHoursId",
                table: "EmployeeExtraHoursDetail");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeExtraHoursDetail_EmployeeExtraHoursId",
                table: "EmployeeExtraHoursDetail");

            migrationBuilder.AddColumn<long>(
                name: "CustomerId",
                table: "EmployeeExtraHours",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "CustomerName",
                table: "EmployeeExtraHours",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EndTime",
                table: "EmployeeExtraHours",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<decimal>(
                name: "HourlySalary",
                table: "EmployeeExtraHours",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "QuantityHours",
                table: "EmployeeExtraHours",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartTime",
                table: "EmployeeExtraHours",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeExtraHours_CustomerId",
                table: "EmployeeExtraHours",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeExtraHours_EmployeeId",
                table: "EmployeeExtraHours",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeExtraHours_Customer_CustomerId",
                table: "EmployeeExtraHours",
                column: "CustomerId",
                principalTable: "Customer",
                principalColumn: "CustomerId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeExtraHours_Employees_EmployeeId",
                table: "EmployeeExtraHours",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "IdEmpleado",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeExtraHours_Customer_CustomerId",
                table: "EmployeeExtraHours");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeExtraHours_Employees_EmployeeId",
                table: "EmployeeExtraHours");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeExtraHours_CustomerId",
                table: "EmployeeExtraHours");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeExtraHours_EmployeeId",
                table: "EmployeeExtraHours");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "EmployeeExtraHours");

            migrationBuilder.DropColumn(
                name: "CustomerName",
                table: "EmployeeExtraHours");

            migrationBuilder.DropColumn(
                name: "EndTime",
                table: "EmployeeExtraHours");

            migrationBuilder.DropColumn(
                name: "HourlySalary",
                table: "EmployeeExtraHours");

            migrationBuilder.DropColumn(
                name: "QuantityHours",
                table: "EmployeeExtraHours");

            migrationBuilder.DropColumn(
                name: "StartTime",
                table: "EmployeeExtraHours");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeExtraHoursDetail_EmployeeExtraHoursId",
                table: "EmployeeExtraHoursDetail",
                column: "EmployeeExtraHoursId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeExtraHoursDetail_EmployeeExtraHours_EmployeeExtraHoursId",
                table: "EmployeeExtraHoursDetail",
                column: "EmployeeExtraHoursId",
                principalTable: "EmployeeExtraHours",
                principalColumn: "EmployeeExtraHoursId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
