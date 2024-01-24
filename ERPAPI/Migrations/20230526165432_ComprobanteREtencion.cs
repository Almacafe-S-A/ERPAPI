using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class ComprobanteREtencion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdEmpleado",
                table: "RetentionReceipt");

            migrationBuilder.DropColumn(
                name: "IdPuntoEmision",
                table: "RetentionReceipt");

            migrationBuilder.RenameColumn(
                name: "NoCorrelativo",
                table: "RetentionReceipt",
                newName: "RangoEmision");

            migrationBuilder.AlterColumn<string>(
                name: "NumeroDEI",
                table: "RetentionReceipt",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaLimiteEmision",
                table: "RetentionReceipt",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NoCorrelativoDocumento",
                table: "RetentionReceipt",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FechaLimiteEmision",
                table: "RetentionReceipt");

            migrationBuilder.DropColumn(
                name: "NoCorrelativoDocumento",
                table: "RetentionReceipt");

            migrationBuilder.RenameColumn(
                name: "RangoEmision",
                table: "RetentionReceipt",
                newName: "NoCorrelativo");

            migrationBuilder.AlterColumn<int>(
                name: "NumeroDEI",
                table: "RetentionReceipt",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<long>(
                name: "IdEmpleado",
                table: "RetentionReceipt",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "IdPuntoEmision",
                table: "RetentionReceipt",
                nullable: false,
                defaultValue: 0L);
        }
    }
}
