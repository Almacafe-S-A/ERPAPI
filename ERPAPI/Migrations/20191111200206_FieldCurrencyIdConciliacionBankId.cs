using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class FieldCurrencyIdConciliacionBankId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Conciliacion_Bank_IdBanco",
                table: "Conciliacion");

            migrationBuilder.DropIndex(
                name: "IX_Conciliacion_IdBanco",
                table: "Conciliacion");

            migrationBuilder.DropColumn(
                name: "IdBanco",
                table: "Conciliacion");

            migrationBuilder.AddColumn<long>(
                name: "BankId",
                table: "Conciliacion",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BankId",
                table: "Conciliacion");

            migrationBuilder.AddColumn<long>(
                name: "IdBanco",
                table: "Conciliacion",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Conciliacion_IdBanco",
                table: "Conciliacion",
                column: "IdBanco");

            migrationBuilder.AddForeignKey(
                name: "FK_Conciliacion_Bank_IdBanco",
                table: "Conciliacion",
                column: "IdBanco",
                principalTable: "Bank",
                principalColumn: "BankId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
