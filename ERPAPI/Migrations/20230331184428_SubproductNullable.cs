using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class SubproductNullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InvoicePaymentsLine_SubProduct_SubProductId",
                table: "InvoicePaymentsLine");

            migrationBuilder.AlterColumn<long>(
                name: "SubProductId",
                table: "InvoicePaymentsLine",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AddForeignKey(
                name: "FK_InvoicePaymentsLine_SubProduct_SubProductId",
                table: "InvoicePaymentsLine",
                column: "SubProductId",
                principalTable: "SubProduct",
                principalColumn: "SubproductId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InvoicePaymentsLine_SubProduct_SubProductId",
                table: "InvoicePaymentsLine");

            migrationBuilder.AlterColumn<long>(
                name: "SubProductId",
                table: "InvoicePaymentsLine",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_InvoicePaymentsLine_SubProduct_SubProductId",
                table: "InvoicePaymentsLine",
                column: "SubProductId",
                principalTable: "SubProduct",
                principalColumn: "SubproductId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
