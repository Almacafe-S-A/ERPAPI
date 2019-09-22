using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class Invoice_InvoiceLines : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SalesOrderLine_Invoice_InvoiceId",
                table: "SalesOrderLine");

            migrationBuilder.DropIndex(
                name: "IX_SalesOrderLine_InvoiceId",
                table: "SalesOrderLine");

            migrationBuilder.DropColumn(
                name: "InvoiceId",
                table: "SalesOrderLine");

            migrationBuilder.AddColumn<DateTime>(
                name: "ExpirationDate",
                table: "Invoice",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExpirationDate",
                table: "Invoice");

            migrationBuilder.AddColumn<int>(
                name: "InvoiceId",
                table: "SalesOrderLine",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SalesOrderLine_InvoiceId",
                table: "SalesOrderLine",
                column: "InvoiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_SalesOrderLine_Invoice_InvoiceId",
                table: "SalesOrderLine",
                column: "InvoiceId",
                principalTable: "Invoice",
                principalColumn: "InvoiceId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
