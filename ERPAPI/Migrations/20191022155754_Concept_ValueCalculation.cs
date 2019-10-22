using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class Concept_ValueCalculation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Calculation",
                table: "Concept",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Value",
                table: "Concept",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Calculation",
                table: "Concept");

            migrationBuilder.DropColumn(
                name: "Value",
                table: "Concept");
        }
    }
}
