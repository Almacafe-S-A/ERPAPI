using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class TaxRetentionReceipt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TaxDescription",
                table: "RetentionReceipt",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "TaxId",
                table: "RetentionReceipt",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_RetentionReceipt_TaxId",
                table: "RetentionReceipt",
                column: "TaxId");

            migrationBuilder.AddForeignKey(
                name: "FK_RetentionReceipt_Tax_TaxId",
                table: "RetentionReceipt",
                column: "TaxId",
                principalTable: "Tax",
                principalColumn: "TaxId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RetentionReceipt_Tax_TaxId",
                table: "RetentionReceipt");

            migrationBuilder.DropIndex(
                name: "IX_RetentionReceipt_TaxId",
                table: "RetentionReceipt");

            migrationBuilder.DropColumn(
                name: "TaxDescription",
                table: "RetentionReceipt");

            migrationBuilder.DropColumn(
                name: "TaxId",
                table: "RetentionReceipt");
        }
    }
}
