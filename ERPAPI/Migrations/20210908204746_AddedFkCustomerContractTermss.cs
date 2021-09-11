using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class AddedFkCustomerContractTermss : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerContractLinesTerms_CustomerContractTerms_ContractTermId",
                table: "CustomerContractLinesTerms");

            migrationBuilder.AlterColumn<int>(
                name: "ContractTermId",
                table: "CustomerContractLinesTerms",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerContractLinesTerms_CustomerContractTerms_ContractTermId",
                table: "CustomerContractLinesTerms",
                column: "ContractTermId",
                principalTable: "CustomerContractTerms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerContractLinesTerms_CustomerContractTerms_ContractTermId",
                table: "CustomerContractLinesTerms");

            migrationBuilder.AlterColumn<int>(
                name: "ContractTermId",
                table: "CustomerContractLinesTerms",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerContractLinesTerms_CustomerContractTerms_ContractTermId",
                table: "CustomerContractLinesTerms",
                column: "ContractTermId",
                principalTable: "CustomerContractTerms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
