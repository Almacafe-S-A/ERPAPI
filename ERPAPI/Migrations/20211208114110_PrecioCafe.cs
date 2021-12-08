using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class PrecioCafe : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DiferencialesUSD",
                table: "PrecioCafe");

            migrationBuilder.DropColumn(
                name: "TotalUSD",
                table: "PrecioCafe");

            migrationBuilder.AddColumn<string>(
                name: "Cosecha",
                table: "PrecioCafe",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "CustomerId",
                table: "PrecioCafe",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "ExchangeRateValue",
                table: "PrecioCafe",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Otros",
                table: "PrecioCafe",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "PrecioQQCalidadesInferiores",
                table: "PrecioCafe",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PrecioCafe_CustomerId",
                table: "PrecioCafe",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_PrecioCafe_Customer_CustomerId",
                table: "PrecioCafe",
                column: "CustomerId",
                principalTable: "Customer",
                principalColumn: "CustomerId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PrecioCafe_Customer_CustomerId",
                table: "PrecioCafe");

            migrationBuilder.DropIndex(
                name: "IX_PrecioCafe_CustomerId",
                table: "PrecioCafe");

            migrationBuilder.DropColumn(
                name: "Cosecha",
                table: "PrecioCafe");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "PrecioCafe");

            migrationBuilder.DropColumn(
                name: "ExchangeRateValue",
                table: "PrecioCafe");

            migrationBuilder.DropColumn(
                name: "Otros",
                table: "PrecioCafe");

            migrationBuilder.DropColumn(
                name: "PrecioQQCalidadesInferiores",
                table: "PrecioCafe");

            migrationBuilder.AddColumn<decimal>(
                name: "DiferencialesUSD",
                table: "PrecioCafe",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalUSD",
                table: "PrecioCafe",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
