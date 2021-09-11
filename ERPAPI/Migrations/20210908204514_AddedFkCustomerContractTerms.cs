using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class AddedFkCustomerContractTerms : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerContractLinesTerms_CustomerContract_CustomerContractId",
                table: "CustomerContractLinesTerms");

            migrationBuilder.AlterColumn<long>(
                name: "CustomerContractId",
                table: "CustomerContractLinesTerms",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerContractLinesTerms_CustomerContract_CustomerContractId",
                table: "CustomerContractLinesTerms",
                column: "CustomerContractId",
                principalTable: "CustomerContract",
                principalColumn: "CustomerContractId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerContractLinesTerms_CustomerContract_CustomerContractId",
                table: "CustomerContractLinesTerms");

            migrationBuilder.AlterColumn<long>(
                name: "CustomerContractId",
                table: "CustomerContractLinesTerms",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerContractLinesTerms_CustomerContract_CustomerContractId",
                table: "CustomerContractLinesTerms",
                column: "CustomerContractId",
                principalTable: "CustomerContract",
                principalColumn: "CustomerContractId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
