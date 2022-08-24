using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class InformacionMediaticaNoReFCustomers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "BlackListCustomers");

            migrationBuilder.DropColumn(
                name: "CustomerName",
                table: "BlackListCustomers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "CustomerId",
                table: "BlackListCustomers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CustomerName",
                table: "BlackListCustomers",
                nullable: true);
        }
    }
}
