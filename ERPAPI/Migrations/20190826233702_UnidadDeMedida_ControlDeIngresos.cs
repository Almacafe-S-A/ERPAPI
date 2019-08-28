using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class UnidadDeMedida_ControlDeIngresos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "UnitOfMeasureId",
                table: "ControlPallets",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "UnitOfMeasureName",
                table: "ControlPallets",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UnitOfMeasureId",
                table: "ControlPallets");

            migrationBuilder.DropColumn(
                name: "UnitOfMeasureName",
                table: "ControlPallets");
        }
    }
}
