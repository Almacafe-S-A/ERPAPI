using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class calculofactura : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Estado",
                table: "InvoiceCalculation",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "IdEstado",
                table: "InvoiceCalculation",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<Guid>(
                name: "Identificador",
                table: "InvoiceCalculation",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Quantity",
                table: "InvoiceCalculation",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Estado",
                table: "InvoiceCalculation");

            migrationBuilder.DropColumn(
                name: "IdEstado",
                table: "InvoiceCalculation");

            migrationBuilder.DropColumn(
                name: "Identificador",
                table: "InvoiceCalculation");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "InvoiceCalculation");
        }
    }
}
