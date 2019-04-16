using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class CotizacionesTotales : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Correo",
                table: "SalesOrder",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Direccion",
                table: "SalesOrder",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RTN",
                table: "SalesOrder",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Tefono",
                table: "SalesOrder",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "TotalExento",
                table: "SalesOrder",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "TotalExonerado",
                table: "SalesOrder",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "TotalGravado",
                table: "SalesOrder",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Correo",
                table: "SalesOrder");

            migrationBuilder.DropColumn(
                name: "Direccion",
                table: "SalesOrder");

            migrationBuilder.DropColumn(
                name: "RTN",
                table: "SalesOrder");

            migrationBuilder.DropColumn(
                name: "Tefono",
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
        }
    }
}
