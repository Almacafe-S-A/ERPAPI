using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class Cambios_Conciliacion_Bancaria : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "SaldoBanco",
                table: "Conciliacion"
            );

            migrationBuilder.AddColumn<decimal>(
                name: "SaldoLibro",
                table: "Conciliacion"
            );

            migrationBuilder.AlterColumn<decimal>(
                name: "SaldoConciliado",
                table: "Conciliacion"
            );

            migrationBuilder.AddForeignKey(
                name:"FK_Conciliacion_Bank",
                table:"Conciliacion",
                column:"BankId",
                principalTable:"Bank",
                principalColumn:"BankId",
                onDelete: ReferentialAction.Restrict
            );

            migrationBuilder.AddForeignKey(
                name: "FK_Conciliacion_Accounting",
                table: "Conciliacion",
                column: "AccountId",
                principalTable: "Accounting",
                principalColumn: "AccountId",
                onDelete:ReferentialAction.Restrict);

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SaldoBanco",
                table: "Conciliacion"
            );

            migrationBuilder.DropColumn(
                name:"SaldoLibro",
                table:"Conciliacion"
            );

            migrationBuilder.AlterColumn<float>(
                name: "SaldoConciliado",
                table: "Conciliacion"
                );

            migrationBuilder.DropForeignKey(
                name: "FK_Conciliacion_Bank",
                table: "Conciliacion");

            migrationBuilder.DropForeignKey(
                name: "FK_Conciliacion_Accounting",
                table: "Conciliacion");
        }
    }
}
