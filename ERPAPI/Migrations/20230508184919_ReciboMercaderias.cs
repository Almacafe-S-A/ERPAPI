using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class ReciboMercaderias : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FechaCreacion",
                table: "GoodsDeliveredLine");

            migrationBuilder.DropColumn(
                name: "FechaModificacion",
                table: "GoodsDeliveredLine");

            migrationBuilder.DropColumn(
                name: "UsuarioCreacion",
                table: "GoodsDeliveredLine");

            migrationBuilder.DropColumn(
                name: "UsuarioModificacion",
                table: "GoodsDeliveredLine");

            migrationBuilder.AlterColumn<int>(
                name: "BoletaPesoId",
                table: "GoodsDelivered",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<long>(
                name: "WeightBallot",
                table: "BoletaDeSalida",
                nullable: true,
                oldClrType: typeof(long));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "FechaCreacion",
                table: "GoodsDeliveredLine",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaModificacion",
                table: "GoodsDeliveredLine",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UsuarioCreacion",
                table: "GoodsDeliveredLine",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UsuarioModificacion",
                table: "GoodsDeliveredLine",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "BoletaPesoId",
                table: "GoodsDelivered",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "WeightBallot",
                table: "BoletaDeSalida",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);
        }
    }
}
