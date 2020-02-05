using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class AddedAuditoria_City : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Estado",
                table: "City",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaCreacion",
                table: "City",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaModificacion",
                table: "City",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "IdEstado",
                table: "City",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UsuarioCreacion",
                table: "City",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UsuarioModificacion",
                table: "City",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_City_IdEstado",
                table: "City",
                column: "IdEstado");

            migrationBuilder.AddForeignKey(
                name: "FK_City_Estados_IdEstado",
                table: "City",
                column: "IdEstado",
                principalTable: "Estados",
                principalColumn: "IdEstado",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_City_Estados_IdEstado",
                table: "City");

            migrationBuilder.DropIndex(
                name: "IX_City_IdEstado",
                table: "City");

            migrationBuilder.DropColumn(
                name: "Estado",
                table: "City");

            migrationBuilder.DropColumn(
                name: "FechaCreacion",
                table: "City");

            migrationBuilder.DropColumn(
                name: "FechaModificacion",
                table: "City");

            migrationBuilder.DropColumn(
                name: "IdEstado",
                table: "City");

            migrationBuilder.DropColumn(
                name: "UsuarioCreacion",
                table: "City");

            migrationBuilder.DropColumn(
                name: "UsuarioModificacion",
                table: "City");
        }
    }
}
