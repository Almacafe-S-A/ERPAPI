using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class AddFieldsToProducts_Prima_FundingRate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FundingInterestRateId",
                table: "Product",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Prima",
                table: "Product",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Product_FundingInterestRateId",
                table: "Product",
                column: "FundingInterestRateId");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_FundingInterestRate_FundingInterestRateId",
                table: "Product",
                column: "FundingInterestRateId",
                principalTable: "FundingInterestRate",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_FundingInterestRate_FundingInterestRateId",
                table: "Product");

            migrationBuilder.DropIndex(
                name: "IX_Product_FundingInterestRateId",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "FundingInterestRateId",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "Prima",
                table: "Product");
        }
    }
}
