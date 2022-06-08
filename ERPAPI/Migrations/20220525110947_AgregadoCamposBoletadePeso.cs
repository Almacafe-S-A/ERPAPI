using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class AgregadoCamposBoletadePeso : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Barco",
                table: "Boleto_Ent",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Destino",
                table: "Boleto_Ent",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MarcaVehiculo",
                table: "Boleto_Ent",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Orden",
                table: "Boleto_Ent",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "PesoPuerto",
                table: "Boleto_Ent",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "Tranportista",
                table: "Boleto_Ent",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Barco",
                table: "Boleto_Ent");

            migrationBuilder.DropColumn(
                name: "Destino",
                table: "Boleto_Ent");

            migrationBuilder.DropColumn(
                name: "MarcaVehiculo",
                table: "Boleto_Ent");

            migrationBuilder.DropColumn(
                name: "Orden",
                table: "Boleto_Ent");

            migrationBuilder.DropColumn(
                name: "PesoPuerto",
                table: "Boleto_Ent");

            migrationBuilder.DropColumn(
                name: "Tranportista",
                table: "Boleto_Ent");
        }
    }
}
