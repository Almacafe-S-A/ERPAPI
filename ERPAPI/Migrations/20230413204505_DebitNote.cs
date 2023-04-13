using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class DebitNote : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Caja",
                table: "DebitNote");

            migrationBuilder.DropColumn(
                name: "CertificadoDepositoId",
                table: "DebitNote");

            migrationBuilder.DropColumn(
                name: "Currency",
                table: "DebitNote");

            migrationBuilder.DropColumn(
                name: "CurrencyId",
                table: "DebitNote");

            migrationBuilder.DropColumn(
                name: "CurrencyName",
                table: "DebitNote");

            migrationBuilder.DropColumn(
                name: "CustomerRefNumber",
                table: "DebitNote");

            migrationBuilder.DropColumn(
                name: "DeliveryDate",
                table: "DebitNote");

            migrationBuilder.DropColumn(
                name: "ExpirationDate",
                table: "DebitNote");

            migrationBuilder.DropColumn(
                name: "Freight",
                table: "DebitNote");

            migrationBuilder.DropColumn(
                name: "Impreso",
                table: "DebitNote");

            migrationBuilder.DropColumn(
                name: "OrderDate",
                table: "DebitNote");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "DebitNote");

            migrationBuilder.DropColumn(
                name: "ProductName",
                table: "DebitNote");

            migrationBuilder.DropColumn(
                name: "SalesOrderId",
                table: "DebitNote");

            migrationBuilder.DropColumn(
                name: "SubProductId",
                table: "DebitNote");

            migrationBuilder.DropColumn(
                name: "Tax18",
                table: "DebitNote");

            migrationBuilder.DropColumn(
                name: "TotalExento",
                table: "DebitNote");

            migrationBuilder.DropColumn(
                name: "TotalExonerado",
                table: "DebitNote");

            migrationBuilder.DropColumn(
                name: "TotalGravado",
                table: "DebitNote");

            migrationBuilder.DropColumn(
                name: "TotalGravado18",
                table: "DebitNote");

            migrationBuilder.RenameColumn(
                name: "SubProductName",
                table: "DebitNote",
                newName: "Descripcion");

            migrationBuilder.RenameColumn(
                name: "ShipmentId",
                table: "DebitNote",
                newName: "InvoiceId");

            migrationBuilder.RenameColumn(
                name: "DebitNoteTypeId",
                table: "DebitNote",
                newName: "DiasVencimiento");

            migrationBuilder.CreateIndex(
                name: "IX_DebitNote_InvoiceId",
                table: "DebitNote",
                column: "InvoiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_DebitNote_Invoice_InvoiceId",
                table: "DebitNote",
                column: "InvoiceId",
                principalTable: "Invoice",
                principalColumn: "InvoiceId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DebitNote_Invoice_InvoiceId",
                table: "DebitNote");

            migrationBuilder.DropIndex(
                name: "IX_DebitNote_InvoiceId",
                table: "DebitNote");

            migrationBuilder.RenameColumn(
                name: "InvoiceId",
                table: "DebitNote",
                newName: "ShipmentId");

            migrationBuilder.RenameColumn(
                name: "DiasVencimiento",
                table: "DebitNote",
                newName: "DebitNoteTypeId");

            migrationBuilder.RenameColumn(
                name: "Descripcion",
                table: "DebitNote",
                newName: "SubProductName");

            migrationBuilder.AddColumn<string>(
                name: "Caja",
                table: "DebitNote",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "CertificadoDepositoId",
                table: "DebitNote",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<decimal>(
                name: "Currency",
                table: "DebitNote",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "CurrencyId",
                table: "DebitNote",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "CurrencyName",
                table: "DebitNote",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CustomerRefNumber",
                table: "DebitNote",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeliveryDate",
                table: "DebitNote",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ExpirationDate",
                table: "DebitNote",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<decimal>(
                name: "Freight",
                table: "DebitNote",
                type: "Money",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "Impreso",
                table: "DebitNote",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "OrderDate",
                table: "DebitNote",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<long>(
                name: "ProductId",
                table: "DebitNote",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "ProductName",
                table: "DebitNote",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "SalesOrderId",
                table: "DebitNote",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "SubProductId",
                table: "DebitNote",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Tax18",
                table: "DebitNote",
                type: "Money",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalExento",
                table: "DebitNote",
                type: "Money",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalExonerado",
                table: "DebitNote",
                type: "Money",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalGravado",
                table: "DebitNote",
                type: "Money",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalGravado18",
                table: "DebitNote",
                type: "Money",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
