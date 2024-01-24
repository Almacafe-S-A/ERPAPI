using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class CuentaBancariInvoiceDebitNoe : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "Bank",
                table: "Invoice",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "BankName",
                table: "Invoice",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CuentaBancaria",
                table: "Invoice",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "CuentaBancariaId",
                table: "Invoice",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "Bank",
                table: "DebitNote",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "BankName",
                table: "DebitNote",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CuentaBancaria",
                table: "DebitNote",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "CuentaBancariaId",
                table: "DebitNote",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Invoice_CuentaBancariaId",
                table: "Invoice",
                column: "CuentaBancariaId");

            migrationBuilder.CreateIndex(
                name: "IX_DebitNote_CuentaBancariaId",
                table: "DebitNote",
                column: "CuentaBancariaId");

            migrationBuilder.AddForeignKey(
                name: "FK_DebitNote_AccountManagement_CuentaBancariaId",
                table: "DebitNote",
                column: "CuentaBancariaId",
                principalTable: "AccountManagement",
                principalColumn: "AccountManagementId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Invoice_AccountManagement_CuentaBancariaId",
                table: "Invoice",
                column: "CuentaBancariaId",
                principalTable: "AccountManagement",
                principalColumn: "AccountManagementId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DebitNote_AccountManagement_CuentaBancariaId",
                table: "DebitNote");

            migrationBuilder.DropForeignKey(
                name: "FK_Invoice_AccountManagement_CuentaBancariaId",
                table: "Invoice");

            migrationBuilder.DropIndex(
                name: "IX_Invoice_CuentaBancariaId",
                table: "Invoice");

            migrationBuilder.DropIndex(
                name: "IX_DebitNote_CuentaBancariaId",
                table: "DebitNote");

            migrationBuilder.DropColumn(
                name: "Bank",
                table: "Invoice");

            migrationBuilder.DropColumn(
                name: "BankName",
                table: "Invoice");

            migrationBuilder.DropColumn(
                name: "CuentaBancaria",
                table: "Invoice");

            migrationBuilder.DropColumn(
                name: "CuentaBancariaId",
                table: "Invoice");

            migrationBuilder.DropColumn(
                name: "Bank",
                table: "DebitNote");

            migrationBuilder.DropColumn(
                name: "BankName",
                table: "DebitNote");

            migrationBuilder.DropColumn(
                name: "CuentaBancaria",
                table: "DebitNote");

            migrationBuilder.DropColumn(
                name: "CuentaBancariaId",
                table: "DebitNote");
        }
    }
}
