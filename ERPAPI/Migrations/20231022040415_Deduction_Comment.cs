using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class Deduction_Comment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EsPorcentaje",
                table: "Deduction");

            migrationBuilder.DropColumn(
                name: "Formula",
                table: "Deduction");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "EsPorcentaje",
                table: "Deduction",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Formula",
                table: "Deduction",
                nullable: true);
        }
    }
}
