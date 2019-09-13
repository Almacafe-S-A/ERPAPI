using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class ExchangeRate_CurrencyId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "CurrencyId",
                table: "ExchangeRate",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "City",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ExchangeRate_CurrencyId",
                table: "ExchangeRate",
                column: "CurrencyId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExchangeRate_Currency_CurrencyId",
                table: "ExchangeRate",
                column: "CurrencyId",
                principalTable: "Currency",
                principalColumn: "CurrencyId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExchangeRate_Currency_CurrencyId",
                table: "ExchangeRate");

            migrationBuilder.DropIndex(
                name: "IX_ExchangeRate_CurrencyId",
                table: "ExchangeRate");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "City");

            migrationBuilder.AlterColumn<long>(
                name: "CurrencyId",
                table: "ExchangeRate",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);
        }
    }
}
