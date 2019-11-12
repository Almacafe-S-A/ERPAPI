using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class FieldCurrencyIdConciliacionLinea : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ConciliacionLinea_Currency_IdMoneda",
                table: "ConciliacionLinea");

            migrationBuilder.DropIndex(
                name: "IX_ConciliacionLinea_IdMoneda",
                table: "ConciliacionLinea");

            migrationBuilder.DropColumn(
                name: "IdMoneda",
                table: "ConciliacionLinea");

            migrationBuilder.AddColumn<int>(
                name: "CurrencyId",
                table: "ConciliacionLinea",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrencyId",
                table: "ConciliacionLinea");

            migrationBuilder.AddColumn<int>(
                name: "IdMoneda",
                table: "ConciliacionLinea",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ConciliacionLinea_IdMoneda",
                table: "ConciliacionLinea",
                column: "IdMoneda");

            migrationBuilder.AddForeignKey(
                name: "FK_ConciliacionLinea_Currency_IdMoneda",
                table: "ConciliacionLinea",
                column: "IdMoneda",
                principalTable: "Currency",
                principalColumn: "CurrencyId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
