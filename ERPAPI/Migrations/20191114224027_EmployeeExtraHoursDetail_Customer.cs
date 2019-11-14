using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class EmployeeExtraHoursDetail_Customer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "CustomerId",
                table: "EmployeeExtraHoursDetail",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "CustomerName",
                table: "EmployeeExtraHoursDetail",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "EmployeeExtraHoursDetail");

            migrationBuilder.DropColumn(
                name: "CustomerName",
                table: "EmployeeExtraHoursDetail");
        }
    }
}
