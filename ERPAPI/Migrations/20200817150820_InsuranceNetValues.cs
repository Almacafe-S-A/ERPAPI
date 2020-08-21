using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class InsuranceNetValues : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InsuredDiference",
                table: "InsuredAssets");

            migrationBuilder.DropColumn(
                name: "MerchadiseTotalValue",
                table: "InsuredAssets");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "InsuredDiference",
                table: "InsuredAssets",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "MerchadiseTotalValue",
                table: "InsuredAssets",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
