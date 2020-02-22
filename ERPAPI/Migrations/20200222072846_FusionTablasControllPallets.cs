using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class FusionTablasControllPallets : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Alto",
                table: "ControlPallets",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Ancho",
                table: "ControlPallets",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "CenterCostId",
                table: "ControlPallets",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CenterCostName",
                table: "ControlPallets",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Otros",
                table: "ControlPallets",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Totallinea",
                table: "ControlPallets",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "cantidadPoliEtileno",
                table: "ControlPallets",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "cantidadYute",
                table: "ControlPallets",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Alto",
                table: "ControlPallets");

            migrationBuilder.DropColumn(
                name: "Ancho",
                table: "ControlPallets");

            migrationBuilder.DropColumn(
                name: "CenterCostId",
                table: "ControlPallets");

            migrationBuilder.DropColumn(
                name: "CenterCostName",
                table: "ControlPallets");

            migrationBuilder.DropColumn(
                name: "Otros",
                table: "ControlPallets");

            migrationBuilder.DropColumn(
                name: "Totallinea",
                table: "ControlPallets");

            migrationBuilder.DropColumn(
                name: "cantidadPoliEtileno",
                table: "ControlPallets");

            migrationBuilder.DropColumn(
                name: "cantidadYute",
                table: "ControlPallets");
        }
    }
}
