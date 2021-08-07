using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class Mod_ContratosClientes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BandaTransportadora",
                table: "CustomerContract");

            migrationBuilder.DropColumn(
                name: "Correo2",
                table: "CustomerContract");

            migrationBuilder.DropColumn(
                name: "Correo3",
                table: "CustomerContract");

            migrationBuilder.DropColumn(
                name: "ExtraHours",
                table: "CustomerContract");

            migrationBuilder.DropColumn(
                name: "FoodPayment",
                table: "CustomerContract");

            migrationBuilder.DropColumn(
                name: "LatePayment",
                table: "CustomerContract");

            migrationBuilder.DropColumn(
                name: "MontaCargas",
                table: "CustomerContract");

            migrationBuilder.DropColumn(
                name: "MulasHidraulicas",
                table: "CustomerContract");

            migrationBuilder.DropColumn(
                name: "Porcentaje1",
                table: "CustomerContract");

            migrationBuilder.DropColumn(
                name: "Porcentaje2",
                table: "CustomerContract");

            migrationBuilder.DropColumn(
                name: "Transport",
                table: "CustomerContract");

            migrationBuilder.DropColumn(
                name: "UsedArea",
                table: "CustomerContract");

            migrationBuilder.DropColumn(
                name: "Valor1",
                table: "CustomerContract");

            migrationBuilder.DropColumn(
                name: "Valor2",
                table: "CustomerContract");

            migrationBuilder.DropColumn(
                name: "ValueBascula",
                table: "CustomerContract");

            migrationBuilder.DropColumn(
                name: "ValueCD",
                table: "CustomerContract");

            migrationBuilder.DropColumn(
                name: "ValueSecure",
                table: "CustomerContract");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "BandaTransportadora",
                table: "CustomerContract",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "Correo2",
                table: "CustomerContract",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Correo3",
                table: "CustomerContract",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "ExtraHours",
                table: "CustomerContract",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "FoodPayment",
                table: "CustomerContract",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "LatePayment",
                table: "CustomerContract",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "MontaCargas",
                table: "CustomerContract",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "MulasHidraulicas",
                table: "CustomerContract",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Porcentaje1",
                table: "CustomerContract",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Porcentaje2",
                table: "CustomerContract",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Transport",
                table: "CustomerContract",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "UsedArea",
                table: "CustomerContract",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Valor1",
                table: "CustomerContract",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Valor2",
                table: "CustomerContract",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "ValueBascula",
                table: "CustomerContract",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "ValueCD",
                table: "CustomerContract",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "ValueSecure",
                table: "CustomerContract",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
