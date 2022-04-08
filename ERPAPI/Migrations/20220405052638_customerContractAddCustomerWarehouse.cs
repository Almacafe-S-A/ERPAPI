using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class customerContractAddCustomerWarehouse : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_CustomerContractWareHouse_CustomerContractId",
                table: "CustomerContractWareHouse",
                column: "CustomerContractId");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerContractWareHouse_CustomerContract_CustomerContractId",
                table: "CustomerContractWareHouse",
                column: "CustomerContractId",
                principalTable: "CustomerContract",
                principalColumn: "CustomerContractId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerContractWareHouse_CustomerContract_CustomerContractId",
                table: "CustomerContractWareHouse");

            migrationBuilder.DropIndex(
                name: "IX_CustomerContractWareHouse_CustomerContractId",
                table: "CustomerContractWareHouse");
        }
    }
}
