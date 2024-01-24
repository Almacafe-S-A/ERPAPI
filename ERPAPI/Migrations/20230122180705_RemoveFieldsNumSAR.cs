using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class RemoveFieldsNumSAR : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DocSubType",
                table: "NumeracionSAR");

            migrationBuilder.DropColumn(
                name: "DocSubTypeId",
                table: "NumeracionSAR");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DocSubType",
                table: "NumeracionSAR",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "DocSubTypeId",
                table: "NumeracionSAR",
                nullable: false,
                defaultValue: 0L);
        }
    }
}
