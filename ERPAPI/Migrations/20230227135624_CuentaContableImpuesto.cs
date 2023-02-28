using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class CuentaContableImpuesto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Accounting",
                table: "Tax");

            migrationBuilder.AddColumn<long>(
                name: "CuentaImpuestoporCobrarId",
                table: "Tax",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CuentaImpuestoporCobrarNombre",
                table: "Tax",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "CuentaImpuestoporPagarId",
                table: "Tax",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CuentaImpuestoporPagarNombre",
                table: "Tax",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tax_CuentaImpuestoporCobrarId",
                table: "Tax",
                column: "CuentaImpuestoporCobrarId");

            migrationBuilder.CreateIndex(
                name: "IX_Tax_CuentaImpuestoporPagarId",
                table: "Tax",
                column: "CuentaImpuestoporPagarId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tax_Accounting_CuentaImpuestoporCobrarId",
                table: "Tax",
                column: "CuentaImpuestoporCobrarId",
                principalTable: "Accounting",
                principalColumn: "AccountId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tax_Accounting_CuentaImpuestoporPagarId",
                table: "Tax",
                column: "CuentaImpuestoporPagarId",
                principalTable: "Accounting",
                principalColumn: "AccountId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tax_Accounting_CuentaImpuestoporCobrarId",
                table: "Tax");

            migrationBuilder.DropForeignKey(
                name: "FK_Tax_Accounting_CuentaImpuestoporPagarId",
                table: "Tax");

            migrationBuilder.DropIndex(
                name: "IX_Tax_CuentaImpuestoporCobrarId",
                table: "Tax");

            migrationBuilder.DropIndex(
                name: "IX_Tax_CuentaImpuestoporPagarId",
                table: "Tax");

            migrationBuilder.DropColumn(
                name: "CuentaImpuestoporCobrarId",
                table: "Tax");

            migrationBuilder.DropColumn(
                name: "CuentaImpuestoporCobrarNombre",
                table: "Tax");

            migrationBuilder.DropColumn(
                name: "CuentaImpuestoporPagarId",
                table: "Tax");

            migrationBuilder.DropColumn(
                name: "CuentaImpuestoporPagarNombre",
                table: "Tax");

            migrationBuilder.AddColumn<long>(
                name: "Accounting",
                table: "Tax",
                nullable: false,
                defaultValue: 0L);
        }
    }
}
