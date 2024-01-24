using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class Pagos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BranchId",
                table: "InvoicePayments",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "CantidadenLetras",
                table: "InvoicePayments",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Observaciones",
                table: "InvoicePayments",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_InvoicePayments_BranchId",
                table: "InvoicePayments",
                column: "BranchId");

            migrationBuilder.AddForeignKey(
                name: "FK_InvoicePayments_Branch_BranchId",
                table: "InvoicePayments",
                column: "BranchId",
                principalTable: "Branch",
                principalColumn: "BranchId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InvoicePayments_Branch_BranchId",
                table: "InvoicePayments");

            migrationBuilder.DropIndex(
                name: "IX_InvoicePayments_BranchId",
                table: "InvoicePayments");

            migrationBuilder.DropColumn(
                name: "BranchId",
                table: "InvoicePayments");

            migrationBuilder.DropColumn(
                name: "CantidadenLetras",
                table: "InvoicePayments");

            migrationBuilder.DropColumn(
                name: "Observaciones",
                table: "InvoicePayments");
        }
    }
}
