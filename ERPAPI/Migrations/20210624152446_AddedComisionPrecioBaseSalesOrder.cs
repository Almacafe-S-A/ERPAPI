using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class AddedComisionPrecioBaseSalesOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "ComisionMax",
                table: "SalesOrder",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "ComisionMin",
                table: "SalesOrder",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "PrecioBase",
                table: "SalesOrder",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ComisionMax",
                table: "SalesOrder");

            migrationBuilder.DropColumn(
                name: "ComisionMin",
                table: "SalesOrder");

            migrationBuilder.DropColumn(
                name: "PrecioBase",
                table: "SalesOrder");
        }
    }
}
