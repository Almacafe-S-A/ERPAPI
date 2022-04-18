using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class EliminarCamposAuditoriaDetalleRecibo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FechaCreacion",
                table: "GoodsReceivedLine");

            migrationBuilder.DropColumn(
                name: "FechaModificacion",
                table: "GoodsReceivedLine");

            migrationBuilder.DropColumn(
                name: "UsuarioCreacion",
                table: "GoodsReceivedLine");

            migrationBuilder.DropColumn(
                name: "UsuarioModificacion",
                table: "GoodsReceivedLine");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "FechaCreacion",
                table: "GoodsReceivedLine",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaModificacion",
                table: "GoodsReceivedLine",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UsuarioCreacion",
                table: "GoodsReceivedLine",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UsuarioModificacion",
                table: "GoodsReceivedLine",
                nullable: true);
        }
    }
}
