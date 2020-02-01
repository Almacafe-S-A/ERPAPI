using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class AddedFields_SalesOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Observacion",
                table: "SalesOrder",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Porcentaje",
                table: "SalesOrder",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Valor",
                table: "SalesOrder",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SubProductName",
                table: "CustomerConditions",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Observacion",
                table: "SalesOrder");

            migrationBuilder.DropColumn(
                name: "Porcentaje",
                table: "SalesOrder");

            migrationBuilder.DropColumn(
                name: "Valor",
                table: "SalesOrder");

            migrationBuilder.DropColumn(
                name: "SubProductName",
                table: "CustomerConditions");
        }
    }
}
