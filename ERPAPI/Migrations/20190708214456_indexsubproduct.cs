﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class indexsubproduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ProductCode",
                table: "SubProduct",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SubProduct_ProductCode",
                table: "SubProduct",
                column: "ProductCode",
                unique: true,
                filter: "[ProductCode] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_SubProduct_ProductCode",
                table: "SubProduct");

            migrationBuilder.AlterColumn<string>(
                name: "ProductCode",
                table: "SubProduct",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
