using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class AddedField_ExhangeRateCompra : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {


            migrationBuilder.AddColumn<decimal>(
                name: "ExchangeRateValueCompra",
                table: "ExchangeRate",
                type: "decimal(18,4)",
                nullable: false,
                defaultValue: 0m);

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {


            migrationBuilder.DropColumn(
                name: "ExchangeRateValueCompra",
                table: "ExchangeRate");

          
        }
    }
}
