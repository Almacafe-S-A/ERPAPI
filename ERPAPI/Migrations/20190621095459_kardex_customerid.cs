using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class kardex_customerid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "CustomerId",
                table: "Kardex",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "CustomerName",
                table: "Kardex",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "SaldoProductoCertificado",
                table: "CustomerProduct",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "SaldoProductoTotal",
                table: "CustomerProduct",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "Kardex");

            migrationBuilder.DropColumn(
                name: "CustomerName",
                table: "Kardex");

            migrationBuilder.DropColumn(
                name: "SaldoProductoCertificado",
                table: "CustomerProduct");

            migrationBuilder.DropColumn(
                name: "SaldoProductoTotal",
                table: "CustomerProduct");
        }
    }
}
