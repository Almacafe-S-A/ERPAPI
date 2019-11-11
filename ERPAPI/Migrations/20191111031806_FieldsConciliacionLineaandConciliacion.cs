using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class FieldsConciliacionLineaandConciliacion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "CheknumberId",
                table: "ConciliacionLinea",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "JournalEntryId",
                table: "ConciliacionLinea",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "JournalEntryLineId",
                table: "ConciliacionLinea",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<bool>(
                name: "Reconciled",
                table: "ConciliacionLinea",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ReferenceTrans",
                table: "ConciliacionLinea",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "TransDate",
                table: "ConciliacionLinea",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<long>(
                name: "VoucherTypeId",
                table: "ConciliacionLinea",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "CheckAccountId",
                table: "Conciliacion",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CheknumberId",
                table: "ConciliacionLinea");

            migrationBuilder.DropColumn(
                name: "JournalEntryId",
                table: "ConciliacionLinea");

            migrationBuilder.DropColumn(
                name: "JournalEntryLineId",
                table: "ConciliacionLinea");

            migrationBuilder.DropColumn(
                name: "Reconciled",
                table: "ConciliacionLinea");

            migrationBuilder.DropColumn(
                name: "ReferenceTrans",
                table: "ConciliacionLinea");

            migrationBuilder.DropColumn(
                name: "TransDate",
                table: "ConciliacionLinea");

            migrationBuilder.DropColumn(
                name: "VoucherTypeId",
                table: "ConciliacionLinea");

            migrationBuilder.DropColumn(
                name: "CheckAccountId",
                table: "Conciliacion");
        }
    }
}
