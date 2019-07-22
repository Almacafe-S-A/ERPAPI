using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class CustomerContract_Almacenaje : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "BandaTransportadora",
                table: "CustomerContract",
                nullable: false,
                defaultValue: 0.0);

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
                name: "Transport",
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

            migrationBuilder.AddColumn<string>(
                name: "WareHouses",
                table: "CustomerContract",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BandaTransportadora",
                table: "CustomerContract");

            migrationBuilder.DropColumn(
                name: "ExtraHours",
                table: "CustomerContract");

            migrationBuilder.DropColumn(
                name: "FoodPayment",
                table: "CustomerContract");

            migrationBuilder.DropColumn(
                name: "Transport",
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

            migrationBuilder.DropColumn(
                name: "WareHouses",
                table: "CustomerContract");
        }
    }
}
