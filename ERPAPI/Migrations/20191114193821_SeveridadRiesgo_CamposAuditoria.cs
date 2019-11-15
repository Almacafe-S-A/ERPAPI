using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class SeveridadRiesgo_CamposAuditoria : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "FechaCreacion",
                table: "SeveridadRiesgo",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaModificacion",
                table: "SeveridadRiesgo",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "UsuarioCreacion",
                table: "SeveridadRiesgo",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UsuarioModificacion",
                table: "SeveridadRiesgo",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FechaCreacion",
                table: "SeveridadRiesgo");

            migrationBuilder.DropColumn(
                name: "FechaModificacion",
                table: "SeveridadRiesgo");

            migrationBuilder.DropColumn(
                name: "UsuarioCreacion",
                table: "SeveridadRiesgo");

            migrationBuilder.DropColumn(
                name: "UsuarioModificacion",
                table: "SeveridadRiesgo");
        }
    }
}
