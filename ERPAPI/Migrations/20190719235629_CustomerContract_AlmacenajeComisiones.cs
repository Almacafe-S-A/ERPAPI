using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class CustomerContract_AlmacenajeComisiones : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Porcentaje1",
                table: "CustomerContract",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Porcentaje2",
                table: "CustomerContract",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Valor1",
                table: "CustomerContract",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Valor2",
                table: "CustomerContract",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Porcentaje1",
                table: "CustomerContract");

            migrationBuilder.DropColumn(
                name: "Porcentaje2",
                table: "CustomerContract");

            migrationBuilder.DropColumn(
                name: "Valor1",
                table: "CustomerContract");

            migrationBuilder.DropColumn(
                name: "Valor2",
                table: "CustomerContract");
        }
    }
}
