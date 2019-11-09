using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class WareHouse_Alerts_SalesOrder_TypeInvoice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "FechaHabilitacion",
                table: "Warehouse",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaLibertadGravamen",
                table: "Warehouse",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "TypeInvoiceId",
                table: "SalesOrder",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "TypeInvoiceName",
                table: "SalesOrder",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FechaHabilitacion",
                table: "Warehouse");

            migrationBuilder.DropColumn(
                name: "FechaLibertadGravamen",
                table: "Warehouse");

            migrationBuilder.DropColumn(
                name: "TypeInvoiceId",
                table: "SalesOrder");

            migrationBuilder.DropColumn(
                name: "TypeInvoiceName",
                table: "SalesOrder");
        }
    }
}
