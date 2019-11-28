using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class Alert_ActionTakenNameVarType_string : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ActionTakenName",
                table: "Alert",
                nullable: true,
                oldClrType: typeof(long));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "ActionTakenName",
                table: "Alert",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
