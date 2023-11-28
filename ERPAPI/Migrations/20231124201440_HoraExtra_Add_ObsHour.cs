using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class HoraExtra_Add_ObsHour : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "HoraEntrada",
                table: "HorasExtrasBiometrico",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HoraSalida",
                table: "HorasExtrasBiometrico",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Observaciones",
                table: "HorasExtrasBiometrico",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HoraEntrada",
                table: "HorasExtrasBiometrico");

            migrationBuilder.DropColumn(
                name: "HoraSalida",
                table: "HorasExtrasBiometrico");

            migrationBuilder.DropColumn(
                name: "Observaciones",
                table: "HorasExtrasBiometrico");
        }
    }
}
