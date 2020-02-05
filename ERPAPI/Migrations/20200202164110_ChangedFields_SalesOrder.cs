using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class ChangedFields_SalesOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Porcentaje",
                table: "SalesOrder");

            migrationBuilder.DropColumn(
                name: "Valor",
                table: "SalesOrder");

            migrationBuilder.AddColumn<decimal>(
                name: "Porcentaje",
                table: "SalesOrderLine",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Valor",
                table: "SalesOrderLine",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Porcentaje",
                table: "SalesOrderLine");

            migrationBuilder.DropColumn(
                name: "Valor",
                table: "SalesOrderLine");

            migrationBuilder.AddColumn<decimal>(
                name: "Porcentaje",
                table: "SalesOrder",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Valor",
                table: "SalesOrder",
                nullable: true);
        }
    }
}
