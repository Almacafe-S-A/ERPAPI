using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class subproduct_merma : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Merma",
                table: "SubProduct",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "UnitOfMeasureName",
                table: "SubProduct",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Merma",
                table: "SubProduct");

            migrationBuilder.DropColumn(
                name: "UnitOfMeasureName",
                table: "SubProduct");
        }
    }
}
