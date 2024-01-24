using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class codigopais : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "State",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "Country",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "City",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Code",
                table: "State");

            migrationBuilder.DropColumn(
                name: "Code",
                table: "Country");

            migrationBuilder.DropColumn(
                name: "Code",
                table: "City");
        }
    }
}
