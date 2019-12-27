using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class Remover_Columnas_Conciliacion_Linea : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccountId",
                table: "ConciliacionLinea");

            migrationBuilder.DropColumn(
                name: "AccountName",
                table: "ConciliacionLinea");

            migrationBuilder.AlterColumn<string>(
                name: "ReferenciaBancaria",
                table: "ConciliacionLinea",
                nullable: true,
                oldNullable: false
            );

            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaModificacion",
                table: "ConciliacionLinea",
                nullable: true,
                oldNullable: false
            );

            migrationBuilder.AlterColumn<string>(
                name: "UsuarioModificacion",
                table: "ConciliacionLinea",
                nullable: true,
                oldNullable: false
            );

            migrationBuilder.AlterColumn<long>(
                name: "CheknumberId",
                table: "ConciliacionLinea",
                nullable: true,
                oldNullable: false
            );

            migrationBuilder.AlterColumn<long>(
                name: "JournalEntryId",
                table: "ConciliacionLinea",
                nullable: true,
                oldNullable: false
            );

            migrationBuilder.AlterColumn<long>(
                name: "JournalEntryLineId",
                table: "ConciliacionLinea",
                nullable: true,
                oldNullable: false
            );

            migrationBuilder.AlterColumn<string>(
                name: "ReferenceTrans",
                table: "ConciliacionLinea",
                nullable: true,
                oldNullable: false
            );

            migrationBuilder.AlterColumn<DateTime>(
                name: "TransDate",
                table: "ConciliacionLinea",
                nullable: true,
                oldNullable: false
            );

            migrationBuilder.AlterColumn<long>(
                name: "VoucherTypeId",
                table: "ConciliacionLinea",
                nullable: true,
                oldNullable: false
            );

            migrationBuilder.AlterColumn<long>(
                name: "MotivoId",
                table: "ConciliacionLinea",
                nullable: true,
                oldNullable: false
            );

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "AccountId",
                nullable:false,
                table: "ConciliacionLinea");

            migrationBuilder.AddColumn<string>(
                name: "AccountName",
                table: "ConciliacionLinea",
                nullable:false);

            migrationBuilder.AlterColumn<string>(
                name: "ReferenciaBancaria",
                table: "ConciliacionLinea",
                nullable: false,
                oldNullable: true
            );

            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaModificacion",
                table: "ConciliacionLinea",
                nullable: false,
                oldNullable: true
            );

            migrationBuilder.AlterColumn<string>(
                name: "UsuarioModificacion",
                table: "ConciliacionLinea",
                nullable: false,
                oldNullable: true
            );

            migrationBuilder.AlterColumn<long>(
                name: "CheknumberId",
                table: "ConciliacionLinea",
                nullable: false,
                oldNullable: true
            );

            migrationBuilder.AlterColumn<long>(
                name: "JournalEntryId",
                table: "ConciliacionLinea",
                nullable: false,
                oldNullable: true
            );

            migrationBuilder.AlterColumn<long>(
                name: "JournalEntryLineId",
                table: "ConciliacionLinea",
                nullable: false,
                oldNullable: true
            );

            migrationBuilder.AlterColumn<string>(
                name: "ReferenceTrans",
                table: "ConciliacionLinea",
                nullable: false,
                oldNullable: true
            );

            migrationBuilder.AlterColumn<DateTime>(
                name: "TransDate",
                table: "ConciliacionLinea",
                nullable: false,
                oldNullable: true
            );

            migrationBuilder.AlterColumn<long>(
                name: "VoucherTypeId",
                table: "ConciliacionLinea",
                nullable: false,
                oldNullable: true
            );

            migrationBuilder.AlterColumn<long>(
                name: "MotivoId",
                table: "ConciliacionLinea",
                nullable: false,
                oldNullable: true
            );
        }
    }
}
