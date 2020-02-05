using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class AddedFields_CheckLines : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RTN",
                table: "CheckAccountLines",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RetencionId",
                table: "CheckAccountLines",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CheckAccountLines_RetencionId",
                table: "CheckAccountLines",
                column: "RetencionId");

            migrationBuilder.AddForeignKey(
                name: "FK_CheckAccountLines_RetentionReceipt_RetencionId",
                table: "CheckAccountLines",
                column: "RetencionId",
                principalTable: "RetentionReceipt",
                principalColumn: "RetentionReceiptId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CheckAccountLines_RetentionReceipt_RetencionId",
                table: "CheckAccountLines");

            migrationBuilder.DropIndex(
                name: "IX_CheckAccountLines_RetencionId",
                table: "CheckAccountLines");

            migrationBuilder.DropColumn(
                name: "RTN",
                table: "CheckAccountLines");

            migrationBuilder.DropColumn(
                name: "RetencionId",
                table: "CheckAccountLines");
        }
    }
}
