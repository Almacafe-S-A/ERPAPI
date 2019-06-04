using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class NumeracionSAR_AddFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "DocSubTypeId",
                table: "NumeracionSAR",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "DocTypeId",
                table: "NumeracionSAR",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "IdPuntoEmision",
                table: "NumeracionSAR",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DocSubTypeId",
                table: "NumeracionSAR");

            migrationBuilder.DropColumn(
                name: "DocTypeId",
                table: "NumeracionSAR");

            migrationBuilder.DropColumn(
                name: "IdPuntoEmision",
                table: "NumeracionSAR");
        }
    }
}
