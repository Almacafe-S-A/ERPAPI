using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class CuentasDebitNote : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "CuentaContableDebitoId",
                table: "DebitNote",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CuentaContableDebitoNombre",
                table: "DebitNote",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "CuentaContableIngresosId",
                table: "DebitNote",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CuentaContableIngresosNombre",
                table: "DebitNote",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DebitNote_CuentaContableDebitoId",
                table: "DebitNote",
                column: "CuentaContableDebitoId");

            migrationBuilder.CreateIndex(
                name: "IX_DebitNote_CuentaContableIngresosId",
                table: "DebitNote",
                column: "CuentaContableIngresosId");

            migrationBuilder.AddForeignKey(
                name: "FK_DebitNote_Accounting_CuentaContableDebitoId",
                table: "DebitNote",
                column: "CuentaContableDebitoId",
                principalTable: "Accounting",
                principalColumn: "AccountId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DebitNote_Accounting_CuentaContableIngresosId",
                table: "DebitNote",
                column: "CuentaContableIngresosId",
                principalTable: "Accounting",
                principalColumn: "AccountId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DebitNote_Accounting_CuentaContableDebitoId",
                table: "DebitNote");

            migrationBuilder.DropForeignKey(
                name: "FK_DebitNote_Accounting_CuentaContableIngresosId",
                table: "DebitNote");

            migrationBuilder.DropIndex(
                name: "IX_DebitNote_CuentaContableDebitoId",
                table: "DebitNote");

            migrationBuilder.DropIndex(
                name: "IX_DebitNote_CuentaContableIngresosId",
                table: "DebitNote");

            migrationBuilder.DropColumn(
                name: "CuentaContableDebitoId",
                table: "DebitNote");

            migrationBuilder.DropColumn(
                name: "CuentaContableDebitoNombre",
                table: "DebitNote");

            migrationBuilder.DropColumn(
                name: "CuentaContableIngresosId",
                table: "DebitNote");

            migrationBuilder.DropColumn(
                name: "CuentaContableIngresosNombre",
                table: "DebitNote");
        }
    }
}
