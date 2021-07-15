using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class ModelChangeSalesOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Amount",
                table: "SalesOrder");

            migrationBuilder.DropColumn(
                name: "Currency",
                table: "SalesOrder");

            migrationBuilder.DropColumn(
                name: "CurrencyId",
                table: "SalesOrder");

            migrationBuilder.DropColumn(
                name: "DeliveryDate",
                table: "SalesOrder");

            migrationBuilder.DropColumn(
                name: "Discount",
                table: "SalesOrder");

            migrationBuilder.DropColumn(
                name: "Freight",
                table: "SalesOrder");

            migrationBuilder.DropColumn(
                name: "SubTotal",
                table: "SalesOrder");

            migrationBuilder.DropColumn(
                name: "Tax",
                table: "SalesOrder");

            migrationBuilder.DropColumn(
                name: "Tax18",
                table: "SalesOrder");

            migrationBuilder.DropColumn(
                name: "Total",
                table: "SalesOrder");

            migrationBuilder.DropColumn(
                name: "TotalExento",
                table: "SalesOrder");

            migrationBuilder.DropColumn(
                name: "TotalExonerado",
                table: "SalesOrder");

            migrationBuilder.DropColumn(
                name: "TotalGravado",
                table: "SalesOrder");

            migrationBuilder.DropColumn(
                name: "TotalGravado18",
                table: "SalesOrder");

            migrationBuilder.RenameColumn(
                name: "PrecioBase",
                table: "SalesOrder",
                newName: "PrecioServicio");

            migrationBuilder.RenameColumn(
                name: "CurrencyName",
                table: "SalesOrder",
                newName: "UnitOfMeasureName");

            migrationBuilder.AddColumn<string>(
                name: "Customer",
                table: "SalesOrder",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "PrecioBaseProducto",
                table: "SalesOrder",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Representante",
                table: "SalesOrder",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UnitOfMeasureId",
                table: "SalesOrder",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Customer",
                table: "SalesOrder");

            migrationBuilder.DropColumn(
                name: "PrecioBaseProducto",
                table: "SalesOrder");

            migrationBuilder.DropColumn(
                name: "Representante",
                table: "SalesOrder");

            migrationBuilder.DropColumn(
                name: "UnitOfMeasureId",
                table: "SalesOrder");

            migrationBuilder.RenameColumn(
                name: "UnitOfMeasureName",
                table: "SalesOrder",
                newName: "CurrencyName");

            migrationBuilder.RenameColumn(
                name: "PrecioServicio",
                table: "SalesOrder",
                newName: "PrecioBase");

            migrationBuilder.AddColumn<decimal>(
                name: "Amount",
                table: "SalesOrder",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Currency",
                table: "SalesOrder",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "CurrencyId",
                table: "SalesOrder",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeliveryDate",
                table: "SalesOrder",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<decimal>(
                name: "Discount",
                table: "SalesOrder",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Freight",
                table: "SalesOrder",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "SubTotal",
                table: "SalesOrder",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Tax",
                table: "SalesOrder",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Tax18",
                table: "SalesOrder",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Total",
                table: "SalesOrder",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalExento",
                table: "SalesOrder",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalExonerado",
                table: "SalesOrder",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalGravado",
                table: "SalesOrder",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalGravado18",
                table: "SalesOrder",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
