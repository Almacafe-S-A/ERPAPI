using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class NumeracionSARId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "IdCAI",
                table: "NumeracionSAR",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdCAI",
                table: "NumeracionSAR");
        }
    }
}
