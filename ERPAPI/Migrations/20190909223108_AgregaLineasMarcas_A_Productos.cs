﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class AgregaLineasMarcas_A_Productos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GrupoId",
                table: "Product",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LineaId",
                table: "Product",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MarcaId",
                table: "Product",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Product_GrupoId",
                table: "Product",
                column: "GrupoId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_LineaId",
                table: "Product",
                column: "LineaId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_MarcaId",
                table: "Product",
                column: "MarcaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Grupos_GrupoId",
                table: "Product",
                column: "GrupoId",
                principalTable: "Grupos",
                principalColumn: "GrupoId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Lineas_LineaId",
                table: "Product",
                column: "LineaId",
                principalTable: "Lineas",
                principalColumn: "LineaId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Marcas_MarcaId",
                table: "Product",
                column: "MarcaId",
                principalTable: "Marcas",
                principalColumn: "MarcaId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_Grupos_GrupoId",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_Lineas_LineaId",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_Marcas_MarcaId",
                table: "Product");

            migrationBuilder.DropIndex(
                name: "IX_Product_GrupoId",
                table: "Product");

            migrationBuilder.DropIndex(
                name: "IX_Product_LineaId",
                table: "Product");

            migrationBuilder.DropIndex(
                name: "IX_Product_MarcaId",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "GrupoId",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "LineaId",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "MarcaId",
                table: "Product");
        }
    }
}
