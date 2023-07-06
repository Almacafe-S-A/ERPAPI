using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class TBL_Notificaton_Update2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Action",
                table: "Notifications",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Controller",
                table: "Notifications",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Icono",
                table: "Notifications",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Titulo",
                table: "Notifications",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Action",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "Controller",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "Icono",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "Titulo",
                table: "Notifications");
        }
    }
}
