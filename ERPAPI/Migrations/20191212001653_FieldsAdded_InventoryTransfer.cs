using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class FieldsAdded_InventoryTransfer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UnitOfMeasureId",
                table: "InventoryTransferLine",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "UnitOfMeasureName",
                table: "InventoryTransferLine",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CurrencyId",
                table: "InventoryTransfer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "CurrencyName",
                table: "InventoryTransfer",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaCreacion",
                table: "InventoryTransfer",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaModificacion",
                table: "InventoryTransfer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UsuarioCreacion",
                table: "InventoryTransfer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UsuarioModificacion",
                table: "InventoryTransfer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_InventoryTransferLine_UnitOfMeasureId",
                table: "InventoryTransferLine",
                column: "UnitOfMeasureId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryTransfer_CurrencyId",
                table: "InventoryTransfer",
                column: "CurrencyId");

            migrationBuilder.AddForeignKey(
                name: "FK_InventoryTransfer_Currency_CurrencyId",
                table: "InventoryTransfer",
                column: "CurrencyId",
                principalTable: "Currency",
                principalColumn: "CurrencyId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InventoryTransferLine_UnitOfMeasure_UnitOfMeasureId",
                table: "InventoryTransferLine",
                column: "UnitOfMeasureId",
                principalTable: "UnitOfMeasure",
                principalColumn: "UnitOfMeasureId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InventoryTransfer_Currency_CurrencyId",
                table: "InventoryTransfer");

            migrationBuilder.DropForeignKey(
                name: "FK_InventoryTransferLine_UnitOfMeasure_UnitOfMeasureId",
                table: "InventoryTransferLine");

            migrationBuilder.DropIndex(
                name: "IX_InventoryTransferLine_UnitOfMeasureId",
                table: "InventoryTransferLine");

            migrationBuilder.DropIndex(
                name: "IX_InventoryTransfer_CurrencyId",
                table: "InventoryTransfer");

            migrationBuilder.DropColumn(
                name: "UnitOfMeasureId",
                table: "InventoryTransferLine");

            migrationBuilder.DropColumn(
                name: "UnitOfMeasureName",
                table: "InventoryTransferLine");

            migrationBuilder.DropColumn(
                name: "CurrencyId",
                table: "InventoryTransfer");

            migrationBuilder.DropColumn(
                name: "CurrencyName",
                table: "InventoryTransfer");

            migrationBuilder.DropColumn(
                name: "FechaCreacion",
                table: "InventoryTransfer");

            migrationBuilder.DropColumn(
                name: "FechaModificacion",
                table: "InventoryTransfer");

            migrationBuilder.DropColumn(
                name: "UsuarioCreacion",
                table: "InventoryTransfer");

            migrationBuilder.DropColumn(
                name: "UsuarioModificacion",
                table: "InventoryTransfer");
        }
    }
}
