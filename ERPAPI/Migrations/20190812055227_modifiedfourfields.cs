using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class modifiedfourfields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "FechaCreacion",
                table: "GeneralLedgerLine",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaModificacion",
                table: "GeneralLedgerLine",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "UsuarioCreacion",
                table: "GeneralLedgerLine",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UsuarioModificacion",
                table: "GeneralLedgerLine",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaCreacion",
                table: "GeneralLedgerHeader",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaModificacion",
                table: "GeneralLedgerHeader",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "UsuarioCreacion",
                table: "GeneralLedgerHeader",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UsuarioModificacion",
                table: "GeneralLedgerHeader",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaCreacion",
                table: "AccountClass",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaModificacion",
                table: "AccountClass",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "UsuarioCreacion",
                table: "AccountClass",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UsuarioModificacion",
                table: "AccountClass",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaCreacion",
                table: "Account",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaModificacion",
                table: "Account",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "UsuarioCreacion",
                table: "Account",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UsuarioModificacion",
                table: "Account",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FechaCreacion",
                table: "GeneralLedgerLine");

            migrationBuilder.DropColumn(
                name: "FechaModificacion",
                table: "GeneralLedgerLine");

            migrationBuilder.DropColumn(
                name: "UsuarioCreacion",
                table: "GeneralLedgerLine");

            migrationBuilder.DropColumn(
                name: "UsuarioModificacion",
                table: "GeneralLedgerLine");

            migrationBuilder.DropColumn(
                name: "FechaCreacion",
                table: "GeneralLedgerHeader");

            migrationBuilder.DropColumn(
                name: "FechaModificacion",
                table: "GeneralLedgerHeader");

            migrationBuilder.DropColumn(
                name: "UsuarioCreacion",
                table: "GeneralLedgerHeader");

            migrationBuilder.DropColumn(
                name: "UsuarioModificacion",
                table: "GeneralLedgerHeader");

            migrationBuilder.DropColumn(
                name: "FechaCreacion",
                table: "AccountClass");

            migrationBuilder.DropColumn(
                name: "FechaModificacion",
                table: "AccountClass");

            migrationBuilder.DropColumn(
                name: "UsuarioCreacion",
                table: "AccountClass");

            migrationBuilder.DropColumn(
                name: "UsuarioModificacion",
                table: "AccountClass");

            migrationBuilder.DropColumn(
                name: "FechaCreacion",
                table: "Account");

            migrationBuilder.DropColumn(
                name: "FechaModificacion",
                table: "Account");

            migrationBuilder.DropColumn(
                name: "UsuarioCreacion",
                table: "Account");

            migrationBuilder.DropColumn(
                name: "UsuarioModificacion",
                table: "Account");
        }
    }
}
