using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class Added_ADendumfields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerContract_CustomerContract_CustomerContractIdAdendum",
                table: "CustomerContract");

            migrationBuilder.RenameColumn(
                name: "CustomerContractIdAdendum",
                table: "CustomerContract",
                newName: "CustomerContractId_Source");

            migrationBuilder.RenameIndex(
                name: "IX_CustomerContract_CustomerContractIdAdendum",
                table: "CustomerContract",
                newName: "IX_CustomerContract_CustomerContractId_Source");

            migrationBuilder.AddColumn<long>(
                name: "CustomerContractId_Source",
                table: "SalesOrder",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AdendumNo",
                table: "CustomerContract",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CustomerContractType",
                table: "CustomerContract",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CustomerContractTypeName",
                table: "CustomerContract",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SalesOrder_CustomerContractId_Source",
                table: "SalesOrder",
                column: "CustomerContractId_Source");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerContract_CustomerContract_CustomerContractId_Source",
                table: "CustomerContract",
                column: "CustomerContractId_Source",
                principalTable: "CustomerContract",
                principalColumn: "CustomerContractId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SalesOrder_CustomerContract_CustomerContractId_Source",
                table: "SalesOrder",
                column: "CustomerContractId_Source",
                principalTable: "CustomerContract",
                principalColumn: "CustomerContractId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerContract_CustomerContract_CustomerContractId_Source",
                table: "CustomerContract");

            migrationBuilder.DropForeignKey(
                name: "FK_SalesOrder_CustomerContract_CustomerContractId_Source",
                table: "SalesOrder");

            migrationBuilder.DropIndex(
                name: "IX_SalesOrder_CustomerContractId_Source",
                table: "SalesOrder");

            migrationBuilder.DropColumn(
                name: "CustomerContractId_Source",
                table: "SalesOrder");

            migrationBuilder.DropColumn(
                name: "AdendumNo",
                table: "CustomerContract");

            migrationBuilder.DropColumn(
                name: "CustomerContractType",
                table: "CustomerContract");

            migrationBuilder.DropColumn(
                name: "CustomerContractTypeName",
                table: "CustomerContract");

            migrationBuilder.RenameColumn(
                name: "CustomerContractId_Source",
                table: "CustomerContract",
                newName: "CustomerContractIdAdendum");

            migrationBuilder.RenameIndex(
                name: "IX_CustomerContract_CustomerContractId_Source",
                table: "CustomerContract",
                newName: "IX_CustomerContract_CustomerContractIdAdendum");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerContract_CustomerContract_CustomerContractIdAdendum",
                table: "CustomerContract",
                column: "CustomerContractIdAdendum",
                principalTable: "CustomerContract",
                principalColumn: "CustomerContractId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
