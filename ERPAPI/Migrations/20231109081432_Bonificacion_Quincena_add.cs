using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class Bonificacion_Quincena_add : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NombreQuincena",
                table: "Bonificaciones",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "Quincena",
                table: "Bonificaciones",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NombreQuincena",
                table: "Bonificaciones");

            migrationBuilder.DropColumn(
                name: "Quincena",
                table: "Bonificaciones");
        }
    }
}
