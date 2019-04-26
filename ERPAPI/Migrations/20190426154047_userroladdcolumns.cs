using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class userroladdcolumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "FechaCreacion",
                table: "Product",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaModificacion",
                table: "Product",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "UsuarioCreacion",
                table: "Product",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UsuarioModificacion",
                table: "Product",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaCreacion",
                table: "Customer",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaModificacion",
                table: "Customer",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "UsuarioCreacion",
                table: "Customer",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UsuarioModificacion",
                table: "Customer",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaCreacion",
                table: "AspNetUserRoles",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaModificacion",
                table: "AspNetUserRoles",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "UsuarioCreacion",
                table: "AspNetUserRoles",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UsuarioModificacion",
                table: "AspNetUserRoles",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FechaCreacion",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "FechaModificacion",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "UsuarioCreacion",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "UsuarioModificacion",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "FechaCreacion",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "FechaModificacion",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "UsuarioCreacion",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "UsuarioModificacion",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "FechaCreacion",
                table: "AspNetUserRoles");

            migrationBuilder.DropColumn(
                name: "FechaModificacion",
                table: "AspNetUserRoles");

            migrationBuilder.DropColumn(
                name: "UsuarioCreacion",
                table: "AspNetUserRoles");

            migrationBuilder.DropColumn(
                name: "UsuarioModificacion",
                table: "AspNetUserRoles");
        }
    }
}
