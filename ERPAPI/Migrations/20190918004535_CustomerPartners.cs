using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class CustomerPartners : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CargoPublico",
                table: "CustomerPartners",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "FuncionarioPublico",
                table: "CustomerPartners",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CargoPublico",
                table: "CustomerPartners");

            migrationBuilder.DropColumn(
                name: "FuncionarioPublico",
                table: "CustomerPartners");
        }
    }
}
