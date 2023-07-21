using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class DebitNote_CAmbiodeStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "AprobadoEl",
                table: "DebitNote",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AprobadoPor",
                table: "DebitNote",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RevisadoEl",
                table: "DebitNote",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RevisadoPor",
                table: "DebitNote",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AprobadoEl",
                table: "DebitNote");

            migrationBuilder.DropColumn(
                name: "AprobadoPor",
                table: "DebitNote");

            migrationBuilder.DropColumn(
                name: "RevisadoEl",
                table: "DebitNote");

            migrationBuilder.DropColumn(
                name: "RevisadoPor",
                table: "DebitNote");
        }
    }
}
