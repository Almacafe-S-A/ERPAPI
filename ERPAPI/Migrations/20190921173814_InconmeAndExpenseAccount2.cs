using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class InconmeAndExpenseAccount2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "FechaCreacion",
                table: "IncomeAndExpensesAccount",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaModificacion",
                table: "IncomeAndExpensesAccount",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UsuarioCreacion",
                table: "IncomeAndExpensesAccount",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UsuarioEjecucion",
                table: "IncomeAndExpensesAccount",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UsuarioModificacion",
                table: "IncomeAndExpensesAccount",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FechaCreacion",
                table: "IncomeAndExpensesAccount");

            migrationBuilder.DropColumn(
                name: "FechaModificacion",
                table: "IncomeAndExpensesAccount");

            migrationBuilder.DropColumn(
                name: "UsuarioCreacion",
                table: "IncomeAndExpensesAccount");

            migrationBuilder.DropColumn(
                name: "UsuarioEjecucion",
                table: "IncomeAndExpensesAccount");

            migrationBuilder.DropColumn(
                name: "UsuarioModificacion",
                table: "IncomeAndExpensesAccount");
        }
    }
}
