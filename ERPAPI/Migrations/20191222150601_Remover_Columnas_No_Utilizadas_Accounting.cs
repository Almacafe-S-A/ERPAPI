using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class Remover_Columnas_No_Utilizadas_Accounting : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccountClasses",
                table: "Accounting");

            migrationBuilder.DropColumn(
                name: "IsContraAccount",
                table: "Accounting");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AccountClasses",
                table: "Accounting",
                nullable: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsCountraAccount",
                table: "Accounting",
                nullable: false);
        }
    }
}
