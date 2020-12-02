 using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class subproduct_controlPalletsLine : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SubPriductId",
                table: "ControlPalletsLine");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "SubPriductId",
                table: "ControlPalletsLine",
                nullable: true);
        }
    }
}
