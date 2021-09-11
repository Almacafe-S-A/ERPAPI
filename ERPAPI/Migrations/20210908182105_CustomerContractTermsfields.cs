using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class CustomerContractTermsfields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "FechaCreacion",
                table: "CustomerContractTerms",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaModificacion",
                table: "CustomerContractTerms",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Servicio",
                table: "CustomerContractTerms",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UsuarioCreacion",
                table: "CustomerContractTerms",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UsuarioModificacion",
                table: "CustomerContractTerms",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "CustomerContractId",
                table: "CustomerContractLinesTerms",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CustomerContractLinesTerms_CustomerContractId",
                table: "CustomerContractLinesTerms",
                column: "CustomerContractId");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerContractLinesTerms_CustomerContract_CustomerContractId",
                table: "CustomerContractLinesTerms",
                column: "CustomerContractId",
                principalTable: "CustomerContract",
                principalColumn: "CustomerContractId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerContractLinesTerms_CustomerContract_CustomerContractId",
                table: "CustomerContractLinesTerms");

            migrationBuilder.DropIndex(
                name: "IX_CustomerContractLinesTerms_CustomerContractId",
                table: "CustomerContractLinesTerms");

            migrationBuilder.DropColumn(
                name: "FechaCreacion",
                table: "CustomerContractTerms");

            migrationBuilder.DropColumn(
                name: "FechaModificacion",
                table: "CustomerContractTerms");

            migrationBuilder.DropColumn(
                name: "Servicio",
                table: "CustomerContractTerms");

            migrationBuilder.DropColumn(
                name: "UsuarioCreacion",
                table: "CustomerContractTerms");

            migrationBuilder.DropColumn(
                name: "UsuarioModificacion",
                table: "CustomerContractTerms");

            migrationBuilder.DropColumn(
                name: "CustomerContractId",
                table: "CustomerContractLinesTerms");
        }
    }
}
