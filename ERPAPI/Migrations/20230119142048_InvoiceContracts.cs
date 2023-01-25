using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class InvoiceContracts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CertificadoDepositoId",
                table: "Invoice");

            migrationBuilder.DropColumn(
                name: "ProformaInvoiceId",
                table: "Invoice");

            migrationBuilder.DropColumn(
                name: "SalesOrderId",
                table: "Invoice");

            migrationBuilder.AlterColumn<long>(
                name: "IdEstado",
                table: "Invoice",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AlterColumn<int>(
                name: "CurrencyId",
                table: "Invoice",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<long>(
                name: "CustomerContractId",
                table: "Invoice",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Invoice_CustomerContractId",
                table: "Invoice",
                column: "CustomerContractId");

            migrationBuilder.AddForeignKey(
                name: "FK_Invoice_CustomerContract_CustomerContractId",
                table: "Invoice",
                column: "CustomerContractId",
                principalTable: "CustomerContract",
                principalColumn: "CustomerContractId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invoice_CustomerContract_CustomerContractId",
                table: "Invoice");

            migrationBuilder.DropIndex(
                name: "IX_Invoice_CustomerContractId",
                table: "Invoice");

            migrationBuilder.DropColumn(
                name: "CustomerContractId",
                table: "Invoice");

            migrationBuilder.AlterColumn<long>(
                name: "IdEstado",
                table: "Invoice",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CurrencyId",
                table: "Invoice",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<long>(
                name: "CertificadoDepositoId",
                table: "Invoice",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "ProformaInvoiceId",
                table: "Invoice",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "SalesOrderId",
                table: "Invoice",
                nullable: false,
                defaultValue: 0L);
        }
    }
}
