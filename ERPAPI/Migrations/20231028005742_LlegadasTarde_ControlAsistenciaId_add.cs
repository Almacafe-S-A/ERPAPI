using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class LlegadasTarde_ControlAsistenciaId_add : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ControlAsistenciaId",
                table: "LlegadasTardeBiometrico",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Dia",
                table: "LlegadasTardeBiometrico",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "Fecha",
                table: "LlegadasTardeBiometrico",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<string>(
                name: "Nombre",
                table: "Feriados",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.CreateIndex(
                name: "IX_LlegadasTardeBiometrico_ControlAsistenciaId",
                table: "LlegadasTardeBiometrico",
                column: "ControlAsistenciaId");

            migrationBuilder.AddForeignKey(
                name: "FK_LlegadasTardeBiometrico_ControlAsistencias_ControlAsistenciaId",
                table: "LlegadasTardeBiometrico",
                column: "ControlAsistenciaId",
                principalTable: "ControlAsistencias",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LlegadasTardeBiometrico_ControlAsistencias_ControlAsistenciaId",
                table: "LlegadasTardeBiometrico");

            migrationBuilder.DropIndex(
                name: "IX_LlegadasTardeBiometrico_ControlAsistenciaId",
                table: "LlegadasTardeBiometrico");

            migrationBuilder.DropColumn(
                name: "ControlAsistenciaId",
                table: "LlegadasTardeBiometrico");

            migrationBuilder.DropColumn(
                name: "Dia",
                table: "LlegadasTardeBiometrico");

            migrationBuilder.DropColumn(
                name: "Fecha",
                table: "LlegadasTardeBiometrico");

            migrationBuilder.AlterColumn<string>(
                name: "Nombre",
                table: "Feriados",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
