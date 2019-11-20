using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class DocumentDate_Subservicios : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DocumentDate",
                table: "SubServicesWareHouse",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Estado",
                table: "SubServicesWareHouse",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "IdEstado",
                table: "SubServicesWareHouse",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DocumentDate",
                table: "SubServicesWareHouse");

            migrationBuilder.DropColumn(
                name: "Estado",
                table: "SubServicesWareHouse");

            migrationBuilder.DropColumn(
                name: "IdEstado",
                table: "SubServicesWareHouse");
        }
    }
}
