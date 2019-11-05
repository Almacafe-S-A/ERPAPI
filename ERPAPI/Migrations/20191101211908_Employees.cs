using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class Employees : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaFinContrato",
                table: "Employees",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AddColumn<long>(
                name: "IdTipoPlanilla",
                table: "Employees",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Profesion",
                table: "Employees",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RTN",
                table: "Employees",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_IdTipoPlanilla",
                table: "Employees",
                column: "IdTipoPlanilla");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_TipoPlanillas_IdTipoPlanilla",
                table: "Employees",
                column: "IdTipoPlanilla",
                principalTable: "TipoPlanillas",
                principalColumn: "IdTipoPlanilla",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_TipoPlanillas_IdTipoPlanilla",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_IdTipoPlanilla",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "IdTipoPlanilla",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "Profesion",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "RTN",
                table: "Employees");

            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaFinContrato",
                table: "Employees",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);
        }
    }
}
