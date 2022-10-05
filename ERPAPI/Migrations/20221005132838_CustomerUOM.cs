using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class CustomerUOM : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customer_UnitOfMeasure_UnitOfMeasurePreference",
                table: "Customer");

            migrationBuilder.DropIndex(
                name: "IX_Customer_UnitOfMeasurePreference",
                table: "Customer");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Customer_UnitOfMeasurePreference",
                table: "Customer",
                column: "UnitOfMeasurePreference");

            migrationBuilder.AddForeignKey(
                name: "FK_Customer_UnitOfMeasure_UnitOfMeasurePreference",
                table: "Customer",
                column: "UnitOfMeasurePreference",
                principalTable: "UnitOfMeasure",
                principalColumn: "UnitOfMeasureId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
