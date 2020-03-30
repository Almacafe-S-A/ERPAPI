using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class AddedFieldsProbabilidadRiesgo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ColorHexadecimal",
                table: "ProbabilidadRiesgo",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "Nivel",
                table: "ProbabilidadRiesgo",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ColorHexadecimal",
                table: "ProbabilidadRiesgo");

            migrationBuilder.DropColumn(
                name: "Nivel",
                table: "ProbabilidadRiesgo");
        }
    }
}
