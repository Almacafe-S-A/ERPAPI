using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class CreditoNote_AccountCustomer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "DocumentoId",
                table: "CustomerAcccountStatus",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "TipoDocumento",
                table: "CustomerAcccountStatus",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "TipoDocumentoId",
                table: "CustomerAcccountStatus",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "AprobadoEl",
                table: "CreditNote",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AprobadoPor",
                table: "CreditNote",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "JournalEntryId",
                table: "CreditNote",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RevisadoEl",
                table: "CreditNote",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RevisadoPor",
                table: "CreditNote",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CustomerAcccountStatus_TipoDocumentoId",
                table: "CustomerAcccountStatus",
                column: "TipoDocumentoId");

            migrationBuilder.CreateIndex(
                name: "IX_CreditNote_JournalEntryId",
                table: "CreditNote",
                column: "JournalEntryId");

            migrationBuilder.AddForeignKey(
                name: "FK_CreditNote_JournalEntry_JournalEntryId",
                table: "CreditNote",
                column: "JournalEntryId",
                principalTable: "JournalEntry",
                principalColumn: "JournalEntryId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerAcccountStatus_TiposDocumento_TipoDocumentoId",
                table: "CustomerAcccountStatus",
                column: "TipoDocumentoId",
                principalTable: "TiposDocumento",
                principalColumn: "IdTipoDocumento",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CreditNote_JournalEntry_JournalEntryId",
                table: "CreditNote");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerAcccountStatus_TiposDocumento_TipoDocumentoId",
                table: "CustomerAcccountStatus");

            migrationBuilder.DropIndex(
                name: "IX_CustomerAcccountStatus_TipoDocumentoId",
                table: "CustomerAcccountStatus");

            migrationBuilder.DropIndex(
                name: "IX_CreditNote_JournalEntryId",
                table: "CreditNote");

            migrationBuilder.DropColumn(
                name: "DocumentoId",
                table: "CustomerAcccountStatus");

            migrationBuilder.DropColumn(
                name: "TipoDocumento",
                table: "CustomerAcccountStatus");

            migrationBuilder.DropColumn(
                name: "TipoDocumentoId",
                table: "CustomerAcccountStatus");

            migrationBuilder.DropColumn(
                name: "AprobadoEl",
                table: "CreditNote");

            migrationBuilder.DropColumn(
                name: "AprobadoPor",
                table: "CreditNote");

            migrationBuilder.DropColumn(
                name: "JournalEntryId",
                table: "CreditNote");

            migrationBuilder.DropColumn(
                name: "RevisadoEl",
                table: "CreditNote");

            migrationBuilder.DropColumn(
                name: "RevisadoPor",
                table: "CreditNote");
        }
    }
}
