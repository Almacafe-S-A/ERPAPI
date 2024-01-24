using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class CamposFeriados : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Domingo",
                table: "Horarios",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Factor",
                table: "Horarios",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Jueves",
                table: "Horarios",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Lunes",
                table: "Horarios",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Martes",
                table: "Horarios",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Miercoles",
                table: "Horarios",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Sabado",
                table: "Horarios",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Viernes",
                table: "Horarios",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Domingo",
                table: "Horarios");

            migrationBuilder.DropColumn(
                name: "Factor",
                table: "Horarios");

            migrationBuilder.DropColumn(
                name: "Jueves",
                table: "Horarios");

            migrationBuilder.DropColumn(
                name: "Lunes",
                table: "Horarios");

            migrationBuilder.DropColumn(
                name: "Martes",
                table: "Horarios");

            migrationBuilder.DropColumn(
                name: "Miercoles",
                table: "Horarios");

            migrationBuilder.DropColumn(
                name: "Sabado",
                table: "Horarios");

            migrationBuilder.DropColumn(
                name: "Viernes",
                table: "Horarios");
        }
    }
}
