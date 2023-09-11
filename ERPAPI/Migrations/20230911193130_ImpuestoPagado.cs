using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class ImpuestoPagado : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "CuentaImpuestoPagadoId",
                table: "Tax",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CuentaImpuestoPagadoNombre",
                table: "Tax",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tax_CuentaImpuestoPagadoId",
                table: "Tax",
                column: "CuentaImpuestoPagadoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tax_Accounting_CuentaImpuestoPagadoId",
                table: "Tax",
                column: "CuentaImpuestoPagadoId",
                principalTable: "Accounting",
                principalColumn: "AccountId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tax_Accounting_CuentaImpuestoPagadoId",
                table: "Tax");

            migrationBuilder.DropIndex(
                name: "IX_Tax_CuentaImpuestoPagadoId",
                table: "Tax");

            migrationBuilder.DropColumn(
                name: "CuentaImpuestoPagadoId",
                table: "Tax");

            migrationBuilder.DropColumn(
                name: "CuentaImpuestoPagadoNombre",
                table: "Tax");
        }
    }
}
