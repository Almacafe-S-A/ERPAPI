using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class WarehouseCapacidad : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "CapacidadBodega",
                table: "Warehouse",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "UnitOfMeasureId",
                table: "Warehouse",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "UnitOfMeasureName",
                table: "Warehouse",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CapacidadBodega",
                table: "Warehouse");

            migrationBuilder.DropColumn(
                name: "UnitOfMeasureId",
                table: "Warehouse");

            migrationBuilder.DropColumn(
                name: "UnitOfMeasureName",
                table: "Warehouse");
        }
    }
}
