using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class TBL_NotificationsUpdate_1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaLectura",
                table: "Notifications",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AddColumn<string>(
                name: "PermisoLectura",
                table: "Notifications",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UsuarioCreacion",
                table: "Notifications",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UsuarioLectura",
                table: "Notifications",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PermisoLectura",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "UsuarioCreacion",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "UsuarioLectura",
                table: "Notifications");

            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaLectura",
                table: "Notifications",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);
        }
    }
}
