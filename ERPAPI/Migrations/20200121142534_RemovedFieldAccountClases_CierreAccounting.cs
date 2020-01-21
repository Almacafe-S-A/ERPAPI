using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class RemovedFieldAccountClases_CierreAccounting : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccountClasses",
                table: "CierresAccounting");

            migrationBuilder.DropColumn(
                name: "IsContraAccount",
                table: "CierresAccounting");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AccountClasses",
                table: "CierresAccounting",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsContraAccount",
                table: "CierresAccounting",
                nullable: false,
                defaultValue: false);
        }
    }
}
