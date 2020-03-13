using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class RemoveFieldCDWarehouse : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WarehouseId",
                table: "CertificadoDeposito");

            migrationBuilder.DropColumn(
                name: "WarehouseName",
                table: "CertificadoDeposito");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
           

            migrationBuilder.AddColumn<long>(
                name: "WarehouseId",
                table: "CertificadoDeposito",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "WarehouseName",
                table: "CertificadoDeposito",
                nullable: true);
        }
    }
}
