using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class ComprobanteRetencionDocumentoComprobante : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CAIDocumento",
                table: "RetentionReceipt",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaLimiteDocumento",
                table: "RetentionReceipt",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CAIDocumento",
                table: "RetentionReceipt");

            migrationBuilder.DropColumn(
                name: "FechaLimiteDocumento",
                table: "RetentionReceipt");
        }
    }
}
