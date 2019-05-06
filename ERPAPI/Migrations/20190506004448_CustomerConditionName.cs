using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class CustomerConditionName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ClientConditionName",
                table: "CustomerConditions",
                newName: "CustomerConditionName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CustomerConditionName",
                table: "CustomerConditions",
                newName: "ClientConditionName");
        }
    }
}
