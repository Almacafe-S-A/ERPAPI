﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class Addefieldsendosos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Estado",
                table: "EndososCertificados",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EstadoId",
                table: "EndososCertificados",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Estado",
                table: "EndososCertificados");

            migrationBuilder.DropColumn(
                name: "EstadoId",
                table: "EndososCertificados");
        }
    }
}
