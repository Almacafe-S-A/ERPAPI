using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class EmployeeExtraHoursForeingKeyCustomerId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.AddColumn<long>(
            //    name: "CustomerId",
            //    table: "EmployeeExtraHours",
            //    nullable: false,
            //    defaultValue: 0L);

            //migrationBuilder.CreateIndex(
            //    name: "IX_EmployeeExtraHours_CustomerId",
            //    table: "EmployeeExtraHours",
            //    column: "CustomerId");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_EmployeeExtraHours_Customer_CustomerId",
            //    table: "EmployeeExtraHours",
            //    column: "CustomerId",
            //    principalTable: "Customer",
            //    principalColumn: "CustomerId",
            //    onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_EmployeeExtraHours_Customer_CustomerId",
            //    table: "EmployeeExtraHours");

            //migrationBuilder.DropIndex(
            //    name: "IX_EmployeeExtraHours_CustomerId",
            //    table: "EmployeeExtraHours");

            //migrationBuilder.DropColumn(
            //    name: "CustomerId",
            //    table: "EmployeeExtraHours");
        }
    }
}
