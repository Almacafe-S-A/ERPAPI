using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class ScheduleServices : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "FechaCreacion",
                table: "ScheduleSubservices",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaModificacion",
                table: "ScheduleSubservices",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "UsuarioCreacion",
                table: "ScheduleSubservices",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UsuarioModificacion",
                table: "ScheduleSubservices",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FechaCreacion",
                table: "ScheduleSubservices");

            migrationBuilder.DropColumn(
                name: "FechaModificacion",
                table: "ScheduleSubservices");

            migrationBuilder.DropColumn(
                name: "UsuarioCreacion",
                table: "ScheduleSubservices");

            migrationBuilder.DropColumn(
                name: "UsuarioModificacion",
                table: "ScheduleSubservices");
        }
    }
}
