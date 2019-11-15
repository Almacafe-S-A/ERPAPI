using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class ScheduleService_Condiciones : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "LogicalConditionId",
                table: "ScheduleSubservices",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "ServiceName",
                table: "ScheduleSubservices",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SubServiceName",
                table: "ScheduleSubservices",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LogicalConditionId",
                table: "ScheduleSubservices");

            migrationBuilder.DropColumn(
                name: "ServiceName",
                table: "ScheduleSubservices");

            migrationBuilder.DropColumn(
                name: "SubServiceName",
                table: "ScheduleSubservices");
        }
    }
}
