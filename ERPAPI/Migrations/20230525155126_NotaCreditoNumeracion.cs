using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class NotaCreditoNumeracion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NúmeroDEI",
                table: "CreditNote");

            migrationBuilder.AddColumn<string>(
                name: "NumeroDEI",
                table: "CreditNote",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumeroDEI",
                table: "CreditNote");

            migrationBuilder.AddColumn<int>(
                name: "NúmeroDEI",
                table: "CreditNote",
                nullable: false,
                defaultValue: 0);
        }
    }
}
