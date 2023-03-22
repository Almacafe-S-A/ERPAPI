using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class ccTax : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "AccountIdPorCobrar",
                table: "ProductRelation",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AccountNamePorCobrar",
                table: "ProductRelation",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductRelation_AccountIdPorCobrar",
                table: "ProductRelation",
                column: "AccountIdPorCobrar");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductRelation_Accounting_AccountIdPorCobrar",
                table: "ProductRelation",
                column: "AccountIdPorCobrar",
                principalTable: "Accounting",
                principalColumn: "AccountId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductRelation_Accounting_AccountIdPorCobrar",
                table: "ProductRelation");

            migrationBuilder.DropIndex(
                name: "IX_ProductRelation_AccountIdPorCobrar",
                table: "ProductRelation");

            migrationBuilder.DropColumn(
                name: "AccountIdPorCobrar",
                table: "ProductRelation");

            migrationBuilder.DropColumn(
                name: "AccountNamePorCobrar",
                table: "ProductRelation");
        }
    }
}
