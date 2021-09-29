using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class fkCustoermContractAdendum : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "CustomerContractIdAdendum",
                table: "CustomerContract",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CustomerContract_CustomerContractIdAdendum",
                table: "CustomerContract",
                column: "CustomerContractIdAdendum");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerContract_CustomerContract_CustomerContractIdAdendum",
                table: "CustomerContract",
                column: "CustomerContractIdAdendum",
                principalTable: "CustomerContract",
                principalColumn: "CustomerContractId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerContract_CustomerContract_CustomerContractIdAdendum",
                table: "CustomerContract");

            migrationBuilder.DropIndex(
                name: "IX_CustomerContract_CustomerContractIdAdendum",
                table: "CustomerContract");

            migrationBuilder.DropColumn(
                name: "CustomerContractIdAdendum",
                table: "CustomerContract");
        }
    }
}
