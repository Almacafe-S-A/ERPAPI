using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class ChangedNameColumnGoodsReceived : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Estado",
                table: "GoodsReceived");

            migrationBuilder.DropColumn(
                name: "ExpirationDate",
                table: "GoodsReceived");

            migrationBuilder.DropColumn(
                name: "IdEstado",
                table: "GoodsReceived");

            migrationBuilder.DropColumn(
                name: "Impreso",
                table: "GoodsReceived");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "GoodsReceived");

            migrationBuilder.RenameColumn(
                name: "Reference",
                table: "GoodsReceived",
                newName: "Motorista");

            migrationBuilder.RenameColumn(
                name: "ExitTicket",
                table: "GoodsReceived",
                newName: "BoletaSalidaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Motorista",
                table: "GoodsReceived",
                newName: "Reference");

            migrationBuilder.RenameColumn(
                name: "BoletaSalidaId",
                table: "GoodsReceived",
                newName: "ExitTicket");

            migrationBuilder.AddColumn<string>(
                name: "Estado",
                table: "GoodsReceived",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ExpirationDate",
                table: "GoodsReceived",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "IdEstado",
                table: "GoodsReceived",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Impreso",
                table: "GoodsReceived",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "GoodsReceived",
                nullable: true);
        }
    }
}
