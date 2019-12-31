﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class Material_Status : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Estado",
                table: "Material",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "IdEstado",
                table: "Material",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Estado",
                table: "Material");

            migrationBuilder.DropColumn(
                name: "IdEstado",
                table: "Material");
        }
    }
}
