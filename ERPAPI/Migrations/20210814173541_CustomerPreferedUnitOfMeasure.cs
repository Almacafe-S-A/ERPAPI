using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class CustomerPreferedUnitOfMeasure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UnitOfMeasureId",
                table: "Customer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UnitOfMeasurePreference",
                table: "Customer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Customer_UnitOfMeasureId",
                table: "Customer",
                column: "UnitOfMeasureId");

            migrationBuilder.AddForeignKey(
                name: "FK_Customer_UnitOfMeasure_UnitOfMeasureId",
                table: "Customer",
                column: "UnitOfMeasureId",
                principalTable: "UnitOfMeasure",
                principalColumn: "UnitOfMeasureId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customer_UnitOfMeasure_UnitOfMeasureId",
                table: "Customer");

            migrationBuilder.DropIndex(
                name: "IX_Customer_UnitOfMeasureId",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "UnitOfMeasureId",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "UnitOfMeasurePreference",
                table: "Customer");
        }
    }
}
